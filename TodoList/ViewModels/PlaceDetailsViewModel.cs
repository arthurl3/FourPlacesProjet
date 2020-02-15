using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class PlaceDetailsViewModel : ViewModelBase
    {
        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        public PlaceDetailsViewModel()
        {
            
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);

            PageName = "Details";

        }
    }
}