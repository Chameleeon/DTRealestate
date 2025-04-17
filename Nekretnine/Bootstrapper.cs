using System.Windows;

namespace Nekretnine
{
    using System;
    using Caliburn.Micro;
    using Microsoft.EntityFrameworkCore;
    using Nekretnine.Data;
    using Nekretnine.Repositories;
    using Nekretnine.Services;
    using Nekretnine.ViewModels;

    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container;

        public Bootstrapper()
        {
            _container = new SimpleContainer();
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container.Singleton<IConfigurationService, ConfigurationService>();
            var configService = _container.GetInstance<IConfigurationService>();
            var connectionString = configService.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<RealEstateDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            _container.Instance(optionsBuilder.Options);
            _container.PerRequest<RealEstateDbContext>();

            _container.Singleton<IUserRepository, UserRepository>();
            _container.Singleton<IRealestateRepository, RealestateRepository>();
            _container.Singleton<IInquiryRepository, InquiryRepository>();
            _container.Singleton<IAuthService, AuthService>();
            _container.Singleton<IUserService, UserService>();
            _container.Singleton<IRealestateService, RealestateService>();
            _container.Singleton<IAgentService, AgentService>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IOfferService, OfferService>();
            _container.Singleton<IInquiryService, InquiryService>();
            _container.Singleton<IContractService, ContractService>();
            _container.Singleton<IContractRepository, ContractRepository>();

            _container.PerRequest<MainViewModel>();
            _container.PerRequest<HomeViewModel>();
            _container.PerRequest<LoginViewModel>();
            _container.PerRequest<SignupViewModel>();
            _container.PerRequest<OffersViewModel>();
            _container.PerRequest<RealestatesViewModel>();
            _container.PerRequest<RealestatesCardViewModel>();
            _container.PerRequest<OfferAddViewModel>();
            _container.PerRequest<DashboardViewModel>();
            _container.PerRequest<AdminDashboardViewModel>();
            _container.PerRequest<AdminAgentsViewModel>();
            _container.PerRequest<InquiriesViewModel>();
            _container.PerRequest<InquirySummaryViewModel>();
            _container.PerRequest<ReplyViewModel>();
            _container.PerRequest<ErrorViewModel>();
            _container.PerRequest<ConfirmationViewModel>();
            _container.PerRequest<ContractsViewModel>();
            _container.PerRequest<AdminHomeViewModel>();
            _container.PerRequest<SettingsViewModel>();
            _container.PerRequest<OfferEditViewModel>();
            _container.PerRequest<RealestateEditViewModel>();
            _container.PerRequest<RealestateDetailsViewModel>();
            _container.PerRequest<OfferDetailsViewModel>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var windowManager = new WindowManager();
            var viewModel = _container.GetInstance<MainViewModel>();
            windowManager.ShowWindowAsync(viewModel);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
    }
}
