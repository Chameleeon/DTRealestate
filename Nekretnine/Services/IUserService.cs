using Nekretnine.Models;

namespace Nekretnine.Services
{
    public interface IUserService
    {
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByUsername(string username);
        int GetActivatedAgentCount();
        int GetDeactivatedAgentCount();
        Task<List<Client>> GetClientsAsync();
    }
}
