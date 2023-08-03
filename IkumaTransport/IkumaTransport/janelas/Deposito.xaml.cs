using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using System.Net.Http;

namespace IkumaTransport.janelas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Deposito : ContentPage
    {
        string meuidonline = "", meufone = "", meusdadosbd = "";
        public SQLiteConnection conn;
        public Registration regmodel;
        public RegistraPerfil regmodel1;
        string antigosdados = "";
        public Deposito()
        {
            InitializeComponent();
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
            { meiopagamento.Title = "means of deposit"; }

            if (minhalingua.Equals("CH"))
            { meiopagamento.Title = "存款方式"; }

            if (minhalingua.Equals("ESP"))
            { meiopagamento.Title = "medios de deposito"; }

            if (minhalingua.Equals("FR"))
            { meiopagamento.Title = "moyen de dépôt"; }

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
                //calculatempoo();
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //envia sms
            if (!sms.Text.ToString().Equals(""))
            {
                enviasmsAsync();
            }
        }

        public async Task enviasmsAsync()
        {
            //sms.Text = "";
           /* Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("enviamensagem", ""),
              new KeyValuePair<string, string>("id", meuidonline),
              new KeyValuePair<string, string>("mensagem", sms.Text.ToString()),
              new KeyValuePair<string, string>("idmotorista", idescolhido),
              new KeyValuePair<string, string>("tipo", otipoescolhido)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            sms.Text = "";
            buscaagendamentos();*/
        }

        public async Task buscaagendamentos()
        {
            oschat.Children.Clear();
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("buscadepositos", meuidonline)
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
                    label.Text = "Operação nº "+arrayfromEntry2[0]+"\nAOS: " + arrayfromEntry2[8] +
                        "\nDebitado: "+arrayfromEntry2[1] +"\nDevido: " + arrayfromEntry2[2] +
                        "\nCreditado: "+arrayfromEntry2[3] +"\nEstado: " + arrayfromEntry2[6] +
                        "\n\n"+arrayfromEntry2[4];
                    if (minhalingua.Equals("ENG"))
                    {
                        label.Text = "Operation # " + arrayfromEntry2[0] + "\nAOS: " + arrayfromEntry2[8] +
                         "\nDebited: " + arrayfromEntry2[1] + "\nDue: " + arrayfromEntry2[2] +
                         "\nCredited: " + arrayfromEntry2[3] + "\nState: " + arrayfromEntry2[6] +
                         "\n\n" + arrayfromEntry2[4];
                    }

                    if (minhalingua.Equals("CH"))
                    {
                        label.Text = "操作# " + arrayfromEntry2[0] + "\nAOS: " + arrayfromEntry2[8] +
                         "\n借方：" + arrayfromEntry2[1] + "\n到期：" + arrayfromEntry2[2] +
                         "\n記入：" + arrayfromEntry2[3] + "\n狀態：" + arrayfromEntry2[6] +
                         "\n\n" + arrayfromEntry2[4];
                    }

                    if (minhalingua.Equals("ESP"))
                    {
                        label.Text = "Operación # " + arrayfromEntry2[0] + "\nAOS: " + arrayfromEntry2[8] +
                         "\nDebitado: " + arrayfromEntry2[1] + "\nDue: " + arrayfromEntry2[2] +
                         "\nAcreditado: " + arrayfromEntry2[3] + "\nState: " + arrayfromEntry2[6] +
                         "\n\n" + arrayfromEntry2[4];
                    }

                    if (minhalingua.Equals("FR"))
                    {
                        label.Text = "Operation # " + arrayfromEntry2[0] + "\nAOS : " + arrayfromEntry2[8] +
                         "\nDébité : " + arrayfromEntry2[1] + "\nDû : " + arrayfromEntry2[2] +
                         "\nCrédit : " + arrayfromEntry2[3] + "\nState : " + arrayfromEntry2[6] +
                         "\n\n" + arrayfromEntry2[4];
                    }

                    if (minhalingua.Equals("PT"))
                    { }
                    StackLayout stacklayout1 = new StackLayout();
                    stacklayout1.Children.Add(label);

                    StackLayout stackLayout2 = new StackLayout();
                    stackLayout2.Orientation = StackOrientation.Horizontal;
                    stackLayout2.Padding = 20;
                    stackLayout2.HorizontalOptions = LayoutOptions.Center;

                    stackLayout.Children.Add(stacklayout1);
                    var tapGestureRecognizer1 = new TapGestureRecognizer();
                    tapGestureRecognizer1.Tapped += async (s, e) =>
                    {
                        await Browser.OpenAsync(arrayfromEntry2[7], BrowserLaunchMode.SystemPreferred);
                    };
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer1);
                    frame.Content = stackLayout;
                    oschat.Children.Add(frame);
                }
            }

        }

        protected override bool OnBackButtonPressed()
        {
            // Alguma lógica de tratamento
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return true;
        }
    }
}