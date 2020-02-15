using System;
using System.Windows.Input;
using Newtonsoft.Json;

namespace TodoList.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public ICommand DeleteTodoCommand { get; internal set; }

        public Todo() { }

        public Todo(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CreatedDate = DateTime.Now;
        }
    }
}
