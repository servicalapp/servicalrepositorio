using SQLite;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Xamarin.Forms.Maps;

namespace IkumaTransport.janelas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        public SQLiteConnection conn;
        public Registration regmodel;
        public Cadastro()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();
            MyMap.IsVisible = false;
            apaga();
        }


        public void apaga()
        {
            cadastrocliente.IsVisible = false;
             cadastroprofissional.IsVisible = false;
             btncadastra.IsVisible = false;
        }
        public void ircliente(object sender, EventArgs e)
        {
            inicio.IsVisible = false;
            fiminicio.IsVisible = false;
            cadastrocliente.IsVisible = true;
        }

        public void irprofissional(object sender, EventArgs e)
        {
            inicio.IsVisible = false;
            fiminicio.IsVisible = false;
            cadastroprofissional.IsVisible = true;
            btncadastra.IsVisible = true;

            figura1.IsVisible = true;
            figura2.IsVisible = false;
            figura3.IsVisible = false;
            figura4.IsVisible = false;
            MyMap.IsVisible = false;
            textobtn.Text = "Avançar";
        }


        int estouacadastrar = 0;
        public void cadastraprofissional(object sender, EventArgs e)
        {
            if (estouacadastrar == 3)
            {
                mandarcolaboradorAsync();
            }
            if (estouacadastrar == 2)
            {
                figura1.IsVisible = false;
                figura2.IsVisible = false;
                figura3.IsVisible = false;
                figura4.IsVisible = true;
                estouacadastrar = 3;
                textobtn.Text = "Cadastrar";
            }
            if (estouacadastrar == 1)
            {
                figura1.IsVisible = false;
                figura2.IsVisible = false;
                figura3.IsVisible = true;
                figura4.IsVisible = false;
                estouacadastrar = 2;
                textobtn.Text = "Avançar";
            }
            if (estouacadastrar == 0){
                figura1.IsVisible = false;
                figura2.IsVisible = true;
                figura3.IsVisible = false;
                figura4.IsVisible = false;
                estouacadastrar = 1;
                textobtn.Text = "Avançar";
            }
            
        }

        public async Task mandarcolaboradorAsync()
        {
            Navigation.PushModalAsync(new PgInicial());
            /*Uri uri = new Uri("http://192.168.1.13/servical/colaborador/cadastro.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("cadastrarusuario", ""),
            new KeyValuePair<string, string>("fone", ""),  
            new KeyValuePair<string, string>("nome", nomeprofissional.Text.ToString()), 
            new KeyValuePair<string, string>("foto", ""),
            new KeyValuePair<string, string>("morada", morada.Text.ToString()),
            new KeyValuePair<string, string>("documentos", ""),
            new KeyValuePair<string, string>("nomecompleto", ""),  
            new KeyValuePair<string, string>("numerobi", ""),
            new KeyValuePair<string, string>("profissao", "baba"),  
            new KeyValuePair<string, string>("preco", preco.Text.ToString())
        };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);
            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            if (content.Contains("<;>"))
            {

                Registration reg = new Registration();
                reg.Dados = "2";
                int x = 0;
                try
                { x = conn.Insert(reg); }
                catch (Exception ex)
                { throw ex; }

                content = content.Replace("<;>", "|");
                var separador = '|';
                var array = content.Split(separador);
                RegistraPerfil reg1 = new RegistraPerfil();
                reg1.id = 1;
                reg1.Idonline = array[1];
                reg1.Nome = email.Text.ToString();
                reg1.Sobrenome = "colaborador";
                reg1.Fone = "";
                int x1 = 0;
                try
                { x1 = conn.Update(reg1); }
                catch (Exception ex)
                { throw ex; }
                if (x1 == 1)
                { Navigation.PushModalAsync(new PgInicial()); }

            }
            else
            {
                if (content.Contains("err")) { DisplayAlert("Senha incorreta!", "Senha Errada" + content, "Voltar"); }
                else { DisplayAlert("Upsi!", "Ocorreu um erro inesperado " + content, "Sair"); }
            }*/
        }

        public void cadastracliente(object sender, EventArgs e)
        {
            mandarclienteAsync();
        }

        public async Task mandarclienteAsync() 
        {
            
            /*Uri uri = new Uri("http://192.168.1.13/servical/cliente/cadastro.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("cadastrarusuario", ""),
            new KeyValuePair<string, string>("nome", email.Text.ToString()),
            new KeyValuePair<string, string>("contacto", ""),
            new KeyValuePair<string, string>("senha", senha.Text.ToString()),
            new KeyValuePair<string, string>("email", ""),
            new KeyValuePair<string, string>("idgmail", ""),
            new KeyValuePair<string, string>("tipogmailfb", "")
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);
            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            if (content.Contains("<;>")) {
               
                Registration reg = new Registration();
                reg.Dados = "2";
                int x = 0;
                try
                { x = conn.Insert(reg); }
                catch (Exception ex)
                { throw ex; }

                content = content.Replace("<;>","|");
                var separador = '|';
                var array= content.Split(separador);
                RegistraPerfil reg1 = new RegistraPerfil();
                reg1.id = 1;
                reg1.Idonline = array[1];
                reg1.Nome = email.Text.ToString();
                reg1.Sobrenome = "cliente";
                reg1.Fone = "";
                int x1 = 0;
                try
                { x1 = conn.Update(reg1); }
                catch (Exception ex)
                { throw ex; }
                if (x1 == 1)
                { Navigation.PushModalAsync(new PgInicial()); }
            
            }
            else {
                if (content.Contains("err")) { DisplayAlert("Senha incorreta!", "Senha Errada" + content, "Voltar"); }
                else { DisplayAlert("Upsi!", "Ocorreu um erro inesperado " + content, "Sair"); } }*/
        }

        int verpass=0;
        public void verpalavrapass(object sender, EventArgs e)
        {
            if (verpass == 0) { verpass = 1; imagemsenha.Source = "olhoaberto"; senha.IsPassword = false; } else { verpass = 0; imagemsenha.Source = "olhofechado"; senha.IsPassword = true; }
        }


        double minhalatitude = 0,minhalongitude=0;
        private async void clicknomapa(object sender, MapClickedEventArgs e)
        {
                minhalatitude = e.Position.Latitude;
                minhalongitude = e.Position.Longitude;
            MyMap.IsVisible = false;
            veroutroindereco();
        }


        public async Task veroutroindereco()
        {
            string minhalatitude1 = minhalatitude.ToString().Replace(",", ".");
            string minhalongitude1 = minhalongitude.ToString().Replace(",", ".");
            Uri uri = new Uri("https://maps.googleapis.com/maps/api/geocode/json?latlng=" + minhalatitude1 + "," + minhalongitude1 + "&result_type=street_address&key=AIzaSyDPkBS_v8gl-EC0MVjietLlqHz7bZS7PYw");
            var postData = new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("", "")
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            if (content.Contains("formatted_address"))
            {
                var separador = '|';
                string content1 = content.Replace("formatted_address", "|");
                var array = content1.Split('|');
                string array1 = array[1].Replace("\",", "|");
                array = array1.Split('|');
              /*  morada.Text = array[0];
                morada.Text = morada.Text.ToString().Replace("\"", "");
                morada.Text = morada.Text.ToString().Replace(":", "");*/
            }
            else
            {
                if (content.Contains("compound_code"))
                {
                    var separador = '|';
                    string content1 = content.Replace("compound_code", "|");
                    var array = content1.Split('|');
                    string array1 = array[1].Replace("\",", "|");
                    array = array1.Split('|');
                   /* morada.Text = array[0];
                    morada.Text = morada.Text.ToString().Replace("\"", "");
                   morada.Text = morada.Text.ToString().Replace(":", "");*/
                }
            }
        }


        public void abremapa(Object sender,EventArgs e)
        {
            MyMap.IsVisible = true;
        }


        protected override bool OnBackButtonPressed()
        {
            // Alguma lógica de tratamento
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return true;
        }
    }
}