using Nekretnine.Models;
using Nekretnine.Repositories;

namespace Nekretnine.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> GetUserById(int userId)
        {
            if (userId <= 0) throw new ArgumentException("Invalid user ID");
            return await _userRepository.GetUserByIdAsync(userId);
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("username");

            return await _userRepository.GetUserByUsernameAsync(username);
        }
        public int GetActivatedAgentCount()
        {
            return _userRepository.GetActivatedAgentCount();
        }
        public int GetDeactivatedAgentCount()
        {
            return _userRepository.GetDeactiatedAgentCount();
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _userRepository.GetAllClientsAsync();
        }
    }
}
