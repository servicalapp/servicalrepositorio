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
    public partial class Chat1 : ContentPage
    {
        string meuidonline = "", meufone = "", meusdadosbd = "";
        public SQLiteConnection conn;
        public Registration regmodel;
        public RegistraPerfil regmodel1;
        public string oid = "";
        string antigosdados = "";
        public Chat1()
        {
            InitializeComponent();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();

        }
        /*
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
            { }

            if (minhalingua.Equals("CH"))
            { }

            if (minhalingua.Equals("ESP"))
            { }

            if (minhalingua.Equals("FR"))
            { }

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
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
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
            buscaagendamentos();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //envia arquivo
        }

        string idescolhido = "-1", osid = "<,>", otipoescolhido = "";
        public async Task buscaagendamentos()
        {
            if (!oid.Equals("")) { idescolhido = oid; oid = ""; }
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("buscachat", meuidonline)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();

            if (!content.Equals("") && !content.Equals(antigosdados))
            {

                //antigosdados = content;
                oschat.Children.Clear();
                oschat1.Children.Clear();
                osid = "<,>";
                otipoescolhido = "";
                if (content.Contains("<;>"))
                {
                    var separador = '|';
                    content = content.ToString().Replace("<;>", "|");
                    var arrayfromEntry = content.ToString().Split(separador).ToList();

                    for (int i = 0; i < arrayfromEntry.Count - 1; i++)
                    {
                        if (arrayfromEntry[i].Contains("<,>"))
                        {
                            string content1 = arrayfromEntry[i].ToString().Replace("<,>", "|");
                            var arrayfromEntry1 = content1.ToString().Split(separador).ToList();

                            StackLayout stackLayout = new StackLayout();
                            stackLayout.Padding = 1;
                            stackLayout.Orientation = StackOrientation.Vertical;

                            Image image = new Image();
                            image.HorizontalOptions = LayoutOptions.Center;
                            image.Source = "perfil.png";
                            if (!arrayfromEntry1[3].Equals("")) { image.Source = arrayfromEntry1[3]; }
                            image.WidthRequest = 50;
                            image.HeightRequest = 50;
                            image.HorizontalOptions = LayoutOptions.Center;
                            image.VerticalOptions = LayoutOptions.Center;

                            Label label0 = new Label();
                            label0.FontSize = 20;
                            label0.FontFamily = "Candara";
                            label0.TextColor = Color.Black;
                            label0.Text = arrayfromEntry1[2];

                            Frame frames = new Frame();
                            frames.CornerRadius = 100;
                            frames.BackgroundColor = Color.FromHex("#FFC000");
                            frames.BorderColor = Color.FromHex("#FFC000");
                            frames.Padding = 0;
                            frames.IsClippedToBounds = true;


                            frames.Content = image;
                            stackLayout.Children.Add(frames);
                            stackLayout.Children.Add(label0);

                            var tapGestureRecognizer = new TapGestureRecognizer();
                            tapGestureRecognizer.Tapped += (s, e) => {
                                // handle the tap
                                idescolhido = arrayfromEntry1[4];
                                buscaagendamentos();
                            };
                            stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                            if (!osid.Contains("<,>" + arrayfromEntry1[4] + "<,>"))
                            {
                                osid = osid + arrayfromEntry1[4] + "<,>";
                                oschat.Children.Add(stackLayout);
                            }

                            if (arrayfromEntry1[4].Equals(idescolhido))
                            {
                                label0.TextColor = Color.Blue;
                                frames.BorderColor = Color.Blue;
                                Frame frame = new Frame();
                                frame.Margin = 20;
                                frame.CornerRadius = 5;
                                frame.BackgroundColor = Color.White;
                                frame.Padding = 10;
                                frame.BorderColor = Color.FromHex("#CC009CFF");
                                frame.IsClippedToBounds = true;

                                StackLayout stackLayout0 = new StackLayout();
                                Label label = new Label();
                                label.FontSize = 20;
                                label.FontFamily = "Candara";
                                label.TextColor = Color.Black;

                                Label label1 = new Label();
                                label1.FontSize = 15;
                                label1.FontFamily = "Candara";
                                label1.TextColor = Color.Black;

                                StackLayout stacklayout1 = new StackLayout();
                                stacklayout1.Children.Add(label);
                                label1.Text = arrayfromEntry1[6];
                                if (arrayfromEntry1[0].Equals(meuidonline))
                                {
                                    label1.Text = "eu: \n" + arrayfromEntry1[6];
                                    frame.BackgroundColor = Color.FromHex("#A6A6A6");
                                }
                                if (arrayfromEntry1[1].Contains("http"))
                                {

                                    var tapGestureRecognizer0 = new TapGestureRecognizer();
                                    tapGestureRecognizer0.Tapped += async (s, e) =>
                                    {
                                        await Browser.OpenAsync(arrayfromEntry1[1], BrowserLaunchMode.SystemPreferred);
                                    };
                                    stacklayout1.GestureRecognizers.Add(tapGestureRecognizer0);
                                    if (arrayfromEntry1[1].Contains(".JPG") || arrayfromEntry1[1].Contains(".GIF") ||
                                        arrayfromEntry1[1].Contains(".PNG") || arrayfromEntry1[1].Contains(".jpg") ||
                                        arrayfromEntry1[1].Contains(".gif") || arrayfromEntry1[1].Contains(".png"))
                                    {
                                        Image images = new Image();
                                        images.HorizontalOptions = LayoutOptions.Center;
                                        images.Source = "foto.png";
                                        images.WidthRequest = 50;
                                        images.HeightRequest = 50;
                                        stacklayout1.Children.Add(images);
                                        label.Text = "\nArquivo de imagem";
                                        if (minhalingua.Equals("ENG"))
                                        { label.Text = "\nImage file"; }

                                        if (minhalingua.Equals("CH"))
                                        { label.Text = "\n圖片文件"; }

                                        if (minhalingua.Equals("ESP"))
                                        { label.Text = "\nArchivo de imagen"; }

                                        if (minhalingua.Equals("FR"))
                                        { label.Text = "\nFichier image"; }

                                        if (minhalingua.Equals("PT"))
                                        { }
                                    }

                                    if (!arrayfromEntry1[1].Contains(".JPG") && !arrayfromEntry1[1].Contains(".GIF") &&
                                        !arrayfromEntry1[1].Contains(".PNG") && !arrayfromEntry1[1].Contains(".jpg") &&
                                        !arrayfromEntry1[1].Contains(".gif") && !arrayfromEntry1[1].Contains(".png") &&
                                        !arrayfromEntry1[1].Contains(".PDF") && !arrayfromEntry1[1].Contains(".txt") &&
                                        !arrayfromEntry1[1].Contains(".pdf") &&
                                        !arrayfromEntry1[1].Contains(".mp3") && !arrayfromEntry1[1].Contains(".mp4") &&
                                        !arrayfromEntry1[1].Contains(".avi") && !arrayfromEntry1[1].Contains(".MP3") &&
                                        !arrayfromEntry1[1].Contains(".MP4") && !arrayfromEntry1[1].Contains(".AVI"))
                                    {
                                        Image images = new Image();
                                        images.HorizontalOptions = LayoutOptions.Center;
                                        images.Source = "web.png";
                                        images.WidthRequest = 50;
                                        images.HeightRequest = 50;
                                        stacklayout1.Children.Add(images);
                                        label.Text = "\nArquivo web";
                                        if (minhalingua.Equals("ENG"))
                                        { label.Text = "\n Web File"; }

                                        if (minhalingua.Equals("CH"))
                                        { label.Text = "\n 網頁文件"; }

                                        if (minhalingua.Equals("ESP"))
                                        { label.Text = "\n archivo web"; }

                                        if (minhalingua.Equals("FR"))
                                        { label.Text = "\n Fichier Web"; }

                                        if (minhalingua.Equals("PT"))
                                        { }
                                    }

                                    if (arrayfromEntry1[1].Contains(".PDF") || arrayfromEntry1[1].Contains(".txt") ||
                                        arrayfromEntry1[1].Contains(".pdf"))
                                    {
                                        Image images = new Image();
                                        images.HorizontalOptions = LayoutOptions.Center;
                                        images.Source = "arquivo.png";
                                        images.WidthRequest = 50;
                                        images.HeightRequest = 50;
                                        stacklayout1.Children.Add(images);
                                        label.Text = "\nArquivo de texto";
                                        if (minhalingua.Equals("ENG"))
                                        { label.Text = "\n Text file"; }

                                        if (minhalingua.Equals("CH"))
                                        { label.Text = "\n 文本文件"; }

                                        if (minhalingua.Equals("ESP"))
                                        { label.Text = "\n archivo de texto"; }

                                        if (minhalingua.Equals("FR"))
                                        { label.Text = "\n Fichier texte"; }

                                        if (minhalingua.Equals("PT"))
                                        { }
                                    }

                                    if (arrayfromEntry1[1].Contains(".mp3") || arrayfromEntry1[1].Contains(".mp4") ||
                                        arrayfromEntry1[1].Contains(".avi") || arrayfromEntry1[1].Contains(".MP3") ||
                                        arrayfromEntry1[1].Contains(".MP4") || arrayfromEntry1[1].Contains(".AVI"))
                                    {
                                        Image images = new Image();
                                        images.HorizontalOptions = LayoutOptions.Center;
                                        images.Source = "video.png";
                                        images.WidthRequest = 50;
                                        images.HeightRequest = 50;
                                        stacklayout1.Children.Add(images);
                                        label.Text = "\n Arquivo de video";
                                        if (minhalingua.Equals("ENG"))
                                        { label.Text = "\n Video file"; }

                                        if (minhalingua.Equals("CH"))
                                        { label.Text = "\n 視頻文件"; }

                                        if (minhalingua.Equals("ESP"))
                                        { label.Text = "\n Archivo de video"; }

                                        if (minhalingua.Equals("FR"))
                                        { label.Text = "\n Fichier vidéo"; }

                                        if (minhalingua.Equals("PT"))
                                        { }
                                    }

                                }
                                else
                                {
                                    label.Text = arrayfromEntry1[1];
                                }

                                stackLayout0.Children.Add(stacklayout1);
                                frame.Content = stackLayout0;
                                oschat1.Children.Add(label1);
                                oschat1.Children.Add(frame);
                                otipoescolhido = arrayfromEntry1[5];
                            }
                        }
                    }
                }
            }

        }


        public void calculatempoo()
        {


           

        }

        protected override bool OnBackButtonPressed()
        {
            // Alguma lógica de tratamento
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return true;
        }*/
    }
}