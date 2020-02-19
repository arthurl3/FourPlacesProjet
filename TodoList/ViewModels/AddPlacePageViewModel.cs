using Plugin.Media;
using Plugin.Media.Abstractions;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Windows.Input;
using TD.Api.Dtos;
using TodoList.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class AddPlacePageViewModel : ViewModelBase
    {
        private readonly Lazy<INavigationService> _navigationService;
        private ApiClient api;

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private ImageSource _photo;
        public ImageSource Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }

        private string _latitude;
        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private string _longitude;
        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private MediaFile _file;

        public ICommand ValidateCommand { get; }
        public ICommand GetLocationCommand { get; }
        public ICommand OpenCameraCommand { get; }


        public AddPlacePageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            api = ApiClient.ApiInstance;
            PageName = "Add Place";
            ValidateCommand = new Command(ValidateAction);
            GetLocationCommand = new Command(GetLocationAction);
            OpenCameraCommand = new Command(OpenCameraAction);

        }


        private async void OpenCameraAction()
        {
            /*            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

                        if (photo != null)
                            Photo.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    */
            await CrossMedia.Current.Initialize();

            _file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (_file != null)
                Photo = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();

                    return stream;
                });
            else
                await Application.Current.MainPage.DisplayAlert("Error", "Photo not found", "OK");

        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    Latitude = location.Latitude.ToString();
                    Longitude = location.Longitude.ToString();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Location not found", "OK");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private async void ValidateAction()
        {
            //Check informations
            if (Title == null || Title.Equals("") || Description == null || Description.Equals(""))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Incomplete informations", "OK");
                return;
            }

            PlaceItem place = new PlaceItem(Title, Description, 2, Convert.ToDouble(Latitude), Convert.ToDouble(Longitude));
            if (_file != null)
            {
                //On envoie la photo
                byte[] imageArray = System.IO.File.ReadAllBytes(_file.Path);
                int idImage = await api.SendImage(imageArray);
                if (idImage!=-1)
                {
                    //On cree le lieu
                    await ApiClient.ApiInstance.CreatePlace(place);
                    await _navigationService.Value.PopAsync();
                }
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a picture", "OK");
        }
    }
}