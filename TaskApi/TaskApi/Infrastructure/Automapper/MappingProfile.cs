using AutoMapper;
using TaskApi.Domain;
using TaskApi.Models.v1;

namespace TaskApi.Infrastructure.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItemModel, TaskItem>();
        }
    }
}