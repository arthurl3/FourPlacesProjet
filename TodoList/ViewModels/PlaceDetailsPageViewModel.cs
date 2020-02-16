using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace TodoList.ViewModels
{
    public class PlaceDetailsPageViewModel : ViewModelBase
    {
        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        [NavigationParameter("PlaceItem")]
        public PlaceItem PlaceItem { get; set; }


        public PlaceDetailsPageViewModel()
        {

        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            PageName = PlaceItem.Title;
            Description = PlaceItem.Description;

        }
    }
}