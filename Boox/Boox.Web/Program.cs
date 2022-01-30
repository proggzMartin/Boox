using Boox.Core.Interfaces;
using Boox.Infrastructure.Data;
using Boox.Infrastructure.Extensions;
using Boox.Infrastructure.Repositories;
using Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddEntityFrameworkSqlite()
    .AddDbContext<BooxContext>();

builder.Services.AddScoped<IBookRepo, BookRepo>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;
    var ctx = provider.GetRequiredService<BooxContext>();
    ctx.SeedBooks();
}

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
