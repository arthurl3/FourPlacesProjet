using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ICommand ValidateCommand { get; }
        public ICommand GetLocationCommand { get; }
        

        public AddPlacePageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            api = ApiClient.ApiInstance;
            PageName = "Add Place";
            ValidateCommand = new Command(ValidateAction);
            GetLocationCommand = new Command(GetLocationAction);
        }

        private async void OpenCameraAction(object sender, EventArgs e)
        {
            //var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            //if (photo != null)
            //    PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });


            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Camera not found", "OK");
            //    return;
            //}

            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            //    Directory = "Sample",
            //    Name = "test.jpg"
            //});

            //if (file == null)
            //    return;

            //await DisplayAlert("File Location", file.Path, "OK");

            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();
            //    return stream;
            //});
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
            PlaceItem place = new PlaceItem(Title, Description, 2, Convert.ToDouble(Latitude), Convert.ToDouble(Longitude));

            //await api.CreatePlace(place); //TODO décommenter
            await _navigationService.Value.PopAsync();
        }
    }
}