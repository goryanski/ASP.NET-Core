using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Business.DTO;

namespace Practice_11.Business.Services.Interfaces
{
    public interface ITasksDbService
    {
        Task<List<TaskDTO>> GetAllTasks();
        Task<TaskDTO> GetTaskById(int id);
        Task CreateNewTask(TaskDTO task);
        Task<bool> CheckTask(int taskId, string userEmail);
        Task DeleteTask(int id);
        Task UpdateTask(TaskDTO task);
    }
}
