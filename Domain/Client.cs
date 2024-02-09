using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RinhaBackend2024.Domain;

[Table("clients", Schema = "public")]
public class Client
{
    private readonly string[] validOperation = ["c", "d"];

    [Key]
    [Column("id")]
    [JsonIgnore]
    public long Id { get; private set; }
    [Column("limit")]
    public long Limit { get; private set; }
    [Column("balance")]
    public long Balance { get; private set; }
    public List<Transaction> Transations { get; private set; } = [];

    public Client(long limit)
    {
        Limit = limit;
        Balance = 0;
    }

    public Client(long id, long limit, long balance)
    {
        Id = id;
        Limit = limit;
        Balance = balance;
    }


    public bool CreateTransaction(long value, string type, string description)
    {
        if (!validOperation.Contains(type.ToLower())) return false;
        int sizeDescription = description.Trim().Length;
        if (sizeDescription <= 0 && sizeDescription > 10) return false;

        if (type == "c")
            return Credit(value, description);
        else
            return Debit(value, description);
    }

    private bool Debit(long value, string description)
    {
        bool canDecrease = Limit + Balance - value >= 0;
        if (!canDecrease) return false;

        Balance -= value;
        AddNewTransaction(value, "d", description);
        return true;
    }

    public bool Credit(long value, string description)
    {
        Balance += value;
        AddNewTransaction(value, "c", description);
        return true;
    }

    private void AddNewTransaction(long value, string type, string description)
    {
        Transations.Add(new Transaction(value, type, description));
    }
}
