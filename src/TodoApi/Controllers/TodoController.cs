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

		/*
		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
		*/
	}
}
