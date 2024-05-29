using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.AppServices;
using TaskManagerAPI.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<TaskService>();

builder.Services.AddDbContext<TaskMgrContext>(dbCtx =>  
            dbCtx.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=taskmgr"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
