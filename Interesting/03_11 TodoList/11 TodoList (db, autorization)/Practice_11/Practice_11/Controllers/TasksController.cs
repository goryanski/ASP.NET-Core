using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice_11.Models.Tasks;
using Practice_11.UI.Services.Interfaces;

namespace Practice_11.Controllers
{
    public class TasksController : Controller
    {
        ITasksService tasksService;
        IUsersService usersService;
        public TasksController(ITasksService tasksService, IUsersService usersService)
        {
            this.tasksService = tasksService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            TaskViewModel task = new TaskViewModel
            {
                Text = model.Text,
                UserId = await usersService.GetUserIdByEmail(User.Identity.Name)
            };
            await tasksService.CreateNewTask(task);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Missing id param of task to delete");
            }
            TaskViewModel task = await tasksService.GetTaskById(id);
            if (task is null)
            {
                return BadRequest("Task wasn`t found, so it can't be deleted");
            }
            await tasksService.DeleteTask(task.Id);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest("Missing id param of task to edit");
            }
            TaskViewModel task = await tasksService.GetTaskById(id);
            if (task is null)
            {
                return BadRequest("Task wasn`t found, so it can't be edit");
            }
            return View(new TaskForEditViewModel 
            { 
                Text = task.Text,
                Id = task.Id,
                UserId = task.UserId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskForEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await tasksService.UpdateTask(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
