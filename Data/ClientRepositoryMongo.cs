using MongoDB.Bson;
using MongoDB.Driver;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Data;

public class ClientRepositoryMongo : IClientRepository
{
    private readonly IMongoDatabase _database;

    public ClientRepositoryMongo(IContextConnection contextConnection)
    {
        _database = contextConnection.GetDatabase();
        InitDb();
    }

    public async Task<Client> GetAsync(int id)
    {
        var collection = _database.GetCollection<Client>("clientes");
        var filter = Builders<Client>.Filter.Eq(c => c.Id, id);
        var clientFound = await collection.Find(filter).FirstOrDefaultAsync();
        return clientFound;

    }

    public async Task<Client> GetWithTransactionAsync(int id)
    {
        return await GetAsync(id);
    }

    public async Task UpdateAsync(Client client)
    {
        var filterCliente = Builders<Client>.Filter.Eq(c => c.Id, client.Id);
        var updateCliente = Builders<Client>.Update
            .Set(c => c.Limit, client.Limit)
            .Set(c => c.Balance, client.Balance)
            .Set(c => c.Transactions, client.Transactions);
        await _database.GetCollection<Client>("clientes").UpdateOneAsync(filterCliente, updateCliente);
    }
    private void InitDb()
    {
        var collection = _database.GetCollection<BsonDocument>("clientes");
        var filter = Builders<BsonDocument>.Filter.Empty; // Filtro vazio para contar todos os documentos
        long count = collection.CountDocuments(filter);

        if (count > 0) return;
        var clients = new List<Client>()
        {
            new(1, 100000, 0),
            new(2, 80000, 0),
            new(3, 1000000, 0),
            new(4, 10000000, 0),
            new(5, 500000, 0)
        };
        _database.GetCollection<Client>("clientes").InsertMany(clients);
    }
}