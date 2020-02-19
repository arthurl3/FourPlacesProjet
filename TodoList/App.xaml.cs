using Storm.Mvvm;
using TodoList.Views;

namespace TodoList
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new MainPage())
        {
            InitializeComponent();
            //DependencyService.Register<ITodoService, TodoService>();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
