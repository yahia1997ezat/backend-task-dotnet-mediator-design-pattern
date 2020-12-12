using System;
using MediatR;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Query
{
   public class GetTaskItemByIdQuery : IRequest<TaskItem>
    {
        public Guid Id { get; set; }
    }
}
