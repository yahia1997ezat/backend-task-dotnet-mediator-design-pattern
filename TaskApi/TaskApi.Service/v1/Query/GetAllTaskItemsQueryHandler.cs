using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskApi.Data.Repository.v1;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Query
{
    public class GetAllTaskItemsQueryHandler : IRequestHandler<GetAllTaskItemsQuery, List<TaskItem>>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public GetAllTaskItemsQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<List<TaskItem>> Handle(GetCompletedTaskItemsQuery request, CancellationToken cancellationToken)
        {
            return await _taskItemRepository.GetAllTasksAsync(cancellationToken);
        }

        public Task<List<TaskItem>> Handle(GetAllTaskItemsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}