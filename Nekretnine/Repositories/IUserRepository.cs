using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<string> AddUserAsync(User user);
        Task<string> AddAgentAsync(Agent agent);
        Task<User?> GetUserByIdAsync(int id);
        int GetActivatedAgentCount();
        int GetDeactiatedAgentCount();
        bool UserExists(string username);
        Task<List<Client>> GetAllClientsAsync();
    }
}
