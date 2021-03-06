﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kanapson
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminMenu : ContentPage
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private async void addorder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddorderAdmin());
        }

        private async void listorders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ListOrders());
        }

        

        private async void listproducts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ListProducts());
        }

        private async void adduser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterAdmin());
        }

        private async void updatecredit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UpdateCredit());
        }

        private async void changepassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ChangePassword());
        }

        

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Forms.Application.Current.Properties.Clear();
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", ex.Message, "Ok");
            }

        }

        private  void Exit_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                if(await DisplayAlert("","Czy chcesz wyjść z aplikacji?","Tak","Nie"))
                      Thread.CurrentThread.Abort();
            });
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("", "Czy chcesz wyjść z aplikacji?", "Tak", "Nie"))
                    Thread.CurrentThread.Abort();
            });
            return true;
        }
    }
}