using System.Text.Json.Serialization;

namespace RinhaBackend2024.Application.DTOs
{
    public class TransactionDtoResponse(long limite, long saldo)
    {
        [JsonPropertyName("limite")]
        public long Limite { get; set; } = limite;
        [JsonPropertyName("saldo")]
        public long Saldo { get; set; } = saldo;
    }
}
