using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Business.DTO;
using Practice_11.Business.Services.Interfaces;
using Practice_11.DB.Entities;
using Practice_11.DB.Repositories;

namespace Practice_11.Business.Services
{
    public class TasksDbService : ITasksDbService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        IUnitOfWork uow;
        public TasksDbService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<List<TaskDTO>> GetAllTasks()
        {
            var result = await uow.TasksRepository.GetAllAsync();
            return objectMapper.Mapper.Map<List<TaskDTO>>(result);
        }

        public async Task<bool> CheckTask(int taskId, string userEmail)
        {
            var tasks = await uow.TasksRepository.GetAllAsync();
            var foundTask = tasks.FirstOrDefault(t => t.Id == taskId);
            var foundUser = await uow.UsersRepository.GetAsync(foundTask.UserId);
            return foundUser.Email.Equals(userEmail);
        }

        public async Task CreateNewTask(TaskDTO newTask)
        {
            var task = objectMapper.Mapper.Map<ToDoItem>(newTask);
            await uow.TasksRepository.CreateAsync(task);
        }

        public async Task<TaskDTO> GetTaskById(int id)
        {
            var result = await uow.TasksRepository.GetAsync(id);
            return objectMapper.Mapper.Map<TaskDTO>(result);
        }

        public async Task DeleteTask(int id)
        {
            await uow.TasksRepository.DeleteTask(id);
        }

        public async Task UpdateTask(TaskDTO task)
        {
            var result = objectMapper.Mapper.Map<ToDoItem>(task);
            await uow.TasksRepository.UpdateTask(result);
        }
    }
}
