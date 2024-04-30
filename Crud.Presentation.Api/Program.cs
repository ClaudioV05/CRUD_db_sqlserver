using Crud.Application.Interfaces;
using Crud.Application.Services;
using Crud.Infraestructure.Data.Context;
using Crud.Infraestructure.Data.Repositories;
using Crud.Infraestructure.Domain.Entities;
using Crud.Presentation.Api.Extensions;
using Crud.Presentation.Api.Filters;
using Crud.Presentation.Api.Swagger;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options => options.RespectBrowserAcceptHeader = true);

builder.Services.AddCors();

builder.Services.AddMvc().AddMvcOptions(options => options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

builder.Services.AddDbContext<DatabaseContext>();

#region Action Filters.
builder.Services.AddScoped<FilterActionContextController>();
builder.Services.AddScoped<FilterActionContextLog>();
builder.Services.AddScoped<FilterActionContextFields<Users>>();
builder.Services.AddScoped<FilterActionContextTables<Users>>();
#endregion Action Filters.

builder.Services.AddScoped<IServiceUsers, ServiceUsers>();

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    // Code for Development here.
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
    });

    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/Crud")
        {
            context.Response.Redirect("/swagger/index.html");
            return;
        }

        await next();
    });

    app.UseDeveloperExceptionPage();
}
else if (app.Environment.IsStaging())
{
    // Code for Homologation here.
}
else if (app.Environment.IsProduction())
{
    // Code for Production here.

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();