using System.Collections.ObjectModel;
using Caliburn.Micro;
using Nekretnine.Models;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class AdminAgentsViewModel : Screen
    {
        private readonly IAgentService _agentService;

        public ObservableCollection<Agent> Agents { get; set; } = new ObservableCollection<Agent>();

        public AdminAgentsViewModel(IAgentService agentService)
        {
            _agentService = agentService;
            LoadAgents();
        }

        public async Task LoadAgents()
        {
            var agents = await _agentService.GetAllAgentsAsync();
            Agents.Clear();

            foreach (var agent in agents)
            {
                Agents.Add(agent);
            }
        }

        public async Task ToggleActivation(Agent agent)
        {
            agent.Activated = !agent.Activated;
            await _agentService.UpdateAgentAsync(agent);
            await LoadAgents();
        }
    }
}
