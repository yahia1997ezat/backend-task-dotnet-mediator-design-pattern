using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskApi.Data.Repository.v1;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Command
{
    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskItemCommand, TaskItem>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public CompleteTaskCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItem> Handle(CompleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            return await _taskItemRepository.UpdateAsync(request.TaskItem);
        }
    }
}