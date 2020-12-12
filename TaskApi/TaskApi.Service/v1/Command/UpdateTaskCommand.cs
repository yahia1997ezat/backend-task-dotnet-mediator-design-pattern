using System.Collections.Generic;
using MediatR;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Command
{
    public class UpdateTaskCommand : IRequest
    {
        public List<TaskItem> TaskItems { get; set; }
    }
}