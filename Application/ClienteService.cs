using Microsoft.AspNetCore.Mvc;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Application
{
    public class ClienteService([FromServices] IClientRepository _clientRepository)
    {
        private readonly string[] validOperation = ["c", "d"];

        public async Task<IResult> DoTransation(int id, TransactionDtoRequest transactionDTO)
        {
            if (string.IsNullOrEmpty(transactionDTO.Description)
                || transactionDTO.Description!.Length > 10
                || !validOperation.Contains(transactionDTO.Type.ToLower())
                || transactionDTO.Value < 1)
                return Results.UnprocessableEntity();


            var client = await _clientRepository.GetAsync(id);
            if (client == null) return Results.NotFound();

            var isOperationValid = client.CreateTransaction(transactionDTO.Value, transactionDTO.Type, transactionDTO.Description);
            if (!isOperationValid) return Results.UnprocessableEntity();

            var ok = await _clientRepository.UpdateAsync(client, transactionDTO.Type);
            if (!ok) return Results.UnprocessableEntity();

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
