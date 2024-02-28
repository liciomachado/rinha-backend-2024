using System.Text.Json.Serialization;

namespace RinhaBackend2024.Domain;

public class Transaction
{
    [JsonIgnore]
    public long Id { get; set; }
    [JsonPropertyName("valor")]
    public long Value { get; set; }
    [JsonPropertyName("tipo")]
    public string Type { get; set; }
    [JsonPropertyName("descricao")]
    public string Description { get; set; }
    [JsonPropertyName("realizada_em")]
    public DateTime Realized { get; set; } = DateTime.Now;

    public Transaction(long value, string type, string description)
    {
        Value = value;
        Type = type;
        Description = description;
    }

    public Transaction(long id, long value, string type, string description, DateTime realized)
    {
        Id = id;
        Value = value;
        Type = type;
        Description = description;
        Realized = realized;
    }
}