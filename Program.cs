using Microsoft.AspNetCore.Mvc;
using RinhaBackend2024.Application;
using RinhaBackend2024.Data;
using RinhaBackend2024.Domain;

var builder = WebApplication.CreateSlimBuilder(args);
var configuration = builder.Configuration;
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddSingleton<IClientRepository, ClientRepositoryADO>();
builder.Services.AddSingleton<ClienteService>();
builder.Services.AddNpgsqlDataSource(configuration.GetConnectionString("postgress")!);

var app = builder.Build();
app.MapPost("/clientes/{id}/transacoes",
   async ([FromRoute] int id, [FromBody] TransactionDtoRequest transacaoRequest, [FromServices] ClienteService clientesService) =>
   {
       return await clientesService.DoTransation(id, transacaoRequest);
   });

app.MapGet("/clientes/{id}/extrato",
    async ([FromRoute] int id, [FromServices] ClienteService clientesService) =>
    {
        return await clientesService.GetTransationByClient(id);
    });
app.Run();
