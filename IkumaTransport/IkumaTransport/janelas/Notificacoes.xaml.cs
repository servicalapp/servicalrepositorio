using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IkumaTransport.janelas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Notificacoes : ContentPage
    {
        string meuidonline = "", meufone = "", meusdadosbd = "";
        public SQLiteConnection conn;
        public Registration regmodel;
        public RegistraPerfil regmodel1;
        
        public Notificacoes ()
		{
			InitializeComponent ();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            vermeusdados();

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
            /*

            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("buscanotificacoes", meuidonline)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();

            asnotificacoes.Children.Clear();
            if (content.Contains("<;>"))
            {
                var separador = '|';
                content = content.ToString().Replace("<;>", "|");
                var arrayfromEntry = content.ToString().Split(separador).ToList();

                    for (int i = 0; i < arrayfromEntry.Count-1; i++)
                    {
                        
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
                        label.FontFamily = "Arial Rounded MT Bold";
                        label.TextColor = Color.Black;
                        label.Text = arrayfromEntry[i];

                        StackLayout stacklayout1 = new StackLayout();
                        stacklayout1.Children.Add(label);

                        stackLayout.Children.Add(stacklayout1);
                        frame.Content = stackLayout;

                    asnotificacoes.Children.Add(frame);
                    }
                }*/

            }
        



    }
}