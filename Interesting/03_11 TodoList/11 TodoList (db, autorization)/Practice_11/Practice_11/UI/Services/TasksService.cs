using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Business.DTO;
using Practice_11.Business.Services.Interfaces;
using Practice_11.Models.Tasks;
using Practice_11.UI.Services.Interfaces;

namespace Practice_11.UI.Services
{
    public class TasksService : ITasksService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        ITasksDbService tasksDbService;

        public TasksService(ITasksDbService tasksDbService)
        {
            this.tasksDbService = tasksDbService;
        }

        public async Task CreateNewTask(TaskViewModel newTask)
        {
            var task = objectMapper.Mapper.Map<TaskDTO>(newTask);
            await tasksDbService.CreateNewTask(task);
        }

        public async Task DeleteTask(int id)
        {
            await tasksDbService.DeleteTask(id);
        }

        public async Task<List<TaskViewModel>> GetAllTasks()
        {
            var result = await tasksDbService.GetAllTasks();
            return objectMapper.Mapper.Map<List<TaskViewModel>>(result);
        }

        public async Task<TaskViewModel> GetTaskById(int id)
        {
            var result = await tasksDbService.GetTaskById(id);
            return objectMapper.Mapper.Map<TaskViewModel>(result);
        }

        public async Task UpdateTask(TaskForEditViewModel model)
        {
            TaskDTO task = new TaskDTO
            {
                 Id = model.Id,
                 Text = model.Text,
                 UserId = model.UserId
            };
            
            await tasksDbService.UpdateTask(task);
        }
    }
}
