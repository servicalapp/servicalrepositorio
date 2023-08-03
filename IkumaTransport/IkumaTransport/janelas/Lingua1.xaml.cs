using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IkumaTransport.janelas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lingua1 : ContentPage
    {
        public SQLiteConnection conn;
        public RegistraLingua regmodel1;
        public Lingua1()
        {
            InitializeComponent();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<RegistraLingua>();
        }

        public void vermeusdados()
        {
            Navigation.PushModalAsync(new BoasVindas());
        }

        private void Irporingles(object sender, EventArgs e)
        {
            RegistraLingua reg0 = new RegistraLingua();
            reg0.lingua = "ENG";
            int x0 = 0;
            try
            { x0 = conn.Insert(reg0); }
            catch (Exception ex)
            { throw ex; }
            if (x0 == 1)
            { }
            else { }
            vermeusdados();
        }

        /*private void Irporarab(object sender, EventArgs e)
        {
            RegistraLingua reg0 = new RegistraLingua();
            reg0.lingua = "ARAB";
            int x0 = 0;
            try
            { x0 = conn.Insert(reg0); }
            catch (Exception ex)
            { throw ex; }
            if (x0 == 1)
            { }
            else { }
            vermeusdados();
        }*/

        private void Irporchines(object sender, EventArgs e)
        {
            RegistraLingua reg0 = new RegistraLingua();
            reg0.lingua = "CH";
            int x0 = 0;
            try
            { x0 = conn.Insert(reg0); }
            catch (Exception ex)
            { throw ex; }
            if (x0 == 1)
            { }
            else { }
            vermeusdados();
        }


        private void Irporespanhol(object sender, EventArgs e)
        { 
            RegistraLingua reg0 = new RegistraLingua();
            reg0.lingua = "ESP";
            int x0 = 0;
            try
            { x0 = conn.Insert(reg0); }
            catch (Exception ex)
            { throw ex; }
            if (x0 == 1)
            { }
            else { }
            vermeusdados();
        }

        private void Irporfrances(object sender, EventArgs e)
        {
            RegistraLingua reg0 = new RegistraLingua();
            reg0.lingua = "FR";
            int x0 = 0;
            try
            { x0 = conn.Insert(reg0); }
            catch (Exception ex)
            { throw ex; }
            if (x0 == 1)
            { }
            else { }

            vermeusdados();
        }

        private void Irportugues(object sender, EventArgs e)
        {

            RegistraLingua reg0 = new RegistraLingua();
            reg0.lingua = "PT";
            int x0 = 0;
            try
            { x0 = conn.Insert(reg0); }
            catch (Exception ex)
            { throw ex; }
            if (x0 == 1)
            { }
            else { }
            vermeusdados();
        }
    }
}