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
using System.IO;

namespace IkumaTransport.janelas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Chat : ContentPage
    {
        string meuidonline = "", meufone = "", meusdadosbd = "";
        public SQLiteConnection conn;
        public Registration regmodel;
        public RegistraPerfil regmodel1;
        public string oid = "";
        string antigosdados = "";
        public Chat (string oid1)
		{
			InitializeComponent ();
            oid = oid1;
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();

        }

    }
}