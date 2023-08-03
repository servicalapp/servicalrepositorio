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
    public partial class SemNet : ContentPage
    {
        public SemNet()
        {
            InitializeComponent();
        }

        private void Avanca(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }
    }
}