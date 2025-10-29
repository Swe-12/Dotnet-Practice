using Microsoft.AspNetCore.Mvc;
using TaskTrackerAPI.Models;
using System.Collections.Generic;
using System.Linq;


namespace TaskTrackerAPI.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
// In-memory store for demo. Replace with DbContext for production.
private static readonly List<TaskItem> tasks = new List<TaskItem>();
private static int nextId = 1;


[HttpGet]
public IActionResult GetAll() => Ok(tasks);


[HttpPost]
public IActionResult Create([FromBody] TaskItem newTask)
{
if (newTask == null) return BadRequest();
newTask.Id = nextId++;
tasks.Add(newTask);
return CreatedAtAction(nameof(GetAll), new { id = newTask.Id }, newTask);
}


[HttpPut("{id}")]
public IActionResult Update(int id, [FromBody] TaskItem updated)
{
var t = tasks.FirstOrDefault(x => x.Id == id);
if (t == null) return NotFound();
t.Title = updated.Title;
t.Description = updated.Description;
t.IsCompleted = updated.IsCompleted;
return Ok(t);
}


[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
var t = tasks.FirstOrDefault(x => x.Id == id);
if (t == null) return NotFound();
tasks.Remove(t);
return NoContent();
}
}
}