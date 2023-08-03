using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace IkumaTransport.janelas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Extrato : ContentPage
    {
        string meuidonline = "", meufone = "", meusdadosbd = "";
        public SQLiteConnection conn;
        public Registration regmodel;
        public RegistraPerfil regmodel1;
        public Extrato ()
		{
			InitializeComponent ();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();
        }

    }
}