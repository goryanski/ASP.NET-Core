using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice03.Models;

namespace Practice03.Helpers
{
    public class TasksStorage
    {
        private static TasksStorage instance;
        public static TasksStorage Instance { get => instance ?? (instance = new TasksStorage()); }

        public List<ToDoItem> Tasks { get; set; }

        private TasksStorage()
        {
            InitTasks();
        }

        private void InitTasks()
        {
            Tasks = new List<ToDoItem>
            {
                new ToDoItem 
                {
                    Id = Guid.NewGuid().ToString(), 
                    Text = "Brush teeth"
                },
                new ToDoItem 
                {
                    Id = Guid.NewGuid().ToString(), 
                    Text = "Buy a car"
                },
                new ToDoItem 
                {
                    Id = Guid.NewGuid().ToString(), 
                    Text = "Meet friends"
                },
                new ToDoItem 
                {
                    Id = Guid.NewGuid().ToString(), 
                    Text = "Take money from John"
                },
                new ToDoItem 
                {
                    Id = Guid.NewGuid().ToString(), 
                    Text = "Drink some beer"
                }
            };
        }

        internal ToDoItem GetTaskById(string id) => Tasks.FirstOrDefault(t => t.Id == id);

        internal void RemoveTask(ToDoItem task)
        {
            Tasks.Remove(task);
        }

        internal void AddTask(ToDoItem task)
        {
            Tasks.Add(task);
        }

        internal void EditTask(ToDoItem task)
        {
            ToDoItem srchTask = GetTaskById(task.Id);
            if (srchTask != null)
            {
                srchTask.Text = task.Text;
            }
        }
    }
}
