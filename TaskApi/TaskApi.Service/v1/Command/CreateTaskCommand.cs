using MediatR;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Command
{
    public class CreateTaskItemCommand : IRequest<TaskItem>
    {
        public TaskItem TaskItem { get; set; }
    }
}
