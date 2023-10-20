using Entity.Models;
using Microsoft.EntityFrameworkCore;
using ReportService.Rabbit;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ bağlantı ayarlarını okuyun
var rabbitMQConfig = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();

// PostgreSQL bağlantı dizesini alın
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// PostgreSQL veritabanı bağlamını yapılandırın
builder.Services.AddDbContext<EntityContext>(options =>options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Entity")));


builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
