using System.Text.Json.Serialization;

namespace RinhaBackend2024.Domain;

public class Client
{


    [JsonIgnore]
    public long Id { get; set; }
    public long Limit { get; set; }

    public long Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = [];

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
}
