using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Part3_Mod32.Middlewares
{
    public class LoggingMiddleware
    {
        private IWebHostEnvironment env;
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this.env = env;
            _next = next;
        }

        private void LogConsole(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private async Task LogFile(HttpContext context)
        {
            await File.AppendAllTextAsync(Path.Combine(env.ContentRootPath, "Logs", "RequestLog.txt"), $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}");
            //await _next.Invoke(context);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogConsole(context);
            await LogFile(context);
            await _next.Invoke(context);
        }
    }
}
