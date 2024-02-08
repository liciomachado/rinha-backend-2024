using Microsoft.EntityFrameworkCore;
using RinhaBackend2024.Domain;

namespace RinhaBackend2024.Data;

public class ClientRepository(DataContext context) : IClientRepository
{
    private readonly DataContext _context = context;

    public async Task<Client> GetAsync(int id)
    {
        return await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Client> GetWithTransactionAsync(int id)
    {
        return await _context.Clients.Include(x => x.Transations).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }
}
