using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.DB.Entities;

namespace Practice_11.DB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        DatabaseContext db;
        TasksRepository _tasksRepository;
        UsersRepository _usersRepository;

        public TasksRepository TasksRepository
            => _tasksRepository ?? (_tasksRepository = new TasksRepository(db));

        public UsersRepository UsersRepository
           => _usersRepository ?? (_usersRepository = new UsersRepository(db));

        public UnitOfWork(DatabaseContext context)
        {
            db = context;
            //DbInit();
        }

        private void DbInit()
        {
            User user1 = new User
            {
                Email = "vasya@gmail.com",
                Password = "2222"
            };
            User user2 = new User
            {
                Email = "igor@gmail.com",
                Password = "1111"
            };

            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();


            ToDoItem task1 = new ToDoItem
            {
                Text = "Brush teeth",
                UserId = user1.Id
            };
            ToDoItem task2 = new ToDoItem
            {
                Text = "Buy a car",
                UserId = user1.Id
            };
            ToDoItem task3 = new ToDoItem
            {
                Text = "Meet friends",
                UserId = user2.Id
            };
            ToDoItem task4 = new ToDoItem
            {
                Text = "Take money from John",
                UserId = user2.Id
            };
            ToDoItem task5 = new ToDoItem
            {
                Text = "Drink some beer",
                UserId = user2.Id
            };

            db.ToDoItems.Add(task1);
            db.ToDoItems.Add(task2);
            db.ToDoItems.Add(task3);
            db.ToDoItems.Add(task4);
            db.ToDoItems.Add(task5);

            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
