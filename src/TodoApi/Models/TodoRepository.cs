using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
	using System.Collections.Concurrent;

	public class TodoRepository : ITodoRepository
	{
		private static readonly ConcurrentDictionary<string, TodoItem> todos = new ConcurrentDictionary<string, TodoItem>();

		public TodoRepository()
		{
			this.Add(new TodoItem { Name = "Item1", IsComplete = false });
		}

		public IEnumerable<TodoItem> GetAll()
		{
			return todos.Values;
		}

		public void Add(TodoItem item)
		{
			item.Key = Guid.NewGuid().ToString();
			todos[item.Key] = item;
		}

		public TodoItem Find(string key)
		{
			TodoItem item;
			todos.TryGetValue(key, out item);
			return item;
		}

		public TodoItem Remove(string key)
		{
			TodoItem item;
			todos.TryRemove(key, out item);
			return item;
		}

		public void Update(TodoItem item)
		{
			todos[item.Key] = item;
		}
	}
}
