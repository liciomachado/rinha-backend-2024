namespace RinhaBackend2024.Domain;

public interface IClientRepository
{
    Task<Client> GetAsync(int id);
    Task<Client> GetWithTransactionAsync(int id);
    Task<bool> UpdateAsync(Client client, string type);
}
