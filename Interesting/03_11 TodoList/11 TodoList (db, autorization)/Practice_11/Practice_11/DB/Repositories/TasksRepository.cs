using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_11.DB.Entities;

namespace Practice_11.DB.Repositories
{
    public class TasksRepository : BaseRepository<ToDoItem>
    {
        public TasksRepository(DatabaseContext context) : base(context)
        {
        }

        internal async Task DeleteTask(int id)
        {
            ToDoItem task = await GetAsync(id);
            db.Entry(task).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        internal async Task UpdateTask(ToDoItem task)
        {
            ToDoItem foundTask = await GetAsync(task.Id);
            foundTask.Text = task.Text;
            db.Entry(foundTask).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
