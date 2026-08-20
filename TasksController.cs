using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;
using TaskTrackerApi.Repositories;

namespace TaskTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAll()
        {
            var tasks = await _repository.GetAllAsync();
            return Ok(tasks.Select(ToResponseDto));
        }

        // GET: api/tasks/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> GetById(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task is null) return NotFound();

            return Ok(ToResponseDto(task));
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> Create([FromBody] TaskCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate
            };

            var created = await _repository.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToResponseDto(created));
        }

        // PUT: api/tasks/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var task = new TaskItem
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                IsComplete = dto.IsComplete,
                DueDate = dto.DueDate
            };

            var updated = await _repository.UpdateAsync(task);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        private static TaskResponseDto ToResponseDto(TaskItem task) => new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsComplete = task.IsComplete,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate
        };
    }
}
