using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RinhaBackend2024.Domain;

[Table("transaction", Schema = "public")]
public class Transaction
{
    [Key]
    [Column("id")]
    [JsonIgnore]
    public long Id { get; set; }
    [Column("value")]
    [JsonPropertyName("valor")]
    public long Value { get; set; }
    [Column("type")]
    [JsonPropertyName("tipo")]
    public string Type { get; set; }
    [Column("description")]
    [JsonPropertyName("descricao")]
    public string Description { get; set; }
    [Column("realized")]
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