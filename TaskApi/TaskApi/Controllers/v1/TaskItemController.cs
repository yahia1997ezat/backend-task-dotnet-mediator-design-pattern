using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Domain;
using TaskApi.Models.v1;
using TaskApi.Service.v1.Command;
using TaskApi.Service.v1.Query;

namespace TaskApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TaskController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        ///     Action to create a new task in the database.
        /// </summary>
        /// <param name="taskItemModel">Model to create a new task</param>
        /// <returns>Returns the created task</returns>
        /// <response code="200">Returned if the task was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<TaskItem>> Task(TaskItemModel taskItemModel)
        {
            try
            {
                return await _mediator.Send(new CreateTaskItemCommand
                {
                    TaskItem = _mapper.Map<TaskItem>(taskItemModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///     Action to retrieve all tasks.
        /// </summary>
        /// <returns>Returns a list of all  tasks or an empty list, if no tasks is created yet</returns>
        /// <response code="200">Returned if the list of tasks was retrieved</response>
        /// <response code="400">Returned if the tasks could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> Tasks()
        {
            try
            {
                return await _mediator.Send(new GetAllTaskItemsQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        ///     Action to retrieve all completed tasks.
        /// </summary>
        /// <returns>Returns a list of all completed tasks or an empty list, if no completed tasks is complete yet</returns>
        /// <response code="200">Returned if the list of tasks was retrieved</response>
        /// <response code="400">Returned if the tasks could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("Completed")]
        public async Task<ActionResult<List<TaskItem>>> CompletedTasks()
        {
            try
            {
                return await _mediator.Send(new GetCompletedTaskItemsQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Action to complete a task.
        /// </summary>
        /// <param name="id">The id of the task which got paid</param>
        /// <returns>Returns the paid task</returns>
        /// <response code="200">Returned if the task was updated (paid)</response>
        /// <response code="400">Returned if the task could not be found with the provided id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("Complete/{id}")]
        public async Task<ActionResult<TaskItem>> Complete(Guid id)
        {
            try
            {
                var task = await _mediator.Send(new GetTaskItemByIdQuery()
                {
                    Id = id
                });

                if (task == null)
                {
                    return BadRequest($"No task found with the id {id}");
                }

                task.Status = Status.Completed;

                return await _mediator.Send(new CompleteTaskItemCommand()
                {
                    TaskItem = _mapper.Map<TaskItem>(task)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}