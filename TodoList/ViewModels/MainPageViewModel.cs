using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TD.Api.Dtos;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly Lazy<INavigationService> _navigationService;
        private readonly Lazy<IDialogService> _dialogService;

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

       

        public ICommand ConnectionCommand { get; }

        public MainPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

            ConnectionCommand = new Command(ConnectionAction);

            PageName = "Connection";
        }

        public async void OnClickConnection()
        {
           // TodoService todoService = new TodoService();
            //List<PlaceItem> ListPlaces = await todoService.getPlaces();

            //HomePage secondPage = new HomePage();
            //secondPage.BindingContext = ListPlaces;
            await _navigationService.Value.PushAsync<HomePage>();
        }
        public async void ConnectionAction()
        {
            await _navigationService.Value.PushAsync<HomePage>();
        }
    }
}
