using Microsoft.EntityFrameworkCore;
using Nekretnine.Data;
using Nekretnine.Localization;
using Nekretnine.Models;

namespace Nekretnine.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealEstateDbContext _dbContext;

        public UserRepository(RealEstateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users
                           .Include(u => u.Agent)
                           .Include(u => u.Administrator)
                           .Include(u => u.Client)
                           .FirstOrDefaultAsync(u => u.Username == username);
        }


        public async Task<string> AddUserAsync(User user)
        {
            var localizer = Localizer.Instance;
            try
            {
                var existingUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Username == user.Username);

                if (existingUser != null)
                {
                    return localizer["user_exists"];
                }

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return localizer["create_success"];
            }
            catch (Exception ex)
            {
                return localizer["signup_error"];
            }
        }


        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.IdUser == id);
        }

        public async Task<string> AddAgentAsync(Agent agent)
        {
            await _dbContext.Agents.AddAsync(agent);
            await _dbContext.SaveChangesAsync();
            return "";
        }

        public int GetActivatedAgentCount()
        {
            return _dbContext.Agents
                             .Where(agent => agent.Activated)
                             .Count();
        }

        public int GetDeactiatedAgentCount()
        {
            return _dbContext.Agents
                             .Where(agent => !agent.Activated)
                             .Count();
        }

        public bool UserExists(string username)
        {
            return _dbContext.Users.Any(u => u.Username == username);
        }
        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _dbContext.Clients
                .Include(c => c.KorisnikIdKorisnikNavigation) // Assuming navigation to User
                .ToListAsync();
        }
    }
}
