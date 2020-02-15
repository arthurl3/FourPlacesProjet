using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Settings;
using TodoList.Models;

namespace TodoList.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetAllTodos();
        Task DeleteTodo(Todo todo);
        Task CreateTodo(Todo todo);
        Task EditTodo(Todo todo);
    }

    public class TodoService : ITodoService
    {
        private const string TODO_LIST = nameof(TODO_LIST);

        private List<Todo> _todoList;

        public TodoService() { }

        public Task CreateTodo(Todo todo)
        {
            _todoList.Add(todo);
            SaveTodos();
            return Task.CompletedTask;
        }

        public Task DeleteTodo(Todo todo)
        {
            _todoList.Remove(todo);
            SaveTodos();
            return Task.CompletedTask;
        }

        public Task EditTodo(Todo todo)
        {
            //edited by reference
            SaveTodos();
            return Task.CompletedTask;
        }

        public Task<List<Todo>> GetAllTodos()
        {
            InitializeIfNeeded();
            return Task.FromResult(_todoList);
        }

        private void InitializeIfNeeded()
        {
            if (_todoList is null)
            {
                var serializedTodoList = CrossSettings.Current.GetValueOrDefault(TODO_LIST, string.Empty);
                if (string.IsNullOrEmpty(serializedTodoList))
                {
                    _todoList = new List<Todo>();
                }
                else
                {
                    _todoList = JsonConvert.DeserializeObject<List<Todo>>(serializedTodoList);
                }
            }
        }

        private void SaveTodos()
        {
            CrossSettings.Current.AddOrUpdateValue(TODO_LIST, JsonConvert.SerializeObject(_todoList));
        }
    }
}
