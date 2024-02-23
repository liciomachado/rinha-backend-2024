using RinhaBackend2024.Domain;
using System.Text.Json.Serialization;

namespace RinhaBackend2024.Application;

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

public record TransactionDtoRequest(
    [property: JsonPropertyName("valor")] long Value,

    [property: JsonPropertyName("tipo")] string Type,

    //[Required]
    //[Length(1, 10)]
    [property: JsonPropertyName("descricao")] string? Description
);

public class TransactionDtoResponse(long limite, long saldo)
{
    [JsonPropertyName("limite")]
    public long Limite { get; set; } = limite;
    [JsonPropertyName("saldo")]
    public long Saldo { get; set; } = saldo;
}

[JsonSerializable(typeof(Saldo))]
[JsonSerializable(typeof(TransactionDtoRequest))]
[JsonSerializable(typeof(ClientDataDtoResponse))]
[JsonSerializable(typeof(TransactionDtoResponse))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}