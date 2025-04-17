using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using Caliburn.Micro;
using Nekretnine.Localization;
using Nekretnine.Models;
using Nekretnine.Services;
using Newtonsoft.Json;

namespace Nekretnine.ViewModels
{
    public class RealestateAddViewModel : Screen
    {
        private string _description;
        public string Description
        {
            get => _description;
            set => _description = value;
        }

        private string _squareFootage;
        public string SquareFootage
        {
            get => _squareFootage;
            set => _squareFootage = value;
        }

        private List<string> _imagePaths = new List<string>();
        public List<string> ImagePaths
        {
            get => _imagePaths;
            set
            {
                _imagePaths = value;
            }
        }


        private string _selectedImageNames;
        public string SelectedImageNames
        {
            get => _selectedImageNames;
            set
            {
                _selectedImageNames = value;
                NotifyOfPropertyChange(() => SelectedImageNames);
            }
        }

        public ObservableCollection<Realestatetype> RealestateTypeOptions { get; set; }
        private Realestatetype _selectedRealestateType;
        public Realestatetype SelectedRealestateType
        {
            get => _selectedRealestateType;
            set => _selectedRealestateType = value;
        }

        private Address _address = new Address();
        public Address Address
        {
            get => _address;
            set => _address = value;
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => _statusMessage = value;
        }

        private readonly IRealestateService _realestateService;

        public RealestateAddViewModel(IRealestateService realestateService)
        {
            _realestateService = realestateService;
            RealestateTypeOptions = new ObservableCollection<Realestatetype>();
            SelectedRealestateType = new Realestatetype();
            SelectedRealestateType.RealestateType1 = "Apartment";
            SelectedRealestateType.IdRealesateType = 1;
        }

        public void BrowseImages()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                ImagePaths.AddRange(dialog.FileNames);
                SelectedImageNames = string.Join(", ", dialog.FileNames.Select(Path.GetFileName));
            }
        }

        public async void SaveRealestate()
        {
            var localizer = Localizer.Instance;
            if (string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(SquareFootage) || SelectedRealestateType == null)
            {
                StatusMessage = localizer["mandatory_fields"];
                return;
            }

            if (!Regex.IsMatch(SquareFootage, @"^\+?\d{1,10}$"))
            {
                StatusMessage = localizer["error_square_meterage"];
                return;
            }

            var newRealestate = new Realestate
            {
                Description = Description,
                SquareFootage = SquareFootage,
                TipNekretnineIdTipNekretnineNavigation = SelectedRealestateType,
                AdresaIdAdresaNavigation = Address,
                ImagePaths = "[]"
            };


            try
            {
                bool isSaved = await _realestateService.AddRealestateAsync(newRealestate);

                if (isSaved)
                {
                    int realestateId = newRealestate.IdRealestate;

                    string targetDirectory = Path.Combine("Images", realestateId.ToString());
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    var imageDataList = new List<ImagePath>();

                    int order = 1;
                    foreach (var file in ImagePaths)
                    {
                        string fileName = Path.GetFileName(file);
                        string targetFilePath = Path.Combine(targetDirectory, fileName);

                        File.Copy(file, targetFilePath, true);

                        var imageData = new ImagePath
                        {
                            Path = targetFilePath,
                            IsPrimary = order == 1,
                            Order = order
                        };
                        imageDataList.Add(imageData);
                        order++;
                    }

                    newRealestate.ImagePaths = JsonConvert.SerializeObject(imageDataList);

                    await _realestateService.UpdateRealestateAsync(newRealestate);

                    await TryCloseAsync(true);
                }
                else
                {
                    StatusMessage = localizer["error_realestate"];
                }
            }
            catch (Exception ex)
            {
                StatusMessage = localizer["error_realestate"];
            }
        }

        public void Cancel()
        {
            StatusMessage = "Canceled.";
            TryCloseAsync(false);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var realestatetypes = await _realestateService.GetRealesTypeOptionsAsync();
            RealestateTypeOptions.Clear();
            foreach (var realtype in realestatetypes)
            {
                RealestateTypeOptions.Add(realtype);
            }
        }

    }
}
