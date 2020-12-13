using System;
using System.Collections.Generic;
using MediatR;
using TaskApi.Domain;

namespace TaskApi.Service.v1.Query
{
   public class GetAllTaskItemsQuery : IRequest<List<TaskItem>>
    {
    }
}
