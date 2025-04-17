using Nekretnine.Models;

namespace Nekretnine.Services
{
    public interface IAuthService
    {
        public User LogedInUser { get; set; }
        Task<bool> LoginAsync(string username, string password);
        Task<string> RegisterUserAsync(User user);

        Task<string> RegisterAgentAsync(Agent agent);
    }

}
