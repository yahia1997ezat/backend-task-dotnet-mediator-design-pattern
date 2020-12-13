using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskApi.Data.Repository.v1;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Query
{
    public class GetTaskItemByUserGuidQueryHandler : IRequestHandler<GetTaskItemByUserGuidQuery, List<TaskItem>>
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public GetTaskItemByUserGuidQueryHandler(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<List<TaskItem>> Handle(GetTaskItemByUserGuidQuery request, CancellationToken cancellationToken)
        {
            return await _taskItemRepository.GetTaskItemByUserAsync(request.AssignedToUser, cancellationToken);
        }
    }
}