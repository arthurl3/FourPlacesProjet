using Storm.Mvvm.Forms;
using TodoList.ViewModels;
using Xamarin.Forms.Xaml;

namespace TodoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfilePage : BaseContentPage
    {
        public UpdateProfilePage()
        {
            BindingContext = new UpdateProfileDetailsPageViewModel();
            InitializeComponent();
        }
    }
}