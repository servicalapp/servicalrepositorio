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
    public partial class IrSair : ContentPage
    {
        public IrSair()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Navigation.PushModalAsync(new Cadastro());
        }

        protected override bool OnBackButtonPressed()
        {
            // Alguma lógica de tratamento
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return true;
        }
    }
}