using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Net.Http;
using Xamarin.Essentials;

namespace IkumaTransport.janelas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ofertas : ContentPage
	{
        string meuidonline = "", meufone = "", meusdadosbd = "";
        public SQLiteConnection conn;
        public Registration regmodel;
        public RegistraPerfil regmodel1;
        public Ofertas ()
		{
			InitializeComponent ();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();
            verlingua();
            vermeusdados();
        }
        string minhalingua = "PT";
        public void verlingua()
        {

            var stocksStartingWithA = conn.Query<RegistraLingua>("SELECT * FROM RegistraLingua WHERE id=1");
            if (stocksStartingWithA == null || stocksStartingWithA.Count <= 0) { }
            else
            {
                foreach (var s in stocksStartingWithA)
                {
                    minhalingua = s.lingua;
                }

            }

            if (minhalingua.Equals("ENG"))
            {}

            if (minhalingua.Equals("CH"))
            {}

            if (minhalingua.Equals("ESP"))
            {}


            if (minhalingua.Equals("FR"))
            {}

            if (minhalingua.Equals("PT"))
            { }
        }


        public void vermeusdados()
        {
            var stocksStartingWithA = conn.Query<RegistraPerfil>("SELECT * FROM RegistraPerfil WHERE id=1");
            if (stocksStartingWithA == null || stocksStartingWithA.Count <= 0) { }
            else
            {
                foreach (var s in stocksStartingWithA)
                {
                    meusdadosbd = s.Nome + s.Sobrenome;
                    meufone = s.Fone;
                    meuidonline = s.Idonline;
                }

                if (meusdadosbd != null || !meusdadosbd.Equals(""))
                {
                    verestadoonlineAsync();
                }

            }
        }

        public async Task verestadoonlineAsync()
        {
            string olink = "verperfil", opost = meuidonline;
            if (meuidonline.Equals("")) { olink = "verperfilfone"; opost = meufone; }
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>(olink, ""),
              new KeyValuePair<string, string>("id", opost)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            if (content.ToString().Contains("<;>"))
            {


                var separador = '|';
                content = content.ToString().Replace("<;>", "|");
                var arrayfromEntry = content.ToString().Split(separador).ToList();
                meuidonline = arrayfromEntry[0];
                meusdadosbd = arrayfromEntry[1] + " " + arrayfromEntry[2];
                meufone = arrayfromEntry[3];

                buscaagendamentos();
            }
        }

        public async Task buscaagendamentos()
        {
            asofertas.Children.Clear();
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("buscaoferta", meuidonline)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();


            if (content.Contains("<;>"))
            {
                var separador = '|';
                content = content.ToString().Replace("<;>", "|");
                var arrayfromEntry = content.ToString().Split(separador).ToList();

                    for (int i = 0; i < arrayfromEntry.Count - 1; i++)
                    {
                        string content2 = arrayfromEntry[i].ToString().Replace("<,>", "|");
                        var arrayfromEntry2 = content2.ToString().Split(separador).ToList();

                        Frame frame = new Frame();
                        frame.Margin = 20;
                        frame.CornerRadius = 5;
                        frame.BackgroundColor = Color.White;
                        frame.Padding = 10;
                        frame.BorderColor = Color.FromHex("#CC009CFF");
                        frame.IsClippedToBounds = true;

                        StackLayout stackLayout = new StackLayout();
                        Label label = new Label();
                        label.FontSize = 20;
                        label.FontFamily = "Candara";
                        label.TextColor = Color.Black;
                        label.Text = arrayfromEntry2[2];

                        StackLayout stacklayout1 = new StackLayout();
                        stacklayout1.Children.Add(label);

                        StackLayout stackLayout2 = new StackLayout();
                        stackLayout2.Orientation = StackOrientation.Horizontal;
                        stackLayout2.Padding = 20;
                        stackLayout2.HorizontalOptions = LayoutOptions.Center;

                        StackLayout stackLayout3 = new StackLayout();
                        Image image = new Image();
                        image.Source = "aceitar.png";
                        image.WidthRequest = 30;
                        image.HeightRequest = 30;
                        stackLayout3.Children.Add(image);
                        var tapGestureRecognizer1 = new TapGestureRecognizer();
                        tapGestureRecognizer1.Tapped += async (s, e) =>
                        {
                            Uri uri1 = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
                            var postData1 = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("aceitaoferta", meuidonline),
              new KeyValuePair<string, string>("idoferta", arrayfromEntry2[0])
          };

                            HttpRequestMessage req1 = new HttpRequestMessage(HttpMethod.Post, uri1);
                            req1.Content = new FormUrlEncodedContent(postData1);
                            HttpClient client1 = new HttpClient();
                            var response1 = await client1.SendAsync(req1);
                            var contents = await response1.Content.ReadAsStringAsync();
                            
                            if (minhalingua.Equals("ENG"))
                            { DisplayAlert("Adhesion to the offer", "You joined the offer!", "Ok"); }

                            if (minhalingua.Equals("CH"))
                            { DisplayAlert("堅持要約","你加入了要約！","好的"); }

                            if (minhalingua.Equals("ESP"))
                            { DisplayAlert("Adhesión a la oferta", "¡Te uniste a la oferta!", "Ok"); }


                            if (minhalingua.Equals("FR"))
                            { DisplayAlert("Adhésion à l'offre", "Vous avez rejoint l'offre !", "Ok"); }

                            if (minhalingua.Equals("PT"))
                            { DisplayAlert("Adesão a oferta", "Aderiste a oferta!", "Ok"); }
                            buscaagendamentos();
                        };
                        stackLayout3.GestureRecognizers.Add(tapGestureRecognizer1);

                        

                        stackLayout2.Children.Add(stackLayout3);

                        stackLayout.Children.Add(stacklayout1);
                    if (!arrayfromEntry2[1].Contains("<:>"+meuidonline+"<:>")) { stackLayout.Children.Add(stackLayout2); }
                    frame.Content = stackLayout;

                        asofertas.Children.Add(frame);
                    }
                }
        }


    }
}