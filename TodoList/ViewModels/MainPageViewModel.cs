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

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand ConnectionCommand { get; }
        public ICommand GotoRegisterCommand { get; }

        public MainPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

            ConnectionCommand = new Command(ConnectionAction);
            GotoRegisterCommand = new Command(GotoRegisterAction);

            PageName = "Connection";
        }

        public async void ConnectionAction()
        {
            bool isAuthentificate = false;
            if (Email != null && Password != null ) 
            {
                ApiClient api = ApiClient.ApiInstance;
                isAuthentificate = await api.Authentification(Email, Password);
            }

            if (isAuthentificate)
                await _navigationService.Value.PushAsync<HomePage>();
            else
                await Application.Current.MainPage.DisplayAlert("Wrong Email or Password", "Please retry", "OK");            
        }

        public async void GotoRegisterAction()
        {
            await _navigationService.Value.PushAsync<InscriptionPage>();
        }
    }
}
