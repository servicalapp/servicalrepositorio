using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using SQLite;
using static Xamarin.Essentials.Permissions;
using System.Net.Http;

namespace IkumaTransport.janelas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoasVindas : ContentPage
    {
        public SQLiteConnection conn;
        public Registration regmodel;
        public BoasVindas()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();

            Registrardados();
        }


        protected override bool OnBackButtonPressed()
        {
            // Alguma lógica de tratamento
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return true;
        }

        public void avanca(Object sender, EventArgs e)
        {
           // DisplayAlert("Registration", "Thanks for Registration", "ok");
            Navigation.PushModalAsync(new Cadastro());
           // Navigation.PushAsync(new Cadastro());
        }


    public void Registrardados()
    {
        Registration reg = new Registration();
        reg.Dados = "1";
        int x = 0;
        try
        {
            x = conn.Insert(reg);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        if (x == 1)
        {
            //DisplayAlert("Registration", "Thanks for Registration", "ok");
            // Navigation.PushModalAsync(new Cadastro());
        }
        else
        {
            //DisplayAlert("Registration Failled!!!", "Please try again", "ERROR");
        }





        RegistraPerfil reg1 = new RegistraPerfil();
        reg1.Idonline = "";
        reg1.Nome = "";
        reg1.Sobrenome = "";
        reg1.Fone = "";
        int x1 = 0;
        try
        { x1 = conn.Insert(reg1); }
        catch (Exception ex)
        { throw ex; }
        if (x1 == 1)
        { }
        else { }

    }

}
}