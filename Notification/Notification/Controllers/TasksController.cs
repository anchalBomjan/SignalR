using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Commands;
using Notification.DTOs;
using Notification.Queries;

namespace Notification.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasks()
        {
            var tasks = await _mediator.Send(new GetTasksQuery());
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTask(CreateTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return Ok(taskId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(Guid id, UpdateTaskCommand command)
        {
            if (id != command.Id) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id, [FromQuery] string deletedBy)
        {
            await _mediator.Send(new DeleteTaskCommand { Id = id, DeletedBy = deletedBy });
            return NoContent();
        }
    }
}
