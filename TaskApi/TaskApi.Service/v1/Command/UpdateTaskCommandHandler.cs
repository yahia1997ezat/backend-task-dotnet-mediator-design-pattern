using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskApi.Data.Repository.v1;

namespace TaskApi.Service.v1.Command
{
    public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public UpdateTaskItemCommandHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            await _taskItemRepository.UpdateRangeAsync(request.TaskItems);

            return Unit.Value;
        }
    }
}