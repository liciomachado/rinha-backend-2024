using Microsoft.AspNetCore.Mvc;
using RinhaBackend2024.Application.DTOs;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController([FromServices] IClientRepository _clientRepository) : ControllerBase
    {
        [HttpPost("{id}/transacoes")]
        public async Task<IActionResult> DoTransation(int id, TransactionDtoRequest transactionDTO)
        {
            if (!ModelState.IsValid) return UnprocessableEntity();

            var client = await _clientRepository.GetAsync(id);
            if (client == null) return NotFound();

            var isOperationValid = client.CreateTransaction(transactionDTO.Value, transactionDTO.Type, transactionDTO.Description);
            await _clientRepository.UpdateAsync(client);
            if (!isOperationValid) return UnprocessableEntity();

            return Ok(new TransactionDtoResponse(client.Limit, client.Balance));
        }

        [HttpGet("{id}/extrato")]
        public async Task<IActionResult> GetTransationByClient(int id)
        {
            var client = await _clientRepository.GetWithTransactionAsync(id);
            if (client == null) return NotFound();

            ClientDataDtoResponse clientDataDto = new()
            {
                Saldo = new Saldo
                {
                    DataExtrato = DateTime.Now,
                    Limite = client.Limit,
                    Total = client.Balance,

                },
                UltimasTransacoes = [.. client.Transactions.OrderByDescending(x => x.Realized).Take(10)]
            };

            return Ok(clientDataDto);
        }
    }
}
