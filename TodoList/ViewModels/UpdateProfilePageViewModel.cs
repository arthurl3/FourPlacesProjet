using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TD.Api.Dtos;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class UpdateProfileDetailsPageViewModel : ViewModelBase
    {
        private readonly Lazy<INavigationService> _navigationService;
        private readonly Lazy<IDialogService> _dialogService;

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
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

        private int? _imageid;
        public int? ImageId
        {
            get => _imageid;
            set => SetProperty(ref _imageid, value);
        }
        public ICommand ValidateUpdateProfileCommand { get; }

        public UpdateProfileDetailsPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

            ValidateUpdateProfileCommand = new Command(UpdateProfileAction);
        }

        public override async void Initialize(Dictionary<string, object> navigationParameters)
        {
            PageName = "Edit Profile";

            base.Initialize(navigationParameters);

            UserItem useritem = await ApiClient.ApiInstance.GetUserSession();
            Lastname = useritem.LastName;
            Firstname = useritem.FirstName;
            //???TODO??? UserImage = useritem.ImageId;

        }

        public async void UpdateProfileAction()
        {
            UpdateProfileRequest updateProfileRequest = new UpdateProfileRequest(Firstname, Lastname, ImageId);

            await ApiClient.ApiInstance.UpdateProfile(updateProfileRequest);
            await _navigationService.Value.PopAsync();
        }
    }
}
