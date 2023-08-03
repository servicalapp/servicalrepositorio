using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using SQLite;
using System.Net.Http;
using Xamarin.CommunityToolkit.UI.Views;
using System.Diagnostics;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Picker = Xamarin.Forms.Picker;
using Plugin.Media;

namespace IkumaTransport.janelas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

	public partial class PaginaInicial : ContentPage
	{
        string meusdadosbd = "", meufone = "", meuidonline = "";
        public SQLiteConnection conn;
        public Registration regmodel;
        public PaginaInicial ()
		{
			InitializeComponent ();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");
           // data.Text = formattedDate;
            vermeusdadosAsync();
            buscaprofissoes();



        }
        public async Task vermeusdadosAsync()
        {
            var stocksStartingWithA = conn.Query<RegistraPerfil>("SELECT * FROM RegistraPerfil WHERE id=1");
            if (stocksStartingWithA == null || stocksStartingWithA.Count <= 0) { }
            else
            {
                foreach (var s in stocksStartingWithA)
                {
                    meusdadosbd = s.Nome;
                    meufone = s.Fone;
                    meuidonline = s.Idonline;
                }
               // nomeperfil.Text= "Olá, "+meusdadosbd+"!";
            }

            if (!meuidonline.Equals("")) {
                Uri uri = new Uri("http://192.168.1.13/servical/colaborador/profissoes.php");
                var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("buscameusdados", meuidonline)
          };

                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
                req.Content = new FormUrlEncodedContent(postData);
                HttpClient client = new HttpClient();
                var response = await client.SendAsync(req);
                var content = await response.Content.ReadAsStringAsync();
                content = "1<;>Zinga<;>Alexandre<;>943707073<;>https://img.freepik.com/vetores-gratis/auto-center-auto-repair-service-icone-de-engrenagem-com-ferramentas-e-carro-fundo-amarelo_1057-4800.jpg?w=740&t=st=1689967815~exp=1689968415~hmac=326a0491b9bb9e31576fd800cf50e800decd33541f8599b0282f6a2ca60370f0<;>";
                if (content.Contains("<;>"))
                {
                    content = content.Replace("<;>","|");
                    var separador = '|';
                    var array = content.Split(separador);

                    RegistraPerfil reg1 = new RegistraPerfil();
                    reg1.id = 1;
                    reg1.Idonline = array[0];
                    reg1.Nome = array[1];
                    reg1.Sobrenome = array[2];
                    reg1.Fone = array[3];
                    int x1 = 0;
                    try
                    { x1 = conn.Update(reg1); }
                    catch (Exception ex)
                    { throw ex; }
                    if (x1 == 1)
                    {}
                    //nomeperfil.Text = "Olá, " + array[1] + "!";
                   // if (!array[4].Equals("")) { foto.Source = array[4]; }
                } 
            }


            }

        string todasprofissoes = "",aprofissao="",apesquisar="";
        int idprofissao = 0;
        public async Task buscaprofissoes()
        {
           // asprofissoes.Children.Clear();
            todasprofissoes = "";

            var content = "";
            content = "Mecânica<,>https://img.freepik.com/vetores-gratis/auto-center-auto-repair-service-icone-de-engrenagem-com-ferramentas-e-carro-fundo-amarelo_1057-4800.jpg?w=740&t=st=1689967815~exp=1689968415~hmac=326a0491b9bb9e31576fd800cf50e800decd33541f8599b0282f6a2ca60370f0<,>1<,><;>";
            if (content.Contains("<;>"))
            {
                content = content.Replace("<;>", "|");
                var separador = '|';
                var array = content.Split(separador);

                for (int i = 0; i < array.Count(); i++)
                {
                    
                    string conteudo = array[i];
                    conteudo = conteudo.Replace("<,>", "|");
                    var array1 = conteudo.Split(separador);

                        Frame frame = new Frame();
                        frame.Margin = 20;
                        frame.CornerRadius = 30;
                        frame.BackgroundColor = Color.FromHex("#F1BF00");
                        frame.HeightRequest = 100;

                        StackLayout stackLayout = new StackLayout();
                        stackLayout.Orientation = StackOrientation.Vertical;
                        stackLayout.WidthRequest = 200;
                        stackLayout.HeightRequest = 100;

                        Label label = new Label();
                        label.Text = array1[0];
                        label.FontFamily = "NoirPro-Regular";
                        label.FontSize = 18;
                        label.TextColor = Color.White;
                        label.HorizontalOptions = LayoutOptions.Center;

                        Image image = new Image();
                        image.Source = array1[1];
                        image.WidthRequest = 80;
                        image.HeightRequest = 80;
                        image.HorizontalOptions = LayoutOptions.Center;

                        stackLayout.Children.Add(label);
                        stackLayout.Children.Add(image);
                        frame.Content = stackLayout;

                    if (array1[2].Contains(apesquisar)) {//asprofissoes.Children.Add(frame);
                        idprofissao = Convert.ToInt32(array1[2]);
                        aprofissao = array1[0];
                        todasprofissoes = todasprofissoes + array1[0]+"<;>";
                    }
                }
            }
                    /* Uri uri = new Uri("http://192.168.1.13/servical/colaborador/profissoes.php");
                     var postData = new List<KeyValuePair<string, string>>
                   {
                       new KeyValuePair<string, string>("buscaprofissoes", "")
                   };

                     HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
                     req.Content = new FormUrlEncodedContent(postData);
                     HttpClient client = new HttpClient();
                     var response = await client.SendAsync(req);
                     var content = await response.Content.ReadAsStringAsync();
                     content = "Mecânica<,>https://img.freepik.com/vetores-gratis/auto-center-auto-repair-service-icone-de-engrenagem-com-ferramentas-e-carro-fundo-amarelo_1057-4800.jpg?w=740&t=st=1689967815~exp=1689968415~hmac=326a0491b9bb9e31576fd800cf50e800decd33541f8599b0282f6a2ca60370f0<,><;>";
                     if (content.Contains("<;>"))
                     {
                         content = content.Replace("<;>", "|");
                         var separador = '|';
                         var array = content.Split(separador);

                         for(int i = 0; i < array.Count(); i++)
                         {
                             string conteudo = array[i];
                             conteudo = conteudo.Replace("<,>","|");
                             var array1 = conteudo.Split(separador);
                             Frame frame = new Frame();
                             frame.Margin = 20;
                             frame.CornerRadius = 30;
                             frame.BackgroundColor = Color.FromHex("#F1BF00");
                             frame.HeightRequest = 100;

                             StackLayout stackLayout = new StackLayout();
                             stackLayout.Orientation = StackOrientation.Vertical;
                             stackLayout.WidthRequest = 200;
                             stackLayout.HeightRequest = 100;

                             Label label = new Label();
                             label.Text = array1[0];
                             label.FontFamily = "NoirPro-Regular";
                             label.FontSize = 18;
                             label.TextColor = Color.White;
                             label.HorizontalOptions = LayoutOptions.Center;

                             Image image = new Image();
                             image.Source = array1[1];
                             image.WidthRequest = 80;
                             image.HeightRequest=80;
                             image.HorizontalOptions = LayoutOptions.Center;

                             stackLayout.Children.Add(label);
                             stackLayout.Children.Add(image);
                             frame.Content = stackLayout;

                             asprofissoes.Children.Add(frame);

                         }*/
                    buscaprofissionais();
                
           // }
        }



        int idprofissional = 0;
        public async Task buscaprofissionais()
        {
           // profissionais.Children.Clear();
            var content = "";
            content = "Antonio Lavosier<,>Enfermeiro<,>10000<,>1<,><;>";
            if (content.Contains("<;>"))
            {
                content = content.Replace("<;>", "|");
                var separador = '|';
                var array = content.Split(separador);

                for (int i = 0; i < array.Count(); i++)
                {

                    string conteudo = array[i];
                    conteudo = conteudo.Replace("<,>", "|");
                    var array1 = conteudo.Split(separador);
                    StackLayout stackLayout = new StackLayout();
                    stackLayout.Orientation = StackOrientation.Horizontal;
                    stackLayout.HorizontalOptions = LayoutOptions.FillAndExpand;

                    StackLayout stackLayout1 = new StackLayout();
                    stackLayout1.Orientation = StackOrientation.Vertical;
                    stackLayout1.HorizontalOptions = LayoutOptions.Start;
                    stackLayout1.Margin = 40;

                    Label label = new Label();
                    label.Text = array1[0];
                    label.FontSize = 18;
                    label.FontAttributes = FontAttributes.Bold;
                    label.FontFamily = "Noir";
                    label.TextColor = Color.FromHex("#1D5959");

                    Label label1 = new Label();
                    label1.Text = array1[1];
                    label1.FontSize = 15;
                    label1.FontFamily = "Noir";
                    label1.TextColor = Color.FromHex("#3B876E");

                    stackLayout1.Children.Add(label);
                    stackLayout1.Children.Add(label1);





                    StackLayout stackLayout2 = new StackLayout();
                    stackLayout2.Orientation = StackOrientation.Vertical;
                    stackLayout2.HorizontalOptions = LayoutOptions.End;
                    stackLayout2.Margin = 40;

                    Label label2 = new Label();
                    label2.Text = array1[2];
                    label2.FontSize = 18;
                    label2.FontAttributes = FontAttributes.Bold;
                    label2.FontFamily = "Noir";
                    label2.TextColor = Color.FromHex("#1D5959");

                    Label label3 = new Label();
                    label3.Text = "Por Hora";
                    label3.FontSize = 15;
                    label3.FontFamily = "Noir";
                    label3.TextColor = Color.FromHex("#3B876E");

                    stackLayout2.Children.Add(label2);
                    stackLayout2.Children.Add(label3);


                    stackLayout.Children.Add(stackLayout1);
                    stackLayout.Children.Add(stackLayout2);

                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => {
                        Navigation.PushModalAsync(new Perfil());
                    };
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);

                   // profissionais.Children.Add(stackLayout);
                    /*<StackLayout  Orientation="Horizontal" HorizontalOptions="FillAndExpand">
                                <StackLayout  Orientation="Vertical" HorizontalOptions="Start" Margin="40,0,40,0">
                                    <Label Text="Martins Francisco" FontSize="18" FontAttributes="Bold" FontFamily="Noir" TextColor="#1D5959"/>
                                <Label Text="Mecânico de Pesados" FontSize="15" FontFamily="Noir" TextColor="#3B876E"/>
                                </StackLayout>

                            <StackLayout  Orientation="Vertical" HorizontalOptions="End" Margin="40,0,40,0">
                                <Label Text="5.000.00 AOA" FontSize="18"  FontFamily="Noir" TextColor="#0B2B41" FontAttributes="Bold"/>
                                <Label Text="Por Hora" FontSize="15"  FontFamily="Noir" TextColor="#3B876E"/>
                                </StackLayout>
                            </StackLayout>*/

                }
            }
                    /*  Uri uri = new Uri("http://192.168.1.13/servical/colaborador/profissionais.php");
                      var postData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("buscaprofissionais", idprofissao.ToString())
                    };

                      HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
                      req.Content = new FormUrlEncodedContent(postData);
                      HttpClient client = new HttpClient();
                      var response = await client.SendAsync(req);
                      var content = await response.Content.ReadAsStringAsync();
                      if (content.Contains("<;>"))
                      {
                          content = content.Replace("<;>", "|");
                          var separador = '|';
                          var array = content.Split(separador);

                          for (int i = 0; i < array.Count(); i++)
                          {


                          }
                      }*/
                }


        public void irperfil(Object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Perfil());
        }


        public void pesquisar(Object sender, TextChangedEventArgs e)
        {
            buscaprofissoes();
        }


        int baixado = 0;
        public void baixaprofissionais(Object sender, EventArgs e)
        {
           // if (baixado == 0) { baixaprofissional.Margin=10; baixado = 1; } else { baixado = 0; baixaprofissional.Margin = 400; }
        }


       


    }
}