using Storm.Mvvm.Forms;
using TodoList.ViewModels;
using Xamarin.Forms.Xaml;

namespace TodoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlacePage : BaseContentPage
    {
        public AddPlacePage()
        {
            BindingContext = new AddPlacePageViewModel();
            InitializeComponent();
        }
    }
}