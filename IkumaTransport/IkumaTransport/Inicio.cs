using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace IkumaTransport
{
    public class Inicio : ContentPage
    {
       /* public Inicio()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }*/
       
        // Define a imagem a ser exibida
        // que deverá ser copiada para pasta
        // Resources/drawable do projeto Android
        Image splashImage;
        public Inicio()
        {
            //define que a navegação não possui barra
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                //define a imagem e seu tamanho
                Source = "centrosplash.gif",
                WidthRequest = 299,
                HeightRequest = 54
            };
            //define um AbsoluteLayout
            AbsoluteLayout.SetLayoutFlags(splashImage,
               AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
             new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);
            //define a cor de fundo
            this.BackgroundColor = Color.FromHex("#FFFF");
            this.Content = sub;
        }
        //executa ao iniciar a aplicação
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            /*await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.1, 1500, Easing.Linear);
            await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await splashImage.ScaleTo(10, 1500, Easing.Linear);
            // parte dois da animação  
            await splashImage.TranslateTo(0, 200, 1500, Easing.BounceIn);
            await splashImage.ScaleTo(2, 1500, Easing.CubicIn);
            await splashImage.RotateTo(360, 1500, Easing.SinInOut);
            await splashImage.ScaleTo(1, 1500, Easing.CubicOut);
            await splashImage.TranslateTo(0, -200, 1500, Easing.BounceOut);*/
            //carrega a pagina principal
            await splashImage.ScaleTo(0.1, 1500, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}