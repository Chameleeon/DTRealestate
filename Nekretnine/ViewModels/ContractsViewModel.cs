using System.Collections.ObjectModel;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using Nekretnine.Localization;
using Nekretnine.Services;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace Nekretnine.ViewModels
{
    public class ContractsViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IContractService _contractService;
        private readonly Dictionary<string, string> _localizedToInvariantOfferType = new()
        {
            { Localizer.Instance["for_sale"], "Sale" },
            { Localizer.Instance["for_rent"], "Rent" }
        };


        private ObservableCollection<ContractItemViewModel> _contracts;
        public ObservableCollection<ContractItemViewModel> Contracts
        {
            get => _contracts;
            set
            {
                _contracts = value;
                NotifyOfPropertyChange(() => Contracts);
            }
        }

        private List<ContractItemViewModel> _allContracts;

        public List<string> SortOptions { get; } = new List<string>
        {
            Localizer.Instance["newest_first"],
           Localizer.Instance["oldest_first"]
        };

        private string _selectedSortOption = Localizer.Instance["newest_first"];
        public string SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                _selectedSortOption = value;
                NotifyOfPropertyChange(() => SelectedSortOption);
                ApplyFilters();
            }
        }


        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                NotifyOfPropertyChange(() => SearchQuery);
                ApplyFilters();
            }
        }

        public ObservableCollection<string> ContractTypes { get; } = new()
        {
            Localizer.Instance["all_selection"],
            Localizer.Instance["for_rent"],
            Localizer.Instance["for_sale"]
        };


        private string _selectedContractType = Localizer.Instance["all_selection"];
        public string SelectedContractType
        {
            get => _selectedContractType;
            set
            {
                _selectedContractType = value;
                NotifyOfPropertyChange(() => SelectedContractType);
                ApplyFilters();
            }
        }

        public ContractsViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, IContractService contractService)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _contractService = contractService;

            Contracts = new ObservableCollection<ContractItemViewModel>();
            _allContracts = new List<ContractItemViewModel>();
            LoadData();
        }

        public async Task LoadData()
        {
            var contracts = await _contractService.GetAllContractsAsync();
            foreach (var contract in contracts)
            {
                var adr = contract.OfferIdOfferNavigation.RealestateIdRealestateNavigation.AdresaIdAdresaNavigation;
                string address = $"{adr.Street} {adr.Number}, {adr.City}";

                var vm = new ContractItemViewModel(contract, address);
                _allContracts.Add(vm);
            }

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = _allContracts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                filtered = filtered.Where(c =>
                    c.Address.Contains(SearchQuery, StringComparison.InvariantCultureIgnoreCase) ||
                    c.Contract.IdContract.ToString().Contains(SearchQuery));
            }

            if (!string.IsNullOrWhiteSpace(SelectedContractType) &&
                SelectedContractType != Localizer.Instance["all_selection"] &&
                _localizedToInvariantOfferType.TryGetValue(SelectedContractType, out var invariantType))
            {
                filtered = filtered.Where(c =>
                    string.Equals(c.Contract.OfferIdOfferNavigation.TipIdTipNavigation.OfferType1,
                                  invariantType,
                                  StringComparison.InvariantCultureIgnoreCase));
            }


            filtered = SelectedSortOption switch
            {
                "Newest First" => filtered.OrderByDescending(c => c.Contract.SignDate),
                "Najnovije prvo" => filtered.OrderByDescending(c => c.Contract.SignDate),
                "Oldest First" => filtered.OrderBy(c => c.Contract.SignDate),
                "Najstarije prvo" => filtered.OrderBy(c => c.Contract.SignDate),
                _ => filtered
            };

            Contracts = new ObservableCollection<ContractItemViewModel>(filtered);
        }


        public void ViewDetails(ContractItemViewModel contractItem)
        {
            var detailsViewModel = new ContractDetailsViewModel(contractItem.Contract);
            _windowManager.ShowDialogAsync(detailsViewModel);
        }


        public void SaveAsPdf(ContractItemViewModel contractViewModel)
        {
            var contractItem = contractViewModel.Contract;
            var dialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Contract_{contractItem.IdContract}.pdf",
                DefaultExt = ".pdf"
            };
            var localizer = Localizer.Instance;
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    PdfDocument document = new PdfDocument();
                    document.Info.Title = $"Contract #{contractItem.IdContract}";

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont titleFont = new XFont("Times New Roman", 16, XFontStyleEx.Bold);
                    XFont bodyFont = new XFont("Times New Roman", 12, XFontStyleEx.Regular);

                    double y = 40;



                    gfx.DrawString($"{localizer["contract"]} #{contractItem.IdContract}", titleFont, XBrushes.Black, new XRect(0, y, page.Width, 30), XStringFormats.TopCenter);
                    y += 40;

                    gfx.DrawString($"{localizer["address"]}: {contractViewModel.Address}", bodyFont, XBrushes.Black, new XRect(40, y, page.Width - 80, 20), XStringFormats.TopLeft);
                    y += 30;

                    gfx.DrawString($"{localizer["sign_date"]}: {contractItem.SignDate:yyyy-MM-dd}", bodyFont, XBrushes.Black, new XRect(40, y, page.Width - 80, 20), XStringFormats.TopLeft);
                    y += 30;

                    gfx.DrawString($"{localizer["valid_for"]}: {contractItem.PeriodVazenja} {localizer["months"]}", bodyFont, XBrushes.Black, new XRect(40, y, page.Width - 80, 20), XStringFormats.TopLeft);
                    y += 40;

                    gfx.DrawString("Contract Content:", titleFont, XBrushes.Black, new XRect(40, y, page.Width - 80, 25), XStringFormats.TopLeft);
                    y += 30;

                    var tf = new XTextFormatter(gfx)
                    {
                        Alignment = XParagraphAlignment.Left
                    };
                    tf.DrawString(contractItem.Content, bodyFont, XBrushes.Black, new XRect(40, y, page.Width - 80, page.Height - y - 40));
                    document.Save(dialog.FileName);

                    System.Windows.MessageBox.Show(localizer["pdf_success"], localizer["success"], MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(localizer["pdf_error"], localizer["error"], MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ClearSearch()
        {
            SearchQuery = string.Empty;
        }
    }
}

