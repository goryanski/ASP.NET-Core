using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Models.Tasks;

namespace Practice_11.UI.Services.Interfaces
{
    public interface ITasksService
    {
        Task<List<TaskViewModel>> GetAllTasks();
        Task<TaskViewModel> GetTaskById(int id);
        Task CreateNewTask(TaskViewModel task);
        Task DeleteTask(int id);
        Task UpdateTask(TaskForEditViewModel model);
    }
}
