using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TD.Api.Dtos;
using TodoList.Services;
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

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public ICommand ValidateCommand { get; }

        public AddPlacePageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            api = new ApiClient();
            PageName = "Add Place";
            ValidateCommand = new Command(ValidateAction);
        }

        private async void ValidateAction()
        {
            PlaceItem place = new PlaceItem(Title, Description, -1, 0, 0);

            await api.CreatePlace(place);
            await _navigationService.Value.PopAsync();
        }
    }
}