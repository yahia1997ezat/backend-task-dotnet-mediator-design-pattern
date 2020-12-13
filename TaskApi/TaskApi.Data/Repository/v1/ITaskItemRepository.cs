using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskApi.Domain;

namespace TaskApi.Data.Repository.v1
{
    public interface ITaskItemRepository: IRepository<TaskItem>
    {
        Task<List<TaskItem>> GetCompletedTasksAsync(CancellationToken cancellationToken);
        Task<List<TaskItem>> GetAllTasksAsync(CancellationToken cancellationToken);

        Task<TaskItem> GetTaskItemByIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task<List<TaskItem>> GetTaskItemByUserAsync(Guid assignedToUser, CancellationToken cancellationToken);

    }
}