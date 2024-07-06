using Microsoft.EntityFrameworkCore;
using SimpleChatApp.BLL.Services.Contracts;
using SimpleChatApp.BLL.Services;
using SimpleChatApp.DAL;
using SimpleChatApp.DAL.Infrastructure.Interfaces;
using SimpleChatApp.DAL.Infrastructure;
using SimpleChatApp.DAL.Repositories.Contracts;
using SimpleChatApp.DAL.Repositories;
using SimpleChatApp.API.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//db context
builder.Services.AddDbContext<SimpleChatDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SimpleChatApp.DAL")));

// AutoMapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// services
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserInChatService, UserInChatService>();

// repositories
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserInChatRepository, UserInChatRepository>();

// infrastructure
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
