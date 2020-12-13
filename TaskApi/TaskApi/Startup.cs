using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TaskApi.Data.Database;
using TaskApi.Data.Repository.v1;
using TaskApi.Domain;
using TaskApi.Messaging.Receive.Options.v1;
using TaskApi.Models.v1;
using TaskApi.Service.v1.Command;
using TaskApi.Service.v1.Query;

using TaskApi.Validators.v1;

namespace TaskApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            services.AddDbContext<TaskItemContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Task Api",
                    Description = "A simple API to create or tasks",
                    Contact = new OpenApiContact
                    {
                        Name = "Yahia Qumboz",
                        Email = "yahiaqumboz@gmail.com",
                        Url = new Uri("https://www.yahia.tech/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count ==
                        actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();

            services.AddTransient<IValidator<TaskItemModel>, TaskItemModelValidator>();

            services
                .AddTransient<IRequestHandler<GetCompletedTaskItemsQuery, List<TaskItem>>, GetCompletedTaskItemsQueryHandler>();
            services
                .AddTransient<IRequestHandler<GetAllTaskItemsQuery, List<TaskItem>>, GetAllTaskItemsQueryHandler>();
            services.AddTransient<IRequestHandler<GetTaskItemByIdQuery, TaskItem>, GetTaskItemByIdQueryHandler>();
            services
                .AddTransient<IRequestHandler<GetTaskItemByUserGuidQuery, List<TaskItem>>,
                    GetTaskItemByUserGuidQueryHandler>();
            services.AddTransient<IRequestHandler<CreateTaskItemCommand, TaskItem>, CreateTaskItemCommandHandler>();
            services.AddTransient<IRequestHandler<CompleteTaskItemCommand, TaskItem>, CompleteTaskCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateTaskCommand>, UpdateTaskItemCommandHandler>();

         
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}