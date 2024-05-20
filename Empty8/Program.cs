using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Use保哥專用的例外處理程式();

app.Use(async (context, next) =>
{
    var buffer = new System.IO.MemoryStream();
    var originalBody = context.Response.Body;
    context.Response.Body = buffer;

    await next();

    buffer.Seek(0, System.IO.SeekOrigin.Begin);
    var responseBody = await new System.IO.StreamReader(buffer).ReadToEndAsync();
    buffer.Seek(0, System.IO.SeekOrigin.Begin);
    await buffer.CopyToAsync(originalBody);
    context.Response.Body = originalBody;
});

app.Use(async (context, next) =>
{
    var remoteIpAddress = context.Connection.RemoteIpAddress;

    if (判斷是否允許瀏覽該網站(remoteIpAddress))
    {
        await next();
    }
    else
    {
        await context.Response.WriteAsync("Bad request");
    }
});

bool 判斷是否允許瀏覽該網站(IPAddress? remoteIpAddress)
{
    return false;
}

app.Use(async (context, next) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    await context.Response.WriteAsync("<h1>");

    await next();

    await context.Response.WriteAsync("</h1>");
});

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("3");

    await next();

    await context.Response.WriteAsync("4");
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync("5");
});

app.Run();
