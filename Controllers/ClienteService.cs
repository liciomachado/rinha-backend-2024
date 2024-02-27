using Microsoft.AspNetCore.Mvc;
using RinhaBackend2024.Application;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Controllers
{
    public class ClienteService([FromServices] IClientRepository _clientRepository)
    {
        public async Task<IResult> DoTransation(int id, TransactionDtoRequest transactionDTO)
        {
            //if (!ModelState.IsValid) return Results.UnprocessableEntity();
            if (transactionDTO.Description == null || transactionDTO.Description!.Length < 1 || transactionDTO.Description!.Length > 10)
                return Results.UnprocessableEntity();

            var client = await _clientRepository.GetAsync(id);
            if (client == null) return Results.NotFound();

            var isOperationValid = client.CreateTransaction(transactionDTO.Value, transactionDTO.Type, transactionDTO.Description);
            if (!isOperationValid) return Results.UnprocessableEntity();

            await _clientRepository.UpdateAsync(client);

            return Results.Ok(new TransactionDtoResponse(client.Limit, client.Balance));
        }

        public async Task<IResult> GetTransationByClient(int id)
        {
            var client = await _clientRepository.GetWithTransactionAsync(id);
            if (client == null) return Results.NotFound();

            ClientDataDtoResponse clientDataDto = new()
            {
                Saldo = new Saldo
                {
                    DataExtrato = DateTime.Now,
                    Limite = client.Limit,
                    Total = client.Balance,

                },
                UltimasTransacoes = [.. client.Transactions.OrderByDescending(x => x.Id).Take(10)]
            };

            return Results.Ok(clientDataDto);
        }
    }
}
