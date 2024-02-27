using Npgsql;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Data;

public class ClientRepository(NpgsqlDataSource connection) : IClientRepository
{
    private readonly NpgsqlDataSource _connection = connection;

    public async Task<Client> GetAsyncLock(int id)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = """SELECT id, "limit", balance FROM public.clients WHERE Id = @Id FOR UPDATE""";
        cmd.Parameters.AddWithValue("Id", id);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (reader.Read())
        {
            var client = new Client(reader.GetInt64(0), reader.GetInt64(1), reader.GetInt64(2));
            return client;
        }
        return null;
    }

    public async Task<Client> GetAsync(int id)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = """SELECT id, "limit", balance FROM public.clients WHERE Id = @Id""";
        cmd.Parameters.AddWithValue("Id", id);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (reader.Read())
        {
            var client = new Client(reader.GetInt64(0), reader.GetInt64(1), reader.GetInt64(2));
            return client;
        }
        return null;
    }

    public async Task<Client> GetWithTransactionAsync(int id)
    {
        var client = await GetAsync(id);
        if (client == null) return null;
        var transaction = await GetTransactionsByClientId(id);
        client.SetTransactions(transaction);
        return client;
    }

    private async Task<List<Transaction>> GetTransactionsByClientId(long clientId)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = """SELECT "id", "value", "type", "description", "realized", "ClientId" FROM public."transaction" WHERE "ClientId" = @clientId ORDER BY "id" DESC LIMIT 10""";
        cmd.Parameters.AddWithValue("clientId", clientId);
        await using var reader = await cmd.ExecuteReaderAsync();
        var resultados = new List<Transaction>();
        while (reader.Read())
        {
            Transaction transaction = new(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2), reader.GetString(3), reader.GetFieldValue<DateTime>(4));
            resultados.Add(transaction);
        }
        return resultados;
    }

    public async Task UpdateAsync(Client client)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = """UPDATE public.clients SET "balance" = @balance WHERE "id" = @id""";
        cmd.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Integer, client.Id);
        cmd.Parameters.AddWithValue("limit", NpgsqlTypes.NpgsqlDbType.Integer, client.Limit);
        cmd.Parameters.AddWithValue("balance", NpgsqlTypes.NpgsqlDbType.Integer, client.Balance);
        await cmd.ExecuteNonQueryAsync();

        foreach (var transaction in client.Transations) //ignorando casos de update de transacoes
        {
            cmd.CommandText = """INSERT INTO public."transaction" ("value", "type", "description", "realized", "ClientId") VALUES (@value, @type, @description, @realized, @clientId)""";
            cmd.Parameters.AddWithValue("value", NpgsqlTypes.NpgsqlDbType.Integer, transaction.Value);
            cmd.Parameters.AddWithValue("type", NpgsqlTypes.NpgsqlDbType.Text, transaction.Type);
            cmd.Parameters.AddWithValue("description", NpgsqlTypes.NpgsqlDbType.Text, transaction.Description);
            cmd.Parameters.AddWithValue("realized", NpgsqlTypes.NpgsqlDbType.Timestamp, transaction.Realized);
            cmd.Parameters.AddWithValue("clientId", NpgsqlTypes.NpgsqlDbType.Integer, client.Id);
            var id = await cmd.ExecuteNonQueryAsync();
            transaction.Id = id;
        }
    }
}
