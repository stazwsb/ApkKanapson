﻿using Kanapson.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kanapson
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AmountCredit : ContentPage
    {
        private HttpClient client;
        private JwtSecurityTokenHandler jwtHandler;
        private string jwtPayload;
        string urlUser = "https://kanapson.pl/users/findbyid/";
        private User user;

        public AmountCredit()
        {
            InitializeComponent();
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            jwtHandler = new JwtSecurityTokenHandler();
            client.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", Xamarin.Forms.Application.Current.Properties["Token"] as string);
            if (!jwtHandler.CanReadToken(Xamarin.Forms.Application.Current.Properties["Token"].ToString())) throw new Exception("The token doesn't seem to be in a proper JWT format.");

            var token = jwtHandler.ReadJwtToken(Xamarin.Forms.Application.Current.Properties["Token"].ToString());
            jwtPayload = token.Claims.First(c => c.Type == "unique_name").Value;
            getCredit(jwtPayload);
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void getCredit(string id)
        {
            user = new User();
            try
            {
                var response = await client.GetAsync(urlUser + id);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var resault = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(resault);
                    Credit.Text = user.Credit + " zł";


                }
                else
                {
                    await DisplayAlert("Błąd", JObject.Parse(response.Content.ReadAsStringAsync().Result)["message"].ToString(), "Ok");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", ex.Message, "Ok");
            }
        }
    }
}