using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RinhaBackend2024.Application.DTOs
{
    public record TransactionDtoRequest(
        [property: JsonPropertyName("valor")] long Value,

        [property: JsonPropertyName("tipo")] string Type,

        [Required]
        [Length(1, 10)]
        [property: JsonPropertyName("descricao")] string Description
    );
}
