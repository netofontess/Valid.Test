using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc.Authorization;
using Valid.Test.Api.Filters;
using Valid.Test.IOC;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<NotificationFilter>();
    options.Filters.Add<ExceptionFilter>();
});

// Add services to the container.
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddScoped<NotificationFilter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
