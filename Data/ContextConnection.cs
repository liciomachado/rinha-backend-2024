using MongoDB.Driver;

namespace RinhaBackend2024.Data;


public interface IContextConnection
{
    IMongoDatabase GetDatabase();
}

public class ContextConnection : IContextConnection
{

    private MongoClient _client = null!;
    private string ConnectionString = "";
    private string DatabaseName = "";

    public ContextConnection(IConfiguration configuration)
    {
        Connect(configuration);
    }

    public IMongoDatabase GetDatabase()
    {
        return _client.GetDatabase(DatabaseName);
    }

    private void Connect(IConfiguration _configuration)
    {
        ConnectionString = _configuration["MONGO_CONNECTION"]!;
        DatabaseName = _configuration["MONGO_DATABASE"]!;
        _client = new MongoClient(ConnectionString);
    }
}