using System;
using System.Collections.Generic;
using MediatR;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Query
{
    public class GetTaskItemByUserGuidQuery : IRequest<List<TaskItem>>
    {
        public Guid AssignedToUser { get; set; }
    }
}