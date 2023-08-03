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
    public partial class Lingua : ContentPage
    {
        public SQLiteConnection conn;
        public RegistraLingua regmodel1;
        public Lingua()
        {
            InitializeComponent();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<RegistraLingua>();
            vermeusdados();
        }

        public void vermeusdados()
        {
            string minhalingua = "PT";
            var stocksStartingWithA = conn.Query<RegistraLingua>("SELECT * FROM RegistraLingua WHERE id=1");
            if (stocksStartingWithA == null || stocksStartingWithA.Count <= 0) { }
            else
            {
                foreach (var s in stocksStartingWithA)
                {
                    minhalingua = s.lingua;
                }

                if (minhalingua.Equals("ENG"))
                {
                    pt.TextColor = Color.Black;
                    ch.TextColor = Color.Black;
                    esp.TextColor = Color.Black;
                    //arab.TextColor = Color.Black;
                    fr.TextColor = Color.Black;
                    eng.TextColor = Color.Green;
                }

                if (minhalingua.Equals("CH"))
                {
                    eng.TextColor = Color.Black;
                    pt.TextColor = Color.Black;
                    esp.TextColor = Color.Black;
                    //arab.TextColor = Color.Black;
                    fr.TextColor = Color.Black;
                    ch.TextColor = Color.Green;
                }

                if (minhalingua.Equals("ESP"))
                {
                    eng.TextColor = Color.Black;
                    ch.TextColor = Color.Black;
                    pt.TextColor = Color.Black;
                   // arab.TextColor = Color.Black;
                    fr.TextColor = Color.Black;
                    esp.TextColor = Color.Green;
                }

                if (minhalingua.Equals("ARAB"))
                {
                    eng.TextColor = Color.Black;
                    ch.TextColor = Color.Black;
                    esp.TextColor = Color.Black;
                    pt.TextColor = Color.Black;
                    fr.TextColor = Color.Black;
                    //arab.TextColor = Color.Green;
                }

                if (minhalingua.Equals("FR"))
                {
                    eng.TextColor = Color.Black;
                    ch.TextColor = Color.Black;
                    esp.TextColor = Color.Black;
                    //arab.TextColor = Color.Black;
                    pt.TextColor = Color.Black;
                    fr.TextColor = Color.Green;
                }

                if (minhalingua.Equals("PT"))
                {
                    eng.TextColor = Color.Black;
                    ch.TextColor = Color.Black;
                    esp.TextColor = Color.Black;
                    //arab.TextColor = Color.Black;
                    fr.TextColor = Color.Black;
                    pt.TextColor = Color.Green;
                }

            }
        }

        private void Irporingles(object sender, EventArgs e)
        {
            RegistraLingua reg1 = new RegistraLingua();
            reg1.id = 1;
            reg1.lingua = "ENG";
            int x1 = 0;
            try
            { x1 = conn.Update(reg1); }
            catch (Exception ex)
            { throw ex; }

            vermeusdados();
        }

       /* private void Irporarab(object sender, EventArgs e)
        {
            RegistraLingua reg1 = new RegistraLingua();
            reg1.id = 1;
            reg1.lingua = "ARAB";
            int x1 = 0;
            try
            { x1 = conn.Update(reg1); }
            catch (Exception ex)
            { throw ex; }

            vermeusdados();
        }*/

        private void Irporchines(object sender, EventArgs e)
        {
            RegistraLingua reg1 = new RegistraLingua();
            reg1.id = 1;
            reg1.lingua = "CH";
            int x1 = 0;
            try
            { x1 = conn.Update(reg1); }
            catch (Exception ex)
            { throw ex; }

            vermeusdados();
        }


        private void Irporespanhol(object sender, EventArgs e)
        {
            RegistraLingua reg1 = new RegistraLingua();
            reg1.id = 1;
            reg1.lingua = "ESP";
            int x1 = 0;
            try
            { x1 = conn.Update(reg1); }
            catch (Exception ex)
            { throw ex; }

            vermeusdados();
        }

        private void Irporfrances(object sender, EventArgs e)
        {
            RegistraLingua reg1 = new RegistraLingua();
            reg1.id = 1;
            reg1.lingua = "FR";
            int x1 = 0;
            try
            { x1 = conn.Update(reg1); }
            catch (Exception ex)
            { throw ex; }

            vermeusdados();
        }

        private void Irportugues(object sender, EventArgs e)
        {
            RegistraLingua reg1 = new RegistraLingua();
            reg1.id = 1;
            reg1.lingua = "PT";
            int x1 = 0;
            try
            { x1 = conn.Update(reg1); }
            catch (Exception ex)
            { throw ex; }

            vermeusdados();
        }
    }
}