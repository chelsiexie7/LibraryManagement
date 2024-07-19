using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Middlewares
{
    public class RepeatNameMiddleware
    {
        private readonly RequestDelegate _next;

        public RepeatNameMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
        {
            // 检查是否是新建操作
            if (context.Request.Method == HttpMethods.Post && 
                (context.Request.Path.StartsWithSegments("/Customer/Create") ||
                 context.Request.Path.StartsWithSegments("/Author/Create") ||
                 context.Request.Path.StartsWithSegments("/Book/Create") ||
                 context.Request.Path.StartsWithSegments("/LibraryBranch/Create")))
            {
                // 读取请求内容
                context.Request.EnableBuffering();
                using var reader = new System.IO.StreamReader(context.Request.Body, System.Text.Encoding.UTF8, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                // 检查重复的名称或标题
                bool isDuplicate = false;
                if (context.Request.Path.StartsWithSegments("/Customer/Create"))
                {
                    var name = ExtractValueFromBody(body, "CustomerName");
                    isDuplicate = dbContext.Customers.Any(c => c.Name == name);
                }
                else if (context.Request.Path.StartsWithSegments("/Author/Create"))
                {
                    var name = ExtractValueFromBody(body, "AuthorName");
                    isDuplicate = dbContext.Authors.Any(a => a.Name == name);
                }
                else if (context.Request.Path.StartsWithSegments("/Book/Create"))
                {
                    var title = ExtractValueFromBody(body, "BookTitle");
                    isDuplicate = dbContext.Books.Any(b => b.Title == title);
                }
                else if (context.Request.Path.StartsWithSegments("/LibraryBranch/Create"))
                {
                    var name = ExtractValueFromBody(body, "BranchName");
                    isDuplicate = dbContext.LibraryBranches.Any(lb => lb.BranchName == name);
                }

                if (isDuplicate)
                {
                    // 设置状态码和错误消息
                   if (isDuplicate)
                {
                    context.Items["CustomStatusCode"] = 0000;
                    context.Response.Redirect("/Home/Error");
                    return;
                }

                   
                    //await context.Response.WriteAsync("The Name/Title is already exist. Please check your input.");
                    return;
                }
            }

            // 调用下一个中间件
            await _next(context);
        }

        private string ExtractValueFromBody(string body, string key)
        {
            // 简单实现：提取JSON键值对中的值
            var keyValue = System.Text.Json.JsonDocument.Parse(body).RootElement.GetProperty(key);
            return keyValue.GetString();
        }

        
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RepeatNameMiddlewareExtensions
    {
        public static IApplicationBuilder UseRepeatNameMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RepeatNameMiddleware>();
        }
    }
}
