using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Windows.Input;
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
#pragma warning disable CS0219 // La variable 'isAuthentificate' est assignée, mais sa valeur n'est jamais utilisée
            bool isAuthentificate = false;
#pragma warning restore CS0219 // La variable 'isAuthentificate' est assignée, mais sa valeur n'est jamais utilisée
            if (Email != null && Password != null)
            {
                isAuthentificate = await ApiClient.ApiInstance.Authentification(Email, Password);
            }

            if (isAuthentificate)
            {
                await ApiClient.ApiInstance.GetUserSession();
                await _navigationService.Value.PushAsync<HomePage>();
            } 
            else
                await Application.Current.MainPage.DisplayAlert("Wrong Email or Password", "Please retry", "OK");            
        }

        public async void GotoRegisterAction()
        {
            await _navigationService.Value.PushAsync<InscriptionPage>();
        }
    }
}
