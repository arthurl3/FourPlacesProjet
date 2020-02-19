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
    public class UpdatePasswordPageViewModel : ViewModelBase
    {
        private readonly Lazy<INavigationService> _navigationService;

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private string _oldpassword;
        public string OldPassword
        {
            get => _oldpassword;
            set => SetProperty(ref _oldpassword, value);
        }

        private string _newpassword;
        public string NewPassword
        {
            get => _newpassword;
            set => SetProperty(ref _newpassword, value);
        }

        private string _confirmpassword;
        public string ConfirmPassword
        {
            get => _confirmpassword;
            set => SetProperty(ref _confirmpassword, value);
        }

        public ICommand ValidateUpdatePasswordCommand { get; }

        public UpdatePasswordPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());

            ValidateUpdatePasswordCommand = new Command(ValidateUpdatePasswordAction);
            PageName = "Edit Password";
        }

        public async void ValidateUpdatePasswordAction()
        {
            if (!ConfirmPassword.Equals(NewPassword))
                await Application.Current.MainPage.DisplayAlert("Error", "New passwords do not match", "OK");
            else
            {
                UpdatePasswordRequest Upd = new UpdatePasswordRequest(OldPassword, NewPassword);

                if (!await ApiClient.ApiInstance.UpdatePassword(Upd)) //Check old password and try to update
                    await Application.Current.MainPage.DisplayAlert("Error", "Wrong old password", "OK");
                else
                    await _navigationService.Value.PopAsync();
                    
            }
            
        }
    }
}
