using Microsoft.AspNetCore.Http;
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
        public void AddUserSync(User user);

        Task<User[]> GetUsers();

    }

    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public void AddUserSync(User user)
        {
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<User[]> GetUsers()
        {
            return await _context.Users.ToArrayAsync();
        }

        public async Task AddRequest( Request request)
        {
            //Request request = new();
            //request.Date = DateTime.Now;
            request.Id = Guid.NewGuid();
            //request.Url = httpContext.Request.Host.Value + httpContext.Request.Path;

            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached) // Это, наверное, не нужно?
                await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

    }
}
