﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MvcStartApp.Models.Db;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MvcStartApp.Middlewares
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
            //await _next.Invoke(context); // Поскольку мы вызываем LogFie из InvokeAsync, в котором уже есть await _next.Invoke, присутствие здесь еще одного await приводит к двойному добавлению записи в БД и выбрасыванию исключения
        }

        private async Task LogDb(HttpContext context)
        {
            Request request = new();
            request.Date = DateTime.Now;
            request.Url = context.Request.Host.Value + context.Request.Path;
            
            //await .AddRequest(request); // context.Request.Host.Value + context.Request.Path);
            //await _next.Invoke(context); // Поскольку мы вызываем LogFie из InvokeAsync, в котором уже есть await _next.Invoke, присутствие здесь еще одного await приводит к двойному добавлению записи в БД и выбрасыванию исключения
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogConsole(context);
            await LogFile(context);
            await LogDb(context);
            await _next.Invoke(context);
        }
    }
}
