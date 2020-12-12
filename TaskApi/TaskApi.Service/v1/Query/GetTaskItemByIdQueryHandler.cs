using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskApi.Data.Repository.v1;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Query
{
    public class GetTaskItemByIdQueryHandler : IRequestHandler<GetTaskItemByIdQuery, TaskItem>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItem> Handle(GetTaskItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskItemRepository.GetTaskItemByIdAsync(request.Id, cancellationToken);
        }
    }
}