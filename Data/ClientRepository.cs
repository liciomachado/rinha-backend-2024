using Npgsql;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Data;

public class ClientRepository(NpgsqlDataSource connection) : IClientRepository
{
    private readonly NpgsqlDataSource _connection = connection;

    public async Task<Client> GetAsync(int id)
    {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = """SELECT id, "limit", balance FROM public.clients WHERE Id = @Id""";
        cmd.Parameters.AddWithValue("Id", id);

        await using var reader = await cmd.ExecuteReaderAsync();

        if (reader.Read())
        {

            var Id = reader.GetInt64(0);
            var limit = reader.GetInt64(1);
            var balance = reader.GetInt64(2);

            var client = new Client(Id, limit, balance);

            return client;
        }

        return null;
    }

    public async Task<Client> GetWithTransactionAsync(int id)
    {
        var client = await GetAsync(id);
        var transaction = await GetTransactionsByClientId(id);
        client.SetTransactions(transaction);
        return client;
    }

    private async Task<List<Transaction>> GetTransactionsByClientId(long clientId)
    {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = """SELECT "id", "value", "type", "description", "realized", "ClientId" FROM public."transaction" WHERE "ClientId" = @clientId ORDER BY "id" DESC""";
        cmd.Parameters.AddWithValue("clientId", clientId);
        await using var reader = await cmd.ExecuteReaderAsync();
        var resultados = new List<Transaction>();
        while (reader.Read())
        {

            var id = reader.GetFieldValue<long>(0);
            var value = reader.GetInt64(1);
            var type = reader.GetString(2);
            var description = reader.GetString(3);
            var date = reader.GetFieldValue<DateTime>(4);

            Transaction transaction = new(id, value, type, description, date);

            resultados.Add(transaction);
        }
        return resultados;
    }

    private async Task<Transaction> AddTransaction(Transaction transaction, long clientId)
    {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = """INSERT INTO public."transaction" ("value", "type", "description", "realized", "ClientId") VALUES (@value, @type, @description, @realized, @clientId)""";
        cmd.Parameters.AddWithValue("value", NpgsqlTypes.NpgsqlDbType.Integer, transaction.Value);
        cmd.Parameters.AddWithValue("type", NpgsqlTypes.NpgsqlDbType.Text, transaction.Type);
        cmd.Parameters.AddWithValue("description", NpgsqlTypes.NpgsqlDbType.Text, transaction.Description);
        cmd.Parameters.AddWithValue("realized", NpgsqlTypes.NpgsqlDbType.Timestamp, transaction.Realized);
        cmd.Parameters.AddWithValue("clientId", NpgsqlTypes.NpgsqlDbType.Integer, clientId);
        var id = await cmd.ExecuteNonQueryAsync();
        transaction.Id = id;
        return transaction;
    }

    public async Task UpdateAsync(Client client)
    {
        var cmd = _connection.CreateCommand();
        cmd.CommandText = """UPDATE public.clients SET "limit" = @limit, "balance" = @balance WHERE "id" = @id""";
        cmd.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Integer, client.Id);
        cmd.Parameters.AddWithValue("limit", NpgsqlTypes.NpgsqlDbType.Integer, client.Limit);
        cmd.Parameters.AddWithValue("balance", NpgsqlTypes.NpgsqlDbType.Integer, client.Balance);
        await cmd.ExecuteNonQueryAsync();

        foreach (var item in client.Transations) //ignorando casos de update
        {
            await AddTransaction(item, client.Id);
        }
    }
}
