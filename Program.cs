using RinhaBackend2024.Data;
using RinhaBackend2024.Domain;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IClientRepository, ClientMongoRepository>();
builder.Services.AddSingleton<IContextConnection>(new ContextConnection(configuration));
builder.Services.AddNpgsqlDataSource(configuration.GetConnectionString("postgress")!);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
