using IkumaTransport.janelas;
using Plugin.Connectivity;
using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IkumaTransport
{
    public partial class MainPage : ContentPage
    {
        public SQLiteConnection conn;
        public Registration regmodel;
        public string dadosbd = "";
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            //Navigation.PushModalAsync(new Cadastro());
            //Navigation.PushModalAsync(new BoasVindas());
            versetemdadosAsync();
        }

        public async Task versetemdadosAsync()
        {


            var stocksStartingWithA = conn.Query<Registration>("SELECT * FROM Registration ORDER BY id ASC");
            //DisplayAlert("Registration", stocksStartingWithA.ToString(), "Cancel");
            if (stocksStartingWithA == null)
            {
              //  calculatempoo();
            }
            else
            {
                if (stocksStartingWithA.Count <= 0)
                {
                  //  calculatempoo();
                }
                else
                {
                    foreach (var s in stocksStartingWithA)
                    {
                        dadosbd = s.Dados;
                    }
                }
                if (dadosbd == "" || dadosbd == "0")
                {
                    Navigation.PushModalAsync(new BoasVindas());
                }
                else
                {
                    if (dadosbd == "1")
                    {
                        Navigation.PushModalAsync(new Cadastro());
                    }

                    if (dadosbd == "2")
                    {
                        Navigation.PushModalAsync(new PgInicial());
                    }
                }
                if (dadosbd != "" && dadosbd != "0" && dadosbd != "1" && dadosbd != "2")
                {
                    Navigation.PushModalAsync(new BoasVindas());
                }
            }
            //DisplayAlert("Registration", "qualquer", "Cancel");
            // calculatempoo();


        }

    }
}
