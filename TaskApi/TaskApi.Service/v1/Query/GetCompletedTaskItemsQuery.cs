using System.Collections.Generic;
using MediatR;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Query
{
    public class GetCompletedTaskItemsQuery : IRequest<List<TaskItem>>
    {
    }
}