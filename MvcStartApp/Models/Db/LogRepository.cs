using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public interface ILogRepository
    {
        public Task AddRequest(Request request);
        Task<Request[]> GetRequests();

    }

    public class LogRepository : ILogRepository
    {
        private readonly BlogContext _context;
        public LogRepository(BlogContext context)
        {
            _context = context;
        }

        //public async Task AddUser(User user)
        //{
        //    user.JoinDate = DateTime.Now;
        //    user.Id = Guid.NewGuid();

        //    var entry = _context.Entry(user);
        //    if (entry.State == EntityState.Detached)
        //        await _context.Users.AddAsync(user);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }

        public async Task AddRequest(Request request)
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
