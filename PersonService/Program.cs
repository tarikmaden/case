using Microsoft.EntityFrameworkCore;
using PersonService.Data;
using Entity.Models;
using PersonService;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EntityContext>(options => options.UseNpgsql(connectionString));
// builder.Services.AddDbContext<EntityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<EntityContext>();
        context.Database.Migrate();
        ContactTypeSeeder.SeedData(services);
    }
    catch (Exception ex)
    {
        // Hata durumunda yapılacaklar
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
