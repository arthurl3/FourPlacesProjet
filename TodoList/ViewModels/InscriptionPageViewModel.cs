using Plugin.Media;
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
    public class InscriptionPageViewModel : ViewModelBase
    {
        private readonly Lazy<INavigationService> _navigationService;
        private ApiClient api;

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

        public ICommand RegisterCommand { get; }


        public InscriptionPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            api = ApiClient.ApiInstance;
            PageName = "Register";
            RegisterCommand = new Command(RegisterAction);
        }


        private async void RegisterAction()
        {
            //await api. //TODO
            await _navigationService.Value.PopAsync();
        }
    }
}