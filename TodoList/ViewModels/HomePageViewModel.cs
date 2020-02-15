using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    class HomePageViewModel : ViewModelBase
    {
        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        public ICommand SeeDetailsCommand { get; }

        public HomePageViewModel()
        {
            SeeDetailsCommand = new Command(SeeDetailsAction);
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);

            PageName = "Home";
        }

        public async void SeeDetailsAction()
        {
            //await _navigationService.Value.PushAsync<HomePage>();
        }


    }
}
