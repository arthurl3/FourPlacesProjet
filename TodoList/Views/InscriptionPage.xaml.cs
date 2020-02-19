using Storm.Mvvm.Forms;
using TodoList.ViewModels;
using Xamarin.Forms.Xaml;

namespace TodoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InscriptionPage : BaseContentPage
    {
        public InscriptionPage()
        {
            BindingContext = new InscriptionPageViewModel();
            InitializeComponent();
        }
    }
}