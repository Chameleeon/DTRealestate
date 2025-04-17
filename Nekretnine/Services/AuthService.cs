using Nekretnine.Helpers;
using Nekretnine.Models;
using Nekretnine.Repositories;

namespace Nekretnine.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User LogedInUser { get; set; }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
                return false;

            if (PasswordHasher.VerifyPassword(password, user.Password, user.Salt))
            {
                return true;
            }

            return false;
        }

        public async Task<string> RegisterUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<string> RegisterAgentAsync(Agent agent)
        {
            return await _userRepository.AddAgentAsync(agent);
        }
    }

}
