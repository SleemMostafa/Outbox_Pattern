using Microsoft.EntityFrameworkCore;
using Outbox_Pattern;
using Outbox_Pattern.Data;
using Outbox_Pattern.Endpoints;
using Outbox_Pattern.Services.Mails;
using Outbox_Pattern.Services.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureEmailService();

builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<SendEmailServiceFaced>();
builder.Services.AddHostedService<SendEmailWorker>();
var configuration = builder.Configuration;
builder.Services.AddDbContext<Db>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.MapEmployeeEndpoints();

app.Run();