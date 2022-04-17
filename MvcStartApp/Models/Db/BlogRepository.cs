using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public interface IBlogRepository
    {
        public Task AddUser(User user);
        //public void AddUserSync(User user);

        Task<User[]> GetUsers();

    }

    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        //public async Task AddUser(User user)
        //{
        //    var entry = _context.Entry(user);
        //    if (entry.State == EntityState.Detached)
        //        await _context.Users.AddAsync(user);
        //    await _context.SaveChangesAsync();
        //}
        //public void AddUserSync(User user)
        //{
        //    var entry = _context.Entry(user);
        //    if (entry.State == EntityState.Detached)
        //        _context.Users.Add(user);
        //    _context.SaveChanges();
        //}

        public async Task<User[]> GetUsers()
        {
            return await _context.Users.ToArrayAsync();
        }

        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
    }

}
