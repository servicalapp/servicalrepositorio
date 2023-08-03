using IkumaTransport.MenuItems;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IkumaTransport.janelas
{
    public partial class PgInicial : MasterDetailPage
    {
        public List<MasterPageItem> menuList
        {
            get;
            set;
        }
        string meusdadosbd = "", meufone="",meuidonline="";
        public SQLiteConnection conn;
        public Registration regmodel; 
        public RegistraPerfil regmodel1;
        string titulo0 = "Termos de uso";
        string titulo1 = "Politícas de privacidade";
        string titulo2 = "Sair";
        string titulo3 = "Depósitos";
        string titulo4 = "Língua";
        string titulo5 = "Agendamentos";
        string titulo6 = "Suporte";
        string titulo7 = "Chat";
        string titulo8 = "Ganhos e Estatísticas";
        string titulo9 = "Ofertas";
        string titulo10 = "Início";
        public PgInicial()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();
            verlingua();
            vermeusdados();
            //verestadoonlineAsync();


            NavigationPage.SetHasNavigationBar(this, false);
            menuList = new List<MasterPageItem>();
            // Adding menu items to menuList and you can define title ,page and icon
           /* menuList.Add(new MasterPageItem()
            {
                Title = titulo10,
                Icon = "inicio.png",
                TargetType = typeof(PaginaInicial)

            });
            menuList.Add(new MasterPageItem()
            {
                Title = titulo9,
                Icon = "oferta.png",
                TargetType = typeof(Ofertas)

            });
            menuList.Add(new MasterPageItem()
            {
                Title = titulo8,
                Icon = "estrato.png",
                TargetType = typeof(Extrato)

            });
            menuList.Add(new MasterPageItem()
            {
                Title = titulo7,
                Icon = "chat1.png",
                TargetType = typeof(Chat1)

            });*/
            menuList.Add(new MasterPageItem()
            {
                Title = titulo6,
                Icon = "notificacoes.png",
                TargetType = typeof(Chat)

            });
          /*  menuList.Add(new MasterPageItem()
            {
                Title = titulo5,
                Icon = "agenda.png",
                TargetType = typeof(Agendamentos)

            });
          

            menuList.Add(new MasterPageItem()
            {
                Title = titulo4,
                Icon = "estrato.png",
                TargetType = typeof(Lingua)

            });
            menuList.Add(new MasterPageItem()
            {
                Title = titulo3,
                Icon = "estrato.png",
                TargetType = typeof(Deposito)

            });*/
            menuList.Add(new MasterPageItem()
            {
                Title = titulo2,
                Icon = "sair.png",
                TargetType = typeof(IrSair)

            });
            menuList.Add(new MasterPageItem()
            {
                Title = titulo1,
                //Icon = "carro.png",
                TargetType = typeof(Politica)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = titulo0,
                //Icon = "carro.png",
                TargetType = typeof(Termos)
            });
            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page  
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PaginaInicial)));
        }
        // Event for Menu Item selection, here we are going to handle navigation based  
        // on user selection in menu ListView  
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }

        public void IrPerfil(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new Perfil());
            // Navigation.PushAsync(new Perfil());
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Perfil)));
        }

        public void vermeusdados()
        {
            var stocksStartingWithA = conn.Query<RegistraPerfil>("SELECT * FROM RegistraPerfil WHERE id=1");
            if (stocksStartingWithA == null || stocksStartingWithA.Count <= 0) { }
            else
            {
                foreach (var s in stocksStartingWithA)
                {
                    meusdadosbd = s.Nome + s.Sobrenome;
                    meufone = s.Fone;
                    meuidonline = s.Idonline;
                }

                if (meusdadosbd != null || !meusdadosbd.Equals(""))
                {
                    verestadoonlineAsync();
                }

            }
        }


        public async Task verestadoonlineAsync()
        {
           /* string olink = "verperfil",opost=meuidonline;
            if (meuidonline.Equals("")) { olink = "verperfilfone";opost = meufone; }*/
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("verperfilfone", ""),
              new KeyValuePair<string, string>("id", meufone)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            //DisplayAlert("Registration", meufone, "Cancel");
            if (content.ToString().Contains("<;>"))
            {
               

                var separador = '|';
                content = content.ToString().Replace("<;>", "|");
                var arrayfromEntry = content.ToString().Split(separador).ToList();
                if (meuidonline == "") {
                    RegistraPerfil reg1 = new RegistraPerfil();
                    reg1.Idonline = arrayfromEntry[0];
                    int x1 = 0;
                    try
                    { x1 = conn.Insert(reg1); }
                    catch (Exception ex)
                    { throw ex; }
                }
                meuidonline = arrayfromEntry[0];
                meusdadosbd = arrayfromEntry[1] + " "+arrayfromEntry[2]+"\n"+
                    arrayfromEntry[8]+" AOA";
                nomeperfil.Text = meusdadosbd;
                meufone = arrayfromEntry[3];
                if (!arrayfromEntry[4].Equals("")) { fotoperfil.Source = arrayfromEntry[4]; }
                //DisplayAlert("Registration", content.ToString()+"-"+arrayfromEntry[4], "Cancel");
                if (arrayfromEntry[10].Equals("suspenso")) { nomeperfil.Text="Perfil "+ arrayfromEntry[10]; }
                
            }

            
        }




        string minhalingua = "PT";
        public void verlingua()
        {

            var stocksStartingWithA = conn.Query<RegistraLingua>("SELECT * FROM RegistraLingua WHERE id=1");
            if (stocksStartingWithA == null || stocksStartingWithA.Count <= 0) { }
            else
            {
                foreach (var s in stocksStartingWithA)
                {
                    minhalingua = s.lingua;
                }

            }

            if (minhalingua.Equals("ENG"))
            {
                nomeperfil.Text = "No open profile\r\n0 AOA\r\n0 IK$";
                titulo0 = "Terms of Use";
                titulo1 = "Privacy Policy";
                titulo2 = "Exit";
                titulo3 = "Deposits";
                titulo4 = "Language";
                titulo5 = "Appointments";
                titulo6 = "Notifications";
                titulo7 = "Chat";
                titulo8 = "Extract";
                titulo9 = "Offers";
                titulo10 = "Start";
            }

            if (minhalingua.Equals("CH"))
            {
                nomeperfil.Text = "沒有打開配置文件\r\n0 AOA\r\n0 IK$";
                titulo0 = "使用條款";
                titulo1 = "隱私政策";
                titulo2 = "退出";
                titulo3 = "存款";
                titulo4 = "語言";
                titulo5 = "約會";
                titulo6 = "通知";
                titulo7 = "聊天";
                titulo8 = "摘錄";
                titulo9 = "優惠";
                titulo10 = "開始";
            }

            if (minhalingua.Equals("ESP"))
            {
                nomeperfil.Text = "No hay perfil abierto\r\n0 AOA\r\n0 IK$";
                titulo0 = "Términos de uso";
                titulo1 = "Política de privacidad";
                titulo2 = "Salir";
                titulo3 = "Depósitos";
                titulo4 = "Idioma";
                titulo5 = "Citas";
                titulo6 = "Notificaciones";
                titulo7 = "Chat";
                titulo8 = "Extraer";
                titulo9 = "Ofertas";
                titulo10 = "Inicio";
            }


            if (minhalingua.Equals("FR"))
            {
                nomeperfil.Text = "Aucun profil ouvert\r\n0 AOA\r\n0 IK$";
                titulo0 = "Conditions d'utilisation";
                titulo1 = "Politique de confidentialité";
                titulo2 = "Quitter";
                titulo3 = "Dépôts";
                titulo4 = "Langue";
                titulo5 = "Rendez-vous";
                titulo6 = "Notifications";
                titulo7 = "Chat";
                titulo8 = "Extraire";
                titulo9 = "Offres";
                titulo10 = "Démarrer";
            }

            if (minhalingua.Equals("PT"))
            {
                nomeperfil.Text = "Sem perfil aberto\r\n0 AOA\r\n0 IK$";
            }
        }


    }
    
}