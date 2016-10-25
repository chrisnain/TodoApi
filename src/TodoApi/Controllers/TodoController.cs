// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc;
	using TodoApi.Models;

	[Route("api/[controller]")]
	public class TodoController : Controller
	{
		public TodoController(ITodoRepository todoItems)
		{
			this.TodoItems = todoItems;
		}

		public ITodoRepository TodoItems { get; set; }

		// GET: api/todo
		[HttpGet]
		public IEnumerable<TodoItem> Get()
		{
			return this.TodoItems.GetAll();
		}

		// GET api/todo/{id}
		[HttpGet("{id}", Name = "GetTodo")]
		public IActionResult GetById(string id)
		{
			var item = this.TodoItems.Find(id);
			if (item == null)
			{
				return this.NotFound();
			}

			return new ObjectResult(item);
		}

		// POST api/todo
		[HttpPost]
		public IActionResult Create([FromBody] TodoItem item)
		{
			if (item == null)
			{
				return this.BadRequest();
			}

			this.TodoItems.Add(item);

			return this.CreatedAtRoute("GetTodo", new { id = item.Key }, item);
		}

		// PUT api/todo/{id}
		[HttpPut("{id}")]
		public IActionResult Update(string id, [FromBody] TodoItem item)
		{
			if (item == null || item.Key != id)
			{
				return this.BadRequest();
			}

			var todo = this.TodoItems.Find(id);
			if (todo == null)
			{
				return this.NotFound();
			}

			this.TodoItems.Update(item);
			return new NoContentResult();
		}

		// PATCH api/todo/{id}
		[HttpPatch("{id}")]
		public IActionResult Update([FromBody] TodoItem item, string id)
		{
			if (item == null)
			{
				return this.BadRequest();
			}

			var todo = this.TodoItems.Find(id);
			if (todo == null)
			{
				return this.NotFound();
			}

			item.Key = todo.Key;

			this.TodoItems.Update(item);
			return new NoContentResult();
		}

		// DELETE api/values/{id}
		[HttpDelete("{id}")]
		public IActionResult Delete(string id)
		{
			var todo = this.TodoItems.Find(id);
			if (todo == null)
			{
				return this.NotFound();
			}

			this.TodoItems.Remove(id);
			return new NoContentResult();
		}
	}
}
