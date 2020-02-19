using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Windows.Input;
using TD.Api.Dtos;
using TodoList.Services;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class InscriptionPageViewModel : ViewModelBase
    {
        private readonly Lazy<INavigationService> _navigationService;
#pragma warning disable CS0169 // Le champ 'InscriptionPageViewModel.api' n'est jamais utilisé
        private ApiClient api;
#pragma warning restore CS0169 // Le champ 'InscriptionPageViewModel.api' n'est jamais utilisé

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

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmpassword;
        public string ConfirmPassword
        {
            get => _confirmpassword;
            set => SetProperty(ref _confirmpassword, value);
        }

        public ICommand RegisterCommand { get; }


        public InscriptionPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());

            PageName = "Register";
            RegisterCommand = new Command(RegisterAction);
        }


        private async void RegisterAction()
        {
            if (!ConfirmPassword.Equals(Password))
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match", "OK");
            else
            {
                RegisterRequest registerRequest = new RegisterRequest(Email, Firstname, Lastname, Password);
                await ApiClient.ApiInstance.CreateRegisterRequest(registerRequest);
                await _navigationService.Value.PopAsync();
            }
            
        }
    }
}