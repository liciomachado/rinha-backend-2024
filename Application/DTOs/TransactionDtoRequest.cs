using System.Text.Json.Serialization;

namespace RinhaBackend2024.Application.DTOs
{
    public record TransactionDtoRequest(
        [property: JsonPropertyName("valor")] long Value,
        [property: JsonPropertyName("tipo")] string Type,
        [property: JsonPropertyName("descricao")] string Description
    );
}
