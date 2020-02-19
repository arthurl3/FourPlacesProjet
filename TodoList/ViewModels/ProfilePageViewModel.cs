using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.Api.Dtos;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
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

        private string _urlimage;
        public string UrlImage
        {
            get => _urlimage;
            set => SetProperty(ref _urlimage, value);
        }

        public ICommand UpdateProfileCommand { get; }
        public ICommand UpdatePasswordCommand { get; }

        public ProfilePageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

            UpdateProfileCommand = new Command(UpdateProfileAction);
            UpdatePasswordCommand = new Command(UpdatePasswordAction);
        }

        public override async Task OnResume()
        {
            //après avoir modifié le profil
            await base.OnResume();

            Reinit();
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            PageName = "Your Profile";
            Reinit();
        }

        private async void Reinit()
        {
            UserItem useritem = await ApiClient.ApiInstance.GetUserSession();
            Lastname = useritem.LastName;
            Firstname = useritem.FirstName;
            Email = useritem.Email;

            UrlImage = "https://td-api.julienmialon.com/images/" + useritem.ImageId;
        }


        public async void UpdateProfileAction()
        {
            await _navigationService.Value.PushAsync<UpdateProfilePage>();
        }

        public async void UpdatePasswordAction()
        {
            await _navigationService.Value.PushAsync<UpdatePasswordPage>();
        }
    }
}
