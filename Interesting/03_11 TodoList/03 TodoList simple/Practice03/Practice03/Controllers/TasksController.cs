using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice03.Helpers;
using Practice03.Models;

namespace Practice03.Controllers
{
    public class TasksController: Controller
    {
        private TasksStorage tasksStorage = TasksStorage.Instance;
        public IActionResult Index()
        {
            ViewData["title"] = "Todo List";
            ViewBag.Tasks = tasksStorage.Tasks;
            return View();
        }

        public IActionResult Add()
        {
            ViewData["title"] = "Add task";
            return View();
        }

        [HttpPost]
        public IActionResult Add(ToDoItem task)
        {
            task.Id = Guid.NewGuid().ToString();
            tasksStorage.AddTask(task);
            return RedirectToAction("index"); 
        }



        public IActionResult Delete(string id)
        {
            if (id is null)
            {
                return BadRequest("Missing id param of task to delete");
            }
            ToDoItem task = tasksStorage.GetTaskById(id);
            if (task is null)
            {
                return BadRequest("Task wasn`t found, so it can't be deleted");
            }
            tasksStorage.RemoveTask(task);
            return RedirectToAction("index");
        }



        public IActionResult Edit(string id)
        {
            if (id is null)
            {
                return BadRequest("Missing id param of task to edit");
            }
            ToDoItem task = tasksStorage.GetTaskById(id);
            if (task is null)
            {
                return BadRequest("Task wasn`t found, so it can't be edit");
            }
            ViewData["title"] = "Edit task";
            ViewBag.TextToEdit = task.Text;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(ToDoItem task)
        {
            tasksStorage.EditTask(task);
            return RedirectToAction("index");
        }
    }
}
