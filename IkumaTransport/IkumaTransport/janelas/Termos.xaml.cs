using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IkumaTransport.janelas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Termos : ContentView
	{
		public Termos ()
		{
			InitializeComponent ();
			abrirlinkAsync();

        }

		public async Task abrirlinkAsync()
		{
            await Browser.OpenAsync("https://ikumatransport.mypressonline.com/taxi/Termos%20de%20uso%20Ikuma%20Transport%20usuario.pdf", BrowserLaunchMode.SystemPreferred);

        }
	}
}