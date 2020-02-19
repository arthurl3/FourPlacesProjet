using Storm.Mvvm.Forms;
using TodoList.ViewModels;
using Xamarin.Forms.Xaml;

namespace TodoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : BaseContentPage
    {
        public ProfilePage()
        {
            BindingContext = new ProfilePageViewModel();
            InitializeComponent();
        }
    }
}