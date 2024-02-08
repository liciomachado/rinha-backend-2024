using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RinhaBackend2024.Domain;

[Table("transaction", Schema = "public")]
public class Transaction(long value, string type, string description)
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
    [Column("value")]
    [JsonPropertyName("valor")]
    public long Value { get; set; } = value;
    [Column("type")]
    [JsonPropertyName("tipo")]
    public string Type { get; set; } = type;
    [Column("description")]
    [JsonPropertyName("descricao")]
    public string Description { get; set; } = description;
    [Column("realized")]
    [JsonPropertyName("realizada_em")]
    public DateTime Realized { get; set; } = DateTime.Now;
}