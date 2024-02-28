using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace RinhaBackend2024.Domain;

public class Client
{
    private readonly string[] validOperation = ["c", "d"];

    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    [JsonIgnore]
    public long Id { get; private set; }
    [BsonElement("limit")]
    public long Limit { get; private set; }

    [BsonElement("balance")]
    public long Balance { get; private set; }
    [BsonElement("transations")]
    public List<Transaction> Transactions { get; private set; } = [];

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
        if (sizeDescription == 0 || sizeDescription > 10) return false;

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
        Transactions.Add(new Transaction(value, type, description));
    }

    public void SetTransactions(List<Transaction> transations)
    {
        Transactions = transations;
    }

    public void SetBalance(long balance)
    {
        Balance = balance;
    }
}
