﻿using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public PlaceDetailsPageViewModel()
        {

        }

        public override async void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);
            PageName = PlaceItem.Title;
            Description = PlaceItem.Description;

            ApiClient api = new ApiClient();
            UrlImage = "https://td-api.julienmialon.com/images/" + PlaceItem.ImageId;
            ListComments = await api.GetCommentsPlace(PlaceItem.Id);

        }
    }
}