using Microsoft.EntityFrameworkCore;
using Nekretnine.Data;
using Nekretnine.Models;

namespace Nekretnine.Services
{
    public interface IAgentService
    {
        Task<List<Agent>> GetAllAgentsAsync();
        Task UpdateAgentAsync(Agent agent);
    }

    public class AgentService : IAgentService
    {
        private readonly RealEstateDbContext _dbContext;

        public AgentService(RealEstateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Agent>> GetAllAgentsAsync()
        {
            return await _dbContext.Agents.Include(a => a.KorisnikIdKorisnikNavigation)
                                           .ToListAsync();
        }

        public async Task UpdateAgentAsync(Agent agent)
        {
            _dbContext.Agents.Update(agent);
            await _dbContext.SaveChangesAsync();
        }
    }
}
