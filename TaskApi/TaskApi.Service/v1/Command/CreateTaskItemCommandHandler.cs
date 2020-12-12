using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskApi.Data.Repository.v1;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Command
{
    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, TaskItem>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItem> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            return await _taskItemRepository.AddAsync(request.TaskItem);
        }
    }
}