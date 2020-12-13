using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data.Database;
using TaskApi.Domain;

namespace TaskApi.Data.Repository.v1
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(TaskItemContext taskItemContext) : base(taskItemContext)
        {
        }

        public async Task<List<TaskItem>> GetCompletedTasksAsync(CancellationToken cancellationToken)
        {
            return await TaskItemContext.TaskItem.Where(x => x.Status == Status.Completed).ToListAsync( cancellationToken);
        }

        public async Task<List<TaskItem>> GetAllTasksAsync(CancellationToken cancellationToken)
        {
            return await TaskItemContext.TaskItem.ToListAsync(cancellationToken);
        }

        public async Task<TaskItem> GetTaskItemByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await TaskItemContext.TaskItem.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
        }

        public Task<List<TaskItem>> GetTaskItemByUserAsync(Guid assignedToUser, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskItem>> GetTaskItemByUserAsync(string assignedToUser, CancellationToken cancellationToken)
        {
            return await TaskItemContext.TaskItem.Where(x => x.AssignedToUser == assignedToUser).ToListAsync( cancellationToken);
        }

    }
}