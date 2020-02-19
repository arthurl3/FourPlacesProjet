using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using TD.Api.Dtos;
using TodoList.Services;
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

        private List<CommentItem> _listComments;
        public List<CommentItem> ListComments
        {
            get => _listComments;
            set => SetProperty(ref _listComments, value);
        }

        private string _urlimage;
        public string UrlImage
        {
            get => _urlimage;
            set => SetProperty(ref _urlimage, value);
        }

        public ICommand OpenCommentCommand { get; }

        public PlaceDetailsPageViewModel()
        {
            OpenCommentCommand = new Command(OpenCommentAction);
        }

        public override async void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            PageName = PlaceItem.Title;
            Description = PlaceItem.Description;

            UrlImage = "https://td-api.julienmialon.com/images/" + PlaceItem.ImageId;
            ListComments = await ApiClient.ApiInstance.GetCommentsPlace(PlaceItem.Id);

        }

        public async void OpenCommentAction()
        {
            string comment = await Application.Current.MainPage.DisplayPromptAsync("Comment", "Post a comment");
            if (comment != null)
            {
                await ApiClient.ApiInstance.CreateComment(PlaceItem.Id, new CreateCommentRequest(comment));
                ListComments = await ApiClient.ApiInstance.GetCommentsPlace(PlaceItem.Id); //refresh
            }
        }

    }
}