using RinhaBackend2024.Domain;
using System.Text.Json.Serialization;

namespace RinhaBackend2024.Application.DTOs;

public class ClientDataDtoResponse
{
    [JsonPropertyName("saldo")]
    public Saldo Saldo { get; set; }

    [JsonPropertyName("ultimas_transacoes")]
    public List<Transaction>? UltimasTransacoes { get; set; }
}

public class Saldo
{
    [JsonPropertyName("total")]
    public long Total { get; set; }

    [JsonPropertyName("data_extrato")]
    public DateTime DataExtrato { get; set; }

    [JsonPropertyName("limite")]
    public long Limite { get; set; }
}