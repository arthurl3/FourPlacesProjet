using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TodoList.Models;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly Lazy<ITodoService> _todoService;
        private readonly Lazy<INavigationService> _navigationService;
        private readonly Lazy<IDialogService> _dialogService;

        public ObservableCollection<Todo> TodoList { get; }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

       

        public ICommand ConnectionCommand { get; }

        public MainPageViewModel()
        {
            _todoService = new Lazy<ITodoService>(() => DependencyService.Resolve<ITodoService>());
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

            TodoList = new ObservableCollection<Todo>();

            ConnectionCommand = new Command(ConnectionAction);

            PageName = "Connection";
        }

        public async void ConnectionAction()
        {
            await _navigationService.Value.PushAsync<HomePage>();
        }
    }
}
