using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.Api.Dtos;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    class HomePageViewModel : ViewModelBase
    {
        private readonly Lazy<INavigationService> _navigationService;

        private List<PlaceItem> _listPlaces;
        public List<PlaceItem> ListPlaces
        {
            get => _listPlaces;
            set => SetProperty(ref _listPlaces, value);
        }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private PlaceItem _selectedPlaceItem;
        public PlaceItem SelectedPlaceItem
        {
            get => _selectedPlaceItem;
            set
            {
                if (SetProperty(ref _selectedPlaceItem, value) && value != null)
                {
                    SeeDetailsAction(value);
                    SelectedPlaceItem = null;
                }
            }
        }

        public ICommand SeeDetailsCommand { get; }
        public ICommand GotoCreatePlaceCommand { get; }

        public HomePageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());

            SeeDetailsCommand = new Command<PlaceItem>(SeeDetailsAction);
            GotoCreatePlaceCommand = new Command(GotoCreatePlaceAction);
        }


        public override async void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            ApiClient api = new ApiClient();

            PageName = "Home";
            ListPlaces = await api.GetPlaces();
        }

        public async void SeeDetailsAction(PlaceItem placeItem)
        {
            await _navigationService.Value.PushAsync<PlaceDetailsPage>(new System.Collections.Generic.Dictionary<string, object>
            {
                {"PlaceItem", placeItem}
            });
        }

        public async void GotoCreatePlaceAction()
        {
            await _navigationService.Value.PushAsync<AddPlacePage>();
        }
    }
}
