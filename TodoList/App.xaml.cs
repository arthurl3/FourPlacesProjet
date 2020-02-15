using Storm.Mvvm;
using TodoList.Services;
using TodoList.Views;
using Xamarin.Forms;

namespace TodoList
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new MainPage())
        {
            InitializeComponent();
            DependencyService.Register<ITodoService, TodoService>();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
