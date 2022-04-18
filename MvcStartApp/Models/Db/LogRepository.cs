using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public interface ILogRepository
    {
        public Task AddRequest(string urlInfo);

        Task<Request[]> GetRequests();

    }

    public class LogRepository : ILogRepository
    {
        private readonly BlogContext _context;
        public LogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }

        public async Task AddRequest(string urlInfo)
        {
            Request request = new();
            request.Url = urlInfo;

            // Добавление пользователя
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
    }
}
