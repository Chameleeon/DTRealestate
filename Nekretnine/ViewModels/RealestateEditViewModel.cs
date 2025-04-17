using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.IdentityModel.Tokens;
using Nekretnine.Events;
using Nekretnine.Localization;
using Nekretnine.Models;
using Nekretnine.Services;
using Newtonsoft.Json;

namespace Nekretnine.ViewModels
{
    public class RealestateEditViewModel : Screen
    {
        private readonly IRealestateService _realestateService;
        private readonly IEventAggregator _eventAggregator;
        private readonly Realestate _originalRealestate;

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        private string _squareFootage;
        public string SquareFootage
        {
            get => _squareFootage;
            set
            {
                _squareFootage = value;
                NotifyOfPropertyChange(() => SquareFootage);
            }
        }

        private List<string> _imagePaths = new List<string>();
        public List<string> ImagePaths
        {
            get => _imagePaths;
            set
            {
                _imagePaths = value;
                NotifyOfPropertyChange(() => ImagePaths);
            }
        }

        private ObservableCollection<ImagePath> _existingImages = new ObservableCollection<ImagePath>();
        public ObservableCollection<ImagePath> ExistingImages
        {
            get => _existingImages;
            set
            {
                _existingImages = value;
                NotifyOfPropertyChange(() => ExistingImages);
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

        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                NotifyOfPropertyChange(() => StatusMessage);
            }
        }

        public ObservableCollection<Realestatetype> RealestateTypeOptions { get; set; }

        private Realestatetype _selectedRealestateType;
        public Realestatetype SelectedRealestateType
        {
            get => _selectedRealestateType;
            set
            {
                _selectedRealestateType = value;
                NotifyOfPropertyChange(() => SelectedRealestateType);
            }
        }

        private Address _address;
        public Address Address
        {
            get => _address;
            set
            {
                _address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        public ICommand BrowseImagesCommand { get; }
        public ICommand RemoveImageCommand { get; }

        public RealestateEditViewModel(IEventAggregator eventAggregator, IRealestateService realestateService, Realestate realestate)
        {
            _eventAggregator = eventAggregator;
            _realestateService = realestateService;
            _originalRealestate = realestate;

            BrowseImagesCommand = new RelayCommand(_ => BrowseImages());
            RemoveImageCommand = new RelayCommand(RemoveImage);

            RealestateTypeOptions = new ObservableCollection<Realestatetype>();

            Description = realestate.Description;
            SquareFootage = realestate.SquareFootage;
            Address = realestate.AdresaIdAdresaNavigation ?? new Address();

            LoadExistingImages();
        }

        private void LoadExistingImages()
        {
            if (!string.IsNullOrEmpty(_originalRealestate.ImagePaths))
            {
                try
                {
                    var imageList = JsonConvert.DeserializeObject<List<ImagePath>>(_originalRealestate.ImagePaths);
                    if (imageList != null)
                    {
                        ExistingImages.Clear();
                        foreach (var image in imageList)
                        {
                            ExistingImages.Add(image);
                        }
                    }
                }
                catch
                {
                    // Handle deserialization errors silently
                    ExistingImages.Clear();
                }
            }
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var realestateTypes = await _realestateService.GetRealesTypeOptionsAsync();

            RealestateTypeOptions.Clear();
            foreach (var realestateType in realestateTypes)
            {
                RealestateTypeOptions.Add(realestateType);

                if (realestateType.IdRealesateType == _originalRealestate.TipNekretnineIdTipNekretnine)
                {
                    SelectedRealestateType = realestateType;
                }
            }
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
                SelectedImageNames = string.Join(", ", ImagePaths.Select(Path.GetFileName));
                NotifyOfPropertyChange(() => SelectedImageNames);
            }
        }

        public void RemoveImage(object parameter)
        {
            if (parameter is ImagePath imagePath)
            {
                ExistingImages.Remove(imagePath);
                NotifyOfPropertyChange(() => ExistingImages);
            }
        }

        public async Task SaveRealestate()
        {
            var localizer = Localizer.Instance;

            if (!Description.IsNullOrEmpty() &&
                !SquareFootage.IsNullOrEmpty() &&
                SelectedRealestateType != null)
            {
                if (!Regex.IsMatch(SquareFootage, @"^\+?\d{1,10}$"))
                {
                    StatusMessage = localizer["error_square_meterage"];
                    return;
                }

                try
                {
                    _originalRealestate.Description = Description;
                    _originalRealestate.SquareFootage = SquareFootage;
                    _originalRealestate.TipNekretnineIdTipNekretnine = SelectedRealestateType.IdRealesateType;
                    _originalRealestate.AdresaIdAdresaNavigation = Address;

                    List<ImagePath> finalImageList = new List<ImagePath>(ExistingImages);

                    if (ImagePaths.Count > 0)
                    {
                        string targetDirectory = Path.Combine("Images", _originalRealestate.IdRealestate.ToString());
                        if (!Directory.Exists(targetDirectory))
                        {
                            Directory.CreateDirectory(targetDirectory);
                        }

                        int order = finalImageList.Count > 0 ? finalImageList.Max(img => img.Order) + 1 : 1;
                        foreach (var file in ImagePaths)
                        {
                            string fileName = Path.GetFileName(file);
                            string targetFilePath = Path.Combine(targetDirectory, fileName);

                            if (!File.Exists(targetFilePath))
                            {
                                File.Copy(file, targetFilePath, true);
                            }

                            var imageData = new ImagePath
                            {
                                Path = targetFilePath,
                                IsPrimary = finalImageList.Count == 0 && order == 1,
                                Order = order
                            };
                            finalImageList.Add(imageData);
                            order++;
                        }
                    }

                    if (finalImageList.Count > 0 && !finalImageList.Any(img => img.IsPrimary))
                    {
                        finalImageList[0].IsPrimary = true;
                    }

                    _originalRealestate.ImagePaths = JsonConvert.SerializeObject(finalImageList);

                    await _realestateService.UpdateRealestateAsync(_originalRealestate);
                    StatusMessage = localizer["realestate_update_success"];
                    await _eventAggregator.PublishOnUIThreadAsync(new RealestateUpdatedEvent
                    {
                        RealestateId = _originalRealestate.IdRealestate
                    });
                    await TryCloseAsync(true);
                }
                catch (Exception ex)
                {
                    StatusMessage = localizer["realestate_update_error"];
                }
            }
            else
            {
                StatusMessage = localizer["mandatory_fields"];
            }
        }

        public async Task Cancel()
        {
            await TryCloseAsync(false);
        }
    }
}