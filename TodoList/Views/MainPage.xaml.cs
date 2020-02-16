using System;
using System.Collections.Generic;
using System.ComponentModel;
using Storm.Mvvm.Forms;
using TD.Api.Dtos;
using TodoList.Services;
using TodoList.ViewModels;

namespace TodoList.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageViewModel();
            InitializeComponent();
        }


    }
}
