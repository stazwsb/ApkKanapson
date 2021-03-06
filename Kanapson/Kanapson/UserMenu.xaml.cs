﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kanapson
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMenu : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public UserMenu()
        {
            InitializeComponent();

            
        }

        

        private async void mycredit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AmountCredit());
        }

        private async void changePassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ChangePassword());
        }

        private async void myorders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Myorders());
        }

        private async void addorder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddOrder());
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Forms.Application.Current.Properties.Clear();
                await Navigation.PopModalAsync();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Błąd", ex.Message, "Ok");
            }
        }

        private  void Exit_Clicked(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("", "Czy chcesz wyjść z aplikacji?", "Tak", "Nie"))
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
