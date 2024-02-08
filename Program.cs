using Microsoft.EntityFrameworkCore;
using RinhaBackend2024.Data;
using RinhaBackend2024.Domain;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddDbContext<DataContext>(options => options
.UseNpgsql(configuration.GetConnectionString("postgress"), x =>
{
    x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
}
));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
