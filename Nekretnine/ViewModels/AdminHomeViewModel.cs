using Caliburn.Micro;
using Nekretnine.Services;

namespace Nekretnine.ViewModels
{
    public class AdminHomeViewModel : Screen
    {
        private readonly IUserService _userService;

        public AdminHomeViewModel(IUserService userService)
        {
            _userService = userService;
            LoadAgentStats();
        }

        private int _activatedAgentsCount;
        public int ActivatedAgentsCount
        {
            get => _activatedAgentsCount;
            set
            {
                _activatedAgentsCount = value;
                NotifyOfPropertyChange("" + ActivatedAgentsCount);
            }
        }

        private int _deactivatedAgentsCount;
        public int DeactivatedAgentsCount
        {
            get => _deactivatedAgentsCount;
            set
            {
                _deactivatedAgentsCount = value;
                NotifyOfPropertyChange("" + DeactivatedAgentsCount);
            }
        }

        private void LoadAgentStats()
        {
            ActivatedAgentsCount = _userService.GetActivatedAgentCount();
            DeactivatedAgentsCount = _userService.GetDeactivatedAgentCount();
        }
    }
}
