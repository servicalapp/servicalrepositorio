using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using static System.Net.Mime.MediaTypeNames;

namespace IkumaTransport.janelas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agendamentos : ContentPage
    {
        string meuidonline = "",meufone="", meusdadosbd="";
        public SQLiteConnection conn;
        public Registration regmodel;
        public RegistraPerfil regmodel1;
        public RegistraLingua regmodel2;
        public Agendamentos()
        {
            InitializeComponent();
            conn = DependencyService.Get<Isqlite>().GetConnection();
            conn.CreateTable<Registration>();
            conn.CreateTable<RegistraPerfil>();
            conn.CreateTable<RegistraLingua>();
           // verlingua();
           // vermeusdados();
        }
       /* string minhalingua = "PT";
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
                bingacomodacoes.Text = "Accommodations"; 
                bingalugaviatura.Text = "Car rental"; 
                bingguia.Text = "Tourist guides"; 
                bingviagem.Text = "Travels";
            }

            if (minhalingua.Equals("CH"))
            {
                bingacomodacoes.Text = "住宿";
                bingalugaviatura.Text = "汽車租賃";
                bingguia.Text = "導遊";
                bingviagem.Text = "旅行";
            }

            if (minhalingua.Equals("ESP"))
            {
                bingacomodacoes.Text = "Alojamientos";
                bingalugaviatura.Text = "Alquiler de coches";
                bingguia.Text = "Guías turísticas";
                bingviagem.Text = "Viaje";
            }

            if (minhalingua.Equals("ARAB"))
            {
                bingacomodacoes.Text = "تجهيزات";
                bingalugaviatura.Text = "تأجير سيارات";
                bingguia.Text = "مرشدون سياحيون";
                bingviagem.Text = "سفر";
                    }

            if (minhalingua.Equals("FR"))
            {
                bingacomodacoes.Text = "Hébergement";
                bingalugaviatura.Text = "Location de voiture";
                bingguia.Text = "Guides touristiques";
                bingviagem.Text = "Voyage";
            }

            if (minhalingua.Equals("PT"))
            { }
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
            string olink = "verperfil", opost = meuidonline;
            if (meuidonline.Equals("")) { olink = "verperfilfone"; opost = meufone; }
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>(olink, ""),
              new KeyValuePair<string, string>("id", opost)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            if (content.ToString().Contains("<;>"))
            {


                var separador = '|';
                content = content.ToString().Replace("<;>", "|");
                var arrayfromEntry = content.ToString().Split(separador).ToList();
                meuidonline = arrayfromEntry[0];
                meusdadosbd = arrayfromEntry[1] + " " + arrayfromEntry[2];
                meufone = arrayfromEntry[3];

                buscaagendamentos();
            }
        }

        public async Task buscaagendamentos()
        {
            agendamentosacomodacoes.Children.Clear();
            agendamentosaluguer.Children.Clear(); 
            agendamentosguia.Children.Clear();
            agendamentosviagem.Children.Clear();
            Uri uri = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
            var postData = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("buscaagendamentos", meuidonline)
          };

            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();


            if (content.Contains("<;;>"))
            {
                var separador = '|';
                content = content.ToString().Replace("<;;>", "|");
                var arrayfromEntry = content.ToString().Split(separador).ToList();
                
                if (arrayfromEntry[0].Contains("<;>"))
                {
                    string content1 = arrayfromEntry[0].ToString().Replace("<;>", "|");
                    var arrayfromEntry1 = content1.ToString().Split(separador).ToList();
                    for (int i = 0; i < arrayfromEntry1.Count-1; i++)
                    {
                        string content2 = arrayfromEntry1[i].ToString().Replace("<,>", "|");
                        var arrayfromEntry2 = content2.ToString().Split(separador).ToList();

                        Frame frame = new Frame();
                        frame.Margin = 20;
                        frame.CornerRadius = 5;
                        frame.BackgroundColor = Color.White;
                        frame.Padding = 10;
                        frame.BorderColor = Color.FromHex("#CC009CFF");
                        frame.IsClippedToBounds = true;

                        StackLayout stackLayout = new StackLayout();
                        Label label = new Label();
                        label.FontSize = 20;
                        label.FontFamily = "Candara";
                        label.TextColor = Color.Black;
                        label.Text = arrayfromEntry2[22] + "-" + arrayfromEntry2[23] + "\nAgendamento de " +
                            arrayfromEntry2[24] + " " + arrayfromEntry2[14] + "\nDetalhes do agendamento:\nAgendamento Nº " +
                            arrayfromEntry2[0] + "\nData do agendamento: " + arrayfromEntry2[21] + "\nCliente nº " + meuidonline + "     Nome: " +
                            meusdadosbd + "\nCategoria do agendamento: Acomodação(" + arrayfromEntry2[24] + ")\n" +
                            "Por favor, faça-se presente no endereço a baixo na data e hora do checkin, caso o agendamento for aceite." +
                            "\r\n" + arrayfromEntry2[23] + "\r\nObrigado\r\n" +
                            "Dados do agendamento: Checkin-" + arrayfromEntry2[1] + " Checkou-" + arrayfromEntry2[2] + " Classe-" + arrayfromEntry2[3]
                            + " Nº de adultos-" + arrayfromEntry2[5] + " Nº de crianças-" + arrayfromEntry2[4] + " Nº de animais-" + arrayfromEntry2[6]
                            + " Nº de bagagens-" + arrayfromEntry2[7] + " Nº de viaturas-" + arrayfromEntry2[8] + " Nº de domésticos-" + arrayfromEntry2[16]
                            + " Nº de cozinheiros-" + arrayfromEntry2[17] + " Nº de lavadeiras-" + arrayfromEntry2[18] + " Nº de engomadeiras-" + arrayfromEntry2[19]
                            + " Nº de arrumadeiras-" + arrayfromEntry2[20] + "\nPor: " + arrayfromEntry2[11] + " AOA\nPagamento via: " + arrayfromEntry2[12] +
                            "\r\nSe estiver com dificuldade de localizar o espaço click no link https://www.google.com/maps/@" + arrayfromEntry2[25] +
                            "\nPoderá também, chegar ao local para obter mais informações detalhadas.\r\n" +
                            "Para apoio técnico, inicie uma conversa com o provedor, no chat ou entrar em contacto com o suporte técnico do Ikuma.";
                        if (minhalingua.Equals("ENG"))
                        {
                            label.Text = arrayfromEntry2[22] + "-" + arrayfromEntry2[23] + "\nSchedule of " +
                             arrayfromEntry2[24] + " " + arrayfromEntry2[14] + "\nAppointment details:\nAppointment No. " +
                             arrayfromEntry2[0] + "\nDate of appointment: " + arrayfromEntry2[21] + "\nCustomer # " + meuidonline + " Name: " +
                             meusdadosbd + "\nBooking category: Accommodation(" + arrayfromEntry2[24] + ")\n" +
                             "Please, be present at the address below on the date and time of checkin, if the appointment is accepted." +
                             "\r\n" + arrayfromEntry2[23] + "\r\nThank you\r\n" +
                             "Schedule data: Checkin-" + arrayfromEntry2[1] + " Checked-" + arrayfromEntry2[2] + " Class-" + arrayfromEntry2[3]
                             + " # of adults-" + arrayfromEntry2[5] + " # of children-" + arrayfromEntry2[4] + " # of animals-" + arrayfromEntry2[6]
                             + " Nº of luggage-" + arrayfromEntry2[7] + " Nº of vehicles-" + arrayfromEntry2[8] + " Nº of domestic-" + arrayfromEntry2[16]
                             + " No. of cooks-" + arrayfromEntry2[17] + " No. of laundresses-" + arrayfromEntry2[18] + " No. of ironers-" + arrayfromEntry2[19]
                             + " Number of maids-" + arrayfromEntry2[20] + "\nBy: " + arrayfromEntry2[11] + " AOA\nPayment via: " + arrayfromEntry2[12] +
                             "\r\nIf you are having difficulty locating the space click on the link https://www.google.com/maps/@" + arrayfromEntry2[25] +
                             "\nYou can also reach the site for more detailed information.\r\n" +
                             "For technical support, start a conversation with the provider, in the chat or contact Ikuma technical support.";
                        }

                        if (minhalingua.Equals("CH"))
                        {
                            label.Text = arrayfromEntry2[22] + "-" + arrayfromEntry2[23] + "\nSchedule of " +
                             arrayfromEntry2[24] + " " + arrayfromEntry2[14] + "\n預約詳情：\n預約號" +
                             arrayfromEntry2[0] + "\n預約日期：" + arrayfromEntry2[21] + "\nCustomer # " + meuidonline + " 姓名：" +
                             meusdadosbd + "\n預訂類別：住宿(" + arrayfromEntry2[24] + ")\n" +
                             "如果預約被接受，請在入住日期和時間出現在下面的地址。" +
                             "\r\n" + arrayfromEntry2[23] + "\r\n謝謝\r\n" +
                             "調度數據：Checkin -" + arrayfromEntry2[1] + " Checked -" + arrayfromEntry2[2] + " Class -" + arrayfromEntry2[3]
                             + " # of adults-" + arrayfromEntry2[5] + " # of children-" + arrayfromEntry2[4] + " # of animals-" + arrayfromEntry2[6]
                             + "行李數量-" + arrayfromEntry2[7] + "車輛數量-" + arrayfromEntry2[8] + "國內數量-" + arrayfromEntry2[16]
                             + "# of cooks-" + arrayfromEntry2[17] + "# of washerwomen-" + arrayfromEntry2[18] + "# of ironers-" + arrayfromEntry2[19]
                             + "女傭人數-" + arrayfromEntry2[20] + "\nBy: " + arrayfromEntry2[11] + " AOA\nPayment via: " + arrayfromEntry2[12] +
                             "\r\n如果您在定位空間時遇到困難，請單擊鏈接 https://www.google.com/maps/@" + arrayfromEntry2[25] +
                             "\n您也可以訪問該站點以獲取更多詳細信息。\r\n" +
                             "要獲得技術支持，請在聊天中與供應商開始對話或聯繫 Ikuma 技術支持。";
                        }

                        if (minhalingua.Equals("ESP"))
                        {
                            label.Text = arrayfromEntry2[22] + "-" + arrayfromEntry2[23] + "\nHorario de " +
                             arrayfromEntry2[24] + " " + arrayfromEntry2[14] + "\nDetalles de la cita:\nNúmero de cita " +
                             arrayfromEntry2[0] + "\nFecha de la cita: " + arrayfromEntry2[21] + "\nCliente # " + meuidonline + " Nombre: " +
                             meusdadosbd + "\nCategoría de reserva: Alojamiento(" + arrayfromEntry2[24] + ")\n" +
                             "Por favor, esté presente en la dirección a continuación en la fecha y hora del check-in, si se acepta la cita" +
                             "\r\n" + arrayfromEntry2[23] + "\r\nGracias\r\n" +
                             "Datos del programa: Check-in-" + arrayfromEntry2[1] + " Checked-" + arrayfromEntry2[2] + " Class-" + arrayfromEntry2[3]
                             + " # de adultos-" + arrayfromEntry2[5] + " # of children-" + arrayfromEntry2[4] + " # of animals-" + arrayfromEntry2[6]
                             + " Nº de equipaje-" + arrayfromEntry2[7] + " Nº de vehículos-" + arrayfromEntry2[8] + " Nº de domésticos-" + arrayfromEntry2[16]
                             + " # de cocineras-" + arrayfromEntry2[17] + " # of lavanderas-" + arrayfromEntry2[18] + " # of ironers-" + arrayfromEntry2[19]
                             + " Número de sirvientas-" + arrayfromEntry2[20] + "\nBy: " + arrayfromEntry2[11] + " AOA\nPago vía: " + arrayfromEntry2[12] +
                             "\r\nSi tiene dificultades para ubicar el espacio, haga clic en el enlace https://www.google.com/maps/@" + arrayfromEntry2[25] +
                             "\nTambién puede acceder al sitio para obtener información más detallada.\r\n" +
                             "Para soporte técnico, inicie una conversación con el proveedor, en el chat o comuníquese con el soporte técnico de Ikuma.";
                        }

                        if (minhalingua.Equals("FR"))
                        {
                            label.Text = arrayfromEntry2[22] + "-" + arrayfromEntry2[23] + "\nSchedule of " +
                              arrayfromEntry2[24] + " " + arrayfromEntry2[14] + "\nDétails du rendez-vous :\nN° de rendez-vous " +
                              arrayfromEntry2[0] + "\nDate du rendez-vous : " + arrayfromEntry2[21] + "\nNuméro client " + meuidonline + " Nom : " +
                              meusdadosbd + "\nCatégorie de réservation : Hébergement(" + arrayfromEntry2[24] + ")\n" +
                              "Veuillez être présent à l'adresse ci-dessous à la date et à l'heure de l'enregistrement, si le rendez-vous est accepté." +
                              "\r\n" + arrayfromEntry2[23] + "\r\nMerci\r\n" +
                              "Données de planification : Checkin-" + arrayfromEntry2[1] + " Checked-" + arrayfromEntry2[2] + " Class-" + arrayfromEntry2[3]
                              + " # d'adultes-" + arrayfromEntry2[5] + " # d'enfants-" + arrayfromEntry2[4] + " # d'animaux-" + arrayfromEntry2[6]
                              + " Nº de bagages-" + arrayfromEntry2[7] + " Nº de véhicules-" + arrayfromEntry2[8] + " Nº de domestique-" + arrayfromEntry2[16]
                              + " # de cuisiniers-" + arrayfromEntry2[17] + " # de laveuses-" + arrayfromEntry2[18] + " # de repasseuses-" + arrayfromEntry2[19]
                              + " Nombre de femmes de ménage-" + arrayfromEntry2[20] + "\nPar : " + arrayfromEntry2[11] + " AOA\nPaiement via : " + arrayfromEntry2[12] +
                              "\r\nSi vous rencontrez des difficultés pour localiser l'espace, cliquez sur le lien https://www.google.com/maps/@" + arrayfromEntry2[25] +
                              "\nVous pouvez également accéder au site pour des informations plus détaillées.\r\n" +
                              "Pour le support technique, démarrez une conversation avec le fournisseur, dans le chat ou contactez le support technique d'Ikuma.";
                        }
                        StackLayout stacklayout1 = new StackLayout();
                        stacklayout1.Children.Add(label);
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) =>
                        {
                            await Browser.OpenAsync("https://ikumatransport.mypressonline.com/turismo/doturismo.php?criapdf=" + label.Text.ToString());
                        };
                        stacklayout1.GestureRecognizers.Add(tapGestureRecognizer);

                        StackLayout stackLayout2 = new StackLayout();
                        stackLayout2.Orientation = StackOrientation.Horizontal;
                        stackLayout2.Padding = 20;
                        stackLayout2.HorizontalOptions = LayoutOptions.Center;

                        StackLayout stackLayout3 = new StackLayout();
                        Image image = new Image();
                        image.Source = "aceitar.png";
                        image.WidthRequest = 30;
                        image.HeightRequest = 30;
                        stackLayout3.Children.Add(image);
                        var tapGestureRecognizer1 = new TapGestureRecognizer();
                        tapGestureRecognizer1.Tapped += (s, e) =>
                        {
                            Navigation.PushModalAsync(new Chat(arrayfromEntry2[0]));
                        };
                        stackLayout3.GestureRecognizers.Add(tapGestureRecognizer1);

                        StackLayout stackLayout4 = new StackLayout();
                        Image image1 = new Image();
                        image1.Source = "cancelar.png";
                        image1.WidthRequest = 30;
                        image1.HeightRequest = 30;
                        stackLayout4.Children.Add(image1);
                        var tapGestureRecognizer2 = new TapGestureRecognizer();
                        tapGestureRecognizer2.Tapped += async (s, e) =>
                        {
                            Uri uri1 = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
                            var postData1 = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("cencelaagendamento", ""),
              new KeyValuePair<string, string>("id", arrayfromEntry2[0]),
              new KeyValuePair<string, string>("tipo", "agendamentosacomodacoes")
          };

                            HttpRequestMessage req1 = new HttpRequestMessage(HttpMethod.Post, uri1);
                            req1.Content = new FormUrlEncodedContent(postData1);
                            HttpClient client1 = new HttpClient();
                            var response1 = await client1.SendAsync(req1);
                            var contents = await response1.Content.ReadAsStringAsync();
                            
                            if (minhalingua.Equals("ENG"))
                            {
                                DisplayAlert("Appointment cancellation", "When canceling an appointment, " +
                                  "will be deducted if the provider has already accepted it.", "Ok");
                            }

                            if (minhalingua.Equals("CH"))
                            {
                                DisplayAlert("取消預約", "取消預約時, " +
                                  "如果供應商已經接受，將被扣除。", "好的");
                            }

                            if (minhalingua.Equals("ESP"))
                            {
                                DisplayAlert("Cancelación de cita", "Al cancelar una cita," +
                                  "se descontará si el proveedor ya lo ha aceptado.", "Ok");
                            }

                            if (minhalingua.Equals("FR"))
                            {
                                DisplayAlert(" Annulation de rendez - vous ", " Lors de l'annulation d'un rendez - vous ," +
                                  "sera déduit si le fournisseur l'a déjà accepté.", "Ok");
                            }

                            if (minhalingua.Equals("PT"))
                            {
                                DisplayAlert("Cancelamento do agendamento", "Ao cancelar um agendamento, " +
                                 "será descontado caso o provedor já o tenha aceite.", "Ok");
                            }
                            buscaagendamentos();
                        };
                        stackLayout4.GestureRecognizers.Add(tapGestureRecognizer2);

                        stackLayout2.Children.Add(stackLayout3);
                        stackLayout2.Children.Add(stackLayout4);

                        stackLayout.Children.Add(stacklayout1);
                        if (!arrayfromEntry2[14].Contains("realizado") || !arrayfromEntry2[14].Contains("cancelado") || !arrayfromEntry2[14].Contains("suspenso"))
                        { stackLayout.Children.Add(stackLayout2); }
                        frame.Content = stackLayout;

                        agendamentosacomodacoes.Children.Add(frame);
                    }
                }



               // DisplayAlert("Cancelamento do agendamento", arrayfromEntry[1], "Ok");

                if (arrayfromEntry[1].Contains("<;>"))
                {
                    
                    string content1 = arrayfromEntry[1].ToString().Replace("<;>", "|");
                    var arrayfromEntry1 = content1.ToString().Split(separador).ToList();
                    for (int i = 0; i < arrayfromEntry1.Count-1; i++)
                    {
                        string content2 = arrayfromEntry1[i].ToString().Replace("<,>", "|");
                        var arrayfromEntry2 = content2.ToString().Split(separador).ToList();

                        Frame frame = new Frame();
                        frame.Margin = 20;
                        frame.CornerRadius = 5;
                        frame.BackgroundColor = Color.White;
                        frame.Padding = 10;
                        frame.BorderColor = Color.FromHex("#CC009CFF");
                        frame.IsClippedToBounds = true;

                        StackLayout stackLayout = new StackLayout();
                        Label label = new Label();
                        label.FontSize = 20;
                        label.FontFamily = "Candara";
                        label.TextColor = Color.Black;
                        label.Text = "oi";
                         label.Text = arrayfromEntry2[12] + "-" + arrayfromEntry2[11] + "\nAgendamento de " +
                             arrayfromEntry2[11] + " "+ arrayfromEntry2[8] + "\nDetalhes do agendamento:\nAgendamento Nº " +
                             arrayfromEntry2[0] + "\nData do agendamento: " + arrayfromEntry2[9] + "\nCliente nº " + meuidonline + "     Nome: " +
                             meusdadosbd + "\nCategoria do agendamento: Viatura(" + arrayfromEntry2[12] + ")\n" +
                             "Por favor, faça-se presente no endereço a baixo na data e hora do checkin, caso o agendamento for aceite." +
                             "\r\n" + arrayfromEntry2[11] + "\r\nObrigado\r\n" +
                             "Dados do agendamento: Checkin-" + arrayfromEntry2[3] + " Checkou-" + arrayfromEntry2[4] + " Com motorista-" + arrayfromEntry2[5]
                            +"\nPor: " + arrayfromEntry2[7] + " AOA\nPagamento via: " + arrayfromEntry2[6] +
                             "\r\nSe estiver com dificuldade de localizar o espaço click no link https://www.google.com/maps/@" + arrayfromEntry2[13] +
                             "\nPoderá também, chegar ao local para obter mais informações detalhadas.\r\n" +
                             "Para apoio técnico, inicie uma conversa com o provedor, no chat ou entrar em contacto com o suporte técnico do Ikuma.";
                        if (minhalingua.Equals("ENG"))
                        {
                            label.Text = arrayfromEntry2[12] + "-" + arrayfromEntry2[11] + "\nSchedule of " +
                              arrayfromEntry2[11] + " " + arrayfromEntry2[8] + "\nAppointment details:\nAppointment # " +
                              arrayfromEntry2[0] + "\nDate of appointment: " + arrayfromEntry2[9] + "\nCustomer # " + meuidonline + " Name: " +
                              meusdadosbd + "\nAppointment category: Vehicle(" + arrayfromEntry2[12] + ")\n" +
                              "Please, be present at the address below on the date and time of checkin, if the appointment is accepted." +
                              "\r\n" + arrayfromEntry2[11] + "\r\nThank you\r\n" +
                              "Schedule data: Checkin-" + arrayfromEntry2[3] + " Checked-" + arrayfromEntry2[4] + " With driver-" + arrayfromEntry2[5]
                             + "\nBy: " + arrayfromEntry2[7] + " AOA\nPayment via: " + arrayfromEntry2[6] +
                              "\r\nIf you are having difficulty locating the space click on the link https://www.google.com/maps/@" + arrayfromEntry2[13] +
                              "\nYou can also reach the site for more detailed information.\r\n" +
                              "For technical support, start a conversation with the provider, in the chat or contact Ikuma technical support.";
                        
                        }

                        if (minhalingua.Equals("CH"))
                        {
                            label.Text = arrayfromEntry2[12] + "-" + arrayfromEntry2[11] + "\nSchedule of " +
                              arrayfromEntry2[11] + " " + arrayfromEntry2[8] + "\n約會詳細信息：\n約會 # " +
                              arrayfromEntry2[0] + "\n預約日期：" + arrayfromEntry2[9] + "\nCustomer # " + meuidonline + " 姓名：" +
                              meusdadosbd + "\n約會類別：車輛(" + arrayfromEntry2[12] + ")\n" +
                              "如果預約被接受，請在入住日期和時間出現在下面的地址。" +
                              "\r\n" + arrayfromEntry2[11] + "\r\n謝謝\r\n" +
                              "Schedule data: Checkin-" + arrayfromEntry2[3] + " Checked-" + arrayfromEntry2[4] + " With driver-" + arrayfromEntry2[5]
                             + "\n通過：" + arrayfromEntry2[7] + " AOA\n付款方式：" + arrayfromEntry2[6] +
                              "\r\n如果您找不到空間，請單擊鏈接 https://www.google.com/maps/@" + arrayfromEntry2[13] +
                              "\n您也可以訪問該站點以獲取更多詳細信息。\r\n" +
                              "要獲得技術支持，請在聊天中與供應商開始對話或聯繫 Ikuma 技術支持。";
                        }

                        if (minhalingua.Equals("ESP"))
                        {
                            label.Text = arrayfromEntry2[12] + "-" + arrayfromEntry2[11] + "\nHorario de " +
                               arrayfromEntry2[11] + " " + arrayfromEntry2[8] + "\nDetalles de la cita:\nCita # " +
                               arrayfromEntry2[0] + "\nFecha de la cita: " + arrayfromEntry2[9] + "\nCliente # " + meuidonline + " Nombre: " +
                               meusdadosbd + "\nCategoría de cita: Vehículo(" + arrayfromEntry2[12] + ")\n" +
                               "Por favor, esté presente en la dirección a continuación en la fecha y hora del check-in, si se acepta la cita" +
                               "\r\n" + arrayfromEntry2[11] + "\r\nGracias\r\n" +
                               "Programar datos: Check-in-" + arrayfromEntry2[3] + " Checked-" + arrayfromEntry2[4] + " With driver-" + arrayfromEntry2[5]
                              + "\nPor: " + arrayfromEntry2[7] + " AOA\nPago mediante: " + arrayfromEntry2[6] +
                               "\r\nSi tiene dificultades para ubicar el espacio, haga clic en el enlace https://www.google.com/maps/@" + arrayfromEntry2[13] +
                               "\nTambién puede acceder al sitio para obtener información más detallada.\r\n" +
                               "Para soporte técnico, inicie una conversación con el proveedor, en el chat o comuníquese con el soporte técnico de Ikuma.";
                        
                        }

                        if (minhalingua.Equals("FR"))
                        {
                            label.Text = arrayfromEntry2[12] + "-" + arrayfromEntry2[11] + "\nSchedule of " +
                              arrayfromEntry2[11] + " " + arrayfromEntry2[8] + "\nDétails du rendez-vous :\nRendez-vous # " +
                              arrayfromEntry2[0] + "\nDate du rendez-vous : " + arrayfromEntry2[9] + "\nNuméro client " + meuidonline + " Nom : " +
                              meusdadosbd + "\nCatégorie de rendez-vous : Véhicule(" + arrayfromEntry2[12] + ")\n" +
                              "Veuillez être présent à l'adresse ci-dessous à la date et à l'heure de l'enregistrement, si le rendez-vous est accepté." +
                              "\r\n" + arrayfromEntry2[11] + "\r\nMerci\r\n" +
                              "Données de planification : Checkin-" + arrayfromEntry2[3] + " Checked-" + arrayfromEntry2[4] + " With driver-" + arrayfromEntry2[5]
                             + "\nPar : " + arrayfromEntry2[7] + " AOA\nPaiement via : " + arrayfromEntry2[6] +
                              "\r\nSi vous rencontrez des difficultés pour localiser l'espace, cliquez sur le lien https://www.google.com/maps/@" + arrayfromEntry2[13] +
                              "\nVous pouvez également accéder au site pour des informations plus détaillées.\r\n" +
                              "Pour le support technique, démarrez une conversation avec le fournisseur, dans le chat ou contactez le support technique d'Ikuma.";
                        
                        }
                        StackLayout stacklayout1 = new StackLayout();
                        stacklayout1.Children.Add(label);
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) =>
                        {
                            await Browser.OpenAsync("https://ikumatransport.mypressonline.com/turismo/doturismo.php?criapdf=" + label.Text.ToString());
                        };
                        stacklayout1.GestureRecognizers.Add(tapGestureRecognizer);

                        StackLayout stackLayout2 = new StackLayout();
                        stackLayout2.Orientation = StackOrientation.Horizontal;
                        stackLayout2.Padding = 20;
                        stackLayout2.HorizontalOptions = LayoutOptions.Center;

                        StackLayout stackLayout3 = new StackLayout();
                        Image image = new Image();
                        image.Source = "aceitar.png";
                        image.WidthRequest = 30;
                        image.HeightRequest = 30;
                        stackLayout3.Children.Add(image);
                        var tapGestureRecognizer1 = new TapGestureRecognizer();
                        tapGestureRecognizer1.Tapped += (s, e) =>
                        {
                            Navigation.PushModalAsync(new Chat(arrayfromEntry2[0]));
                        };
                        stackLayout3.GestureRecognizers.Add(tapGestureRecognizer1);

                        StackLayout stackLayout4 = new StackLayout();
                        Image image1 = new Image();
                        image1.Source = "cancelar.png";
                        image1.WidthRequest = 30;
                        image1.HeightRequest = 30;
                        stackLayout4.Children.Add(image1);
                        var tapGestureRecognizer2 = new TapGestureRecognizer();
                        tapGestureRecognizer2.Tapped += async (s, e) =>
                        {
                            Uri uri1 = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
                            var postData1 = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("cencelaagendamento", ""),
              new KeyValuePair<string, string>("id", arrayfromEntry2[0]),
              new KeyValuePair<string, string>("tipo", "agendamentosaluguer")
          };

                            HttpRequestMessage req1 = new HttpRequestMessage(HttpMethod.Post, uri1);
                            req1.Content = new FormUrlEncodedContent(postData1);
                            HttpClient client1 = new HttpClient();
                            var response1 = await client1.SendAsync(req1);
                            var contents = await response1.Content.ReadAsStringAsync();
                            
                            if (minhalingua.Equals("ENG"))
                            {
                                DisplayAlert("Appointment cancellation", "When canceling an appointment, " +
                                 "will be deducted if the provider has already accepted it.", "Ok");
                            }

                            if (minhalingua.Equals("CH"))
                            {
                                DisplayAlert("取消預約", "取消預約時, " +
                                 "如果供應商已經接受，將被扣除。", "好的");
                            }

                            if (minhalingua.Equals("ESP"))
                            {
                                DisplayAlert("Cancelación de cita", "Al cancelar una cita se descontará si el proveedor ya lo ha aceptado.", "Ok");
                            }

                            if (minhalingua.Equals("FR"))
                            {
                                DisplayAlert(" Annulation de rendez - vous ", " Lors de l'annulation d'un rendez - vous sera déduit si le fournisseur l'a déjà accepté.", "Ok");
                            }

                            if (minhalingua.Equals("PT"))
                            {
                                DisplayAlert("Cancelamento do agendamento", "Ao cancelar um agendamento, " +
                                "será descontado caso o provedor já o tenha aceite.", "Ok");
                            }
                            buscaagendamentos();
                        };
                        stackLayout4.GestureRecognizers.Add(tapGestureRecognizer2);

                        stackLayout2.Children.Add(stackLayout3);
                        stackLayout2.Children.Add(stackLayout4);

                        stackLayout.Children.Add(stacklayout1);
                        if (!arrayfromEntry2[8].Contains("realizado") || !arrayfromEntry2[8].Contains("cancelado") || !arrayfromEntry2[8].Contains("suspenso"))
                        { stackLayout.Children.Add(stackLayout2); }
                        frame.Content = stackLayout;

                        agendamentosaluguer.Children.Add(frame);
                    }
                }





                if (arrayfromEntry[2].Contains("<;>"))
                {
                    string content1 = arrayfromEntry[2].ToString().Replace("<;>", "|");
                    var arrayfromEntry1 = content1.ToString().Split(separador).ToList();
                    for (int i = 0; i < arrayfromEntry1.Count-1; i++)
                    {
                        string content2 = arrayfromEntry1[i].ToString().Replace("<,>", "|");
                        var arrayfromEntry2 = content2.ToString().Split(separador).ToList();

                        Frame frame = new Frame();
                        frame.Margin = 20;
                        frame.CornerRadius = 5;
                        frame.BackgroundColor = Color.White;
                        frame.Padding = 10;
                        frame.BorderColor = Color.FromHex("#CC009CFF");
                        frame.IsClippedToBounds = true;

                        StackLayout stackLayout = new StackLayout();
                        Label label = new Label();
                        label.FontSize = 20;
                        label.FontFamily = "Candara";
                        label.TextColor = Color.Black;
                        label.Text = arrayfromEntry2[10] + "\nAgendamento de Guia de turismo" +
                            " " + arrayfromEntry2[7] + "\nDetalhes do agendamento:\nAgendamento Nº " +
                            arrayfromEntry2[0] + "\nData do agendamento: " + arrayfromEntry2[9] + "\nCliente nº " + meuidonline + "     Nome: " +
                            meusdadosbd + "\nCategoria do agendamento: Guia de turismo(" + arrayfromEntry2[10] + ")\n" +
                            "Por favor, faça-se presente no endereço a baixo na data e hora do checkin, caso o agendamento for aceite." +
                            "\r\n" + arrayfromEntry2[10] + "\r\nObrigado\r\n" +
                            "Dados do agendamento: Checkin-" + arrayfromEntry2[2] + " Checkou-" + arrayfromEntry2[3] + 
                          "\nPor: " + arrayfromEntry2[4] + " AOA\n" +
                           "Para apoio técnico, inicie uma conversa com o provedor, no chat ou entrar em contacto com o suporte técnico do Ikuma.";
                        if (minhalingua.Equals("ENG"))
                        {
                            label.Text = arrayfromEntry2[10] + "\nTour Guide Scheduling" +
                             " " + arrayfromEntry2[7] + "\nScheduling details:\nScheduling # " +
                             arrayfromEntry2[0] + "\nDate of appointment: " + arrayfromEntry2[9] + "\nCustomer # " + meuidonline + " Name: " +
                             meusdadosbd + "\nAppointment category: Tour guide(" + arrayfromEntry2[10] + ")\n" +
                             "Please, be present at the address below on the date and time of checkin, if the appointment is accepted." +
                             "\r\n" + arrayfromEntry2[10] + "\r\nThank you\r\n" +
                             "Schedule data: Checkin-" + arrayfromEntry2[2] + " Checked-" + arrayfromEntry2[3] +
                           "\nBy: " + arrayfromEntry2[4] + " AOA\n" +
                            "For technical support, start a conversation with the provider, in the chat or contact Ikuma technical support.";
                        }

                        if (minhalingua.Equals("CH"))
                        {
                            label.Text = arrayfromEntry2[10] + "\n導遊安排" +
                              " " + arrayfromEntry2[7] + "\n調度詳細信息：\n調度 # " +
                              arrayfromEntry2[0] + "\n預約日期：" + arrayfromEntry2[9] + "\nCustomer # " + meuidonline + " 姓名：" +
                              meusdadosbd + "\n預約類別：導遊(" + arrayfromEntry2[10] + ")\n" +
                             "如果預約被接受，請在入住日期和時間出現在下面的地址。" +
                             "\r\n" + arrayfromEntry2[10] + "\r\n謝謝\r\n" +
                             "計劃數據：Checkin -" + arrayfromEntry2[2] + "Checked -" + arrayfromEntry2[3] +
                           "\n通過：" + arrayfromEntry2[4] + " AOA\n" +
                            "要獲得技術支持，請在聊天中與供應商開始對話或聯繫 Ikuma 技術支持。";
                        }

                        if (minhalingua.Equals("ESP"))
                        {
                            label.Text = arrayfromEntry2[10] + "\nProgramación de guía turística" +
                             " " + arrayfromEntry2[7] + "\nDetalles de programación:\nProgramación # " +
                             arrayfromEntry2[0] + "\nFecha de la cita: " + arrayfromEntry2[9] + "\nCliente # " + meuidonline + " Nombre: " +
                            meusdadosbd + "\nCategoría de cita: Guía turístico(" + arrayfromEntry2[10] + ")\n" +
                             "Por favor, esté presente en la dirección a continuación en la fecha y hora del check-in, si se acepta la cita" +
                             "\r\n" + arrayfromEntry2[10] + "\r\nGracias\r\n" +
                             "Programar datos: Registrar-" + arrayfromEntry2[2] + " Checked-" + arrayfromEntry2[3] +
                           "\nPor: " + arrayfromEntry2[4] + " AOA\n" +
                            "Para soporte técnico, inicie una conversación con el proveedor, en el chat o comuníquese con el soporte técnico de Ikuma.";
                        }

                        if (minhalingua.Equals("FR"))
                        {
                            label.Text = arrayfromEntry2[10] + "\nPlanification du guide touristique" +
                              " " + arrayfromEntry2[7] + "\nDétails de la planification :\nPlanification # " +
                              arrayfromEntry2[0] + "\nDate du rendez-vous : " + arrayfromEntry2[9] + "\nNuméro client " + meuidonline + " Nom : " +
                              meusdadosbd + "\nCatégorie de rendez-vous : guide touristique(" + arrayfromEntry2[10] + ")\n" +
                              "Veuillez être présent à l'adresse ci-dessous à la date et à l'heure de l'enregistrement, si le rendez-vous est accepté." +
                              "\r\n" + arrayfromEntry2[10] + "\r\nMerci\r\n" +
                              "Données de planification : Checkin-" + arrayfromEntry2[2] + " Checked-" + arrayfromEntry2[3] +
                            "\nPar : " + arrayfromEntry2[4] + " AOA\n" +
                             "Pour le support technique, démarrez une conversation avec le fournisseur, dans le chat ou contactez le support technique d'Ikuma.";
                        }
                        StackLayout stacklayout1 = new StackLayout();
                        stacklayout1.Children.Add(label);
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) =>
                        {
                            await Browser.OpenAsync("https://ikumatransport.mypressonline.com/turismo/doturismo.php?criapdf=" + label.Text.ToString());
                        };
                        stacklayout1.GestureRecognizers.Add(tapGestureRecognizer);
                        StackLayout stackLayout2 = new StackLayout();
                        stackLayout2.Orientation = StackOrientation.Horizontal;
                        stackLayout2.Padding = 20;
                        stackLayout2.HorizontalOptions = LayoutOptions.Center;

                        StackLayout stackLayout3 = new StackLayout();
                        Image image = new Image();
                        image.Source = "aceitar.png";
                        image.WidthRequest = 30;
                        image.HeightRequest = 30;
                        stackLayout3.Children.Add(image);
                        var tapGestureRecognizer1 = new TapGestureRecognizer();
                        tapGestureRecognizer1.Tapped += (s, e) =>
                        {
                            Navigation.PushModalAsync(new Chat(arrayfromEntry2[0]));
                        };
                        stackLayout3.GestureRecognizers.Add(tapGestureRecognizer1);

                        StackLayout stackLayout4 = new StackLayout();
                        Image image1 = new Image();
                        image1.Source = "cancelar.png";
                        image1.WidthRequest = 30;
                        image1.HeightRequest = 30;
                        stackLayout4.Children.Add(image1);
                        var tapGestureRecognizer2 = new TapGestureRecognizer();
                        tapGestureRecognizer2.Tapped += async (s, e) =>
                        {
                            Uri uri1 = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
                            var postData1 = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("cencelaagendamento", ""),
              new KeyValuePair<string, string>("id", arrayfromEntry2[0]),
              new KeyValuePair<string, string>("tipo", "agendamentosguia")
          };

                            HttpRequestMessage req1 = new HttpRequestMessage(HttpMethod.Post, uri1);
                            req1.Content = new FormUrlEncodedContent(postData1);
                            HttpClient client1 = new HttpClient();
                            var response1 = await client1.SendAsync(req1);
                            var contents = await response1.Content.ReadAsStringAsync();
                            
                            if (minhalingua.Equals("ENG"))
                            {
                                DisplayAlert("Appointment cancellation", "When canceling an appointment, " +
                                 "will be deducted if the provider has already accepted it.", "Ok");
                            }

                            if (minhalingua.Equals("CH"))
                            {
                                DisplayAlert("取消預約", "取消預約時, " +
                                 "如果供應商已經接受，將被扣除。", "好的");
                            }

                            if (minhalingua.Equals("ESP"))
                            {
                                DisplayAlert("Cancelación de cita", "Al cancelar una cita se descontará si el proveedor ya lo ha aceptado.", "Ok");
                            }

                            if (minhalingua.Equals("FR"))
                            {
                                DisplayAlert(" Annulation de rendez - vous ", " Lors de l'annulation d'un rendez - vous sera déduit si le fournisseur l'a déjà accepté.", "Ok");
                            }

                            if (minhalingua.Equals("PT"))
                            {
                                DisplayAlert("Cancelamento do agendamento", "Ao cancelar um agendamento, " +
                                "será descontado caso o provedor já o tenha aceite.", "Ok");
                            }
                            buscaagendamentos();
                        };
                        stackLayout4.GestureRecognizers.Add(tapGestureRecognizer2);

                        stackLayout2.Children.Add(stackLayout3);
                        stackLayout2.Children.Add(stackLayout4);

                        stackLayout.Children.Add(stacklayout1);
                        if (!arrayfromEntry2[7].Contains("realizado") || !arrayfromEntry2[7].Contains("cancelado") || !arrayfromEntry2[8].Contains("suspenso"))
                        { stackLayout.Children.Add(stackLayout2); }
                        frame.Content = stackLayout;

                        agendamentosguia.Children.Add(frame);
                    }
                }




                if (arrayfromEntry[3].Contains("<;>"))
                {
                    string content1 = arrayfromEntry[3].ToString().Replace("<;>", "|");
                    var arrayfromEntry1 = content1.ToString().Split(separador).ToList();
                    for (int i = 0; i < arrayfromEntry1.Count-1; i++)
                    {
                        string content2 = arrayfromEntry1[i].ToString().Replace("<,>", "|");
                        var arrayfromEntry2 = content2.ToString().Split(separador).ToList();

                        Frame frame = new Frame();
                        frame.Margin = 20;
                        frame.CornerRadius = 5;
                        frame.BackgroundColor = Color.White;
                        frame.Padding = 10;
                        frame.BorderColor = Color.FromHex("#CC009CFF");
                        frame.IsClippedToBounds = true;

                        StackLayout stackLayout = new StackLayout();
                        Label label = new Label();
                        label.FontSize = 20;
                        label.FontFamily = "Candara";
                        label.TextColor = Color.Black;
                        label.Text = arrayfromEntry2[13] + "\nAgendamento de compainha " +
                            arrayfromEntry2[14] + "\nDetalhes do agendamento:\nAgendamento Nº " +
                            arrayfromEntry2[0] + "\nData do agendamento: " + arrayfromEntry2[12] + "\nCliente nº " + meuidonline + "     Nome: " +
                            meusdadosbd + "\nCategoria do agendamento: Viagem(" + arrayfromEntry2[14] + ")\n" +
                            "Por favor, faça-se presente no endereço a baixo na data e hora do checkin, caso o agendamento for aceite." +
                            "\r\n" + arrayfromEntry2[2] + "\r\nObrigado\r\n" +
                            "Dados do agendamento: Checkin-" + arrayfromEntry2[2] + " Classe-" + arrayfromEntry2[4] + " Adultos-" + arrayfromEntry2[5]+
                             " Crianças-" + arrayfromEntry2[6] +" Durante-" + arrayfromEntry2[7]+" mins Ida e volta-" + arrayfromEntry2[9]
                           + "\nPor: " + arrayfromEntry2[8] + " AOA\n"+
                            "\nPoderá também, chegar ao local para obter mais informações detalhadas.\r\n" +
                            "Para apoio técnico, inicie uma conversa com o provedor, no chat ou entrar em contacto com o suporte técnico do Ikuma.";
                        if (minhalingua.Equals("ENG"))
                        {
                            label.Text = arrayfromEntry2[13] + "\nCompany Scheduling " +
                             arrayfromEntry2[14] + "\nAppointment details:\nAppointment # " +
                             arrayfromEntry2[0] + "\nDate of appointment: " + arrayfromEntry2[12] + "\nCustomer # " + meuidonline + " Name: " +
                             meusdadosbd + "\nBooking category: Travel(" + arrayfromEntry2[14] + ")\n" +
                             "Please, be present at the address below on the date and time of checkin, if the appointment is accepted." +
                             "\r\n" + arrayfromEntry2[2] + "\r\nThank you\r\n" +
                             "Schedule data: Checkin-" + arrayfromEntry2[2] + " Class-" + arrayfromEntry2[4] + " Adults-" + arrayfromEntry2[5] +
                              " Children-" + arrayfromEntry2[6] + " During-" + arrayfromEntry2[7] + " mins Round trip-" + arrayfromEntry2[9]
                            + "\nBy: " + arrayfromEntry2[8] + " AOA\n" +
                             "\nYou can also reach the site for more detailed information.\r\n" +
                             "For technical support, start a conversation with the provider, in the chat or contact Ikuma technical support.";
                        
                        }

                        if (minhalingua.Equals("CH"))
                        {
                            label.Text = arrayfromEntry2[13] + "\n公司排班 " +
                             arrayfromEntry2[14] + "\n約會詳情：\n約會 # " +
                             arrayfromEntry2[0] + "\n預約日期：" + arrayfromEntry2[12] + "\nCustomer # " + meuidonline + " 姓名：" +
                             meusdadosbd + "\n預訂類別：旅遊(" + arrayfromEntry2[14] + ")\n" +
                             "如果預約被接受，請在入住日期和時間出現在下面的地址。" +
                             "\r\n" + arrayfromEntry2[2] + "\r\n謝謝\r\n" +
                             "調度數據：Checkin -" + arrayfromEntry2[2] + "Class -" + arrayfromEntry2[4] + "Adults -" + arrayfromEntry2[5] +
                              " Children -" + arrayfromEntry2[6] + " During -" + arrayfromEntry2[7] + " mins Round trip -" + arrayfromEntry2[9]
                            + "\n通過：" + arrayfromEntry2[8] + " AOA\n" +
                             "\n您也可以訪問該站點以獲取更多詳細信息。\r\n" +
                             "要獲得技術支持，請在聊天中與供應商開始對話或聯繫 Ikuma 技術支持。";
                        }

                        if (minhalingua.Equals("ESP"))
                        {
                            label.Text = arrayfromEntry2[13] + "\nProgramación de la empresa" +
                             arrayfromEntry2[14] + "\nDetalles de la cita:\nCita # " +
                             arrayfromEntry2[0] + "\nFecha de la cita: " + arrayfromEntry2[12] + "\nCliente # " + meuidonline + " Nombre: " +
                             meusdadosbd + "\nCategoría de reserva: Viajes(" + arrayfromEntry2[14] + ")\n" +
                             "Por favor, esté presente en la dirección a continuación en la fecha y hora del check-in, si se acepta la cita" +
                             "\r\n" + arrayfromEntry2[2] + "\r\nGracias\r\n" +
                             "Programar datos: Check-in-" + arrayfromEntry2[2] + " Clase-" + arrayfromEntry2[4] + " Adultos-" + arrayfromEntry2[5] +
                              " Hijos-" + arrayfromEntry2[6] + " Durante-" + arrayfromEntry2[7] + " mins Ida y vuelta-" + arrayfromEntry2[9]
                            + "\nPor: " + arrayfromEntry2[8] + " AOA\n" +
                             "\nTambién puede acceder al sitio para obtener información más detallada.\r\n" +
                             "Para soporte técnico, inicie una conversación con el proveedor, en el chat o comuníquese con el soporte técnico de Ikuma.";
                        
                        }

                        if (minhalingua.Equals("FR"))
                        {
                            label.Text = arrayfromEntry2[13] + "\nPlanification de l'entreprise " +
                               arrayfromEntry2[14] + "\nDétails du rendez-vous :\nRendez-vous # " +
                               arrayfromEntry2[0] + "\nDate du rendez-vous : " + arrayfromEntry2[12] + "\nNuméro client " + meuidonline + " Nom : " +
                               meusdadosbd + "\nCatégorie de réservation : Voyage(" + arrayfromEntry2[14] + ")\n" +
                               "Veuillez être présent à l'adresse ci-dessous à la date et à l'heure de l'enregistrement, si le rendez-vous est accepté." +
                               "\r\n" + arrayfromEntry2[2] + "\r\nMerci\r\n" +
                               "Données de l'horaire : Checkin-" + arrayfromEntry2[2] + "Class-" + arrayfromEntry2[4] + "Adults-" + arrayfromEntry2[5] +
                                " Enfants-" + arrayfromEntry2[6] + " Pendant-" + arrayfromEntry2[7] + " minutes Aller-retour-" + arrayfromEntry2[9]
                              + "\nPar : " + arrayfromEntry2[8] + " AOA\n" +
                               "\nVous pouvez également accéder au site pour des informations plus détaillées.\r\n" +
                               "Pour le support technique, démarrez une conversation avec le fournisseur, dans le chat ou contactez le support technique d'Ikuma.";
                        
                        }
                        StackLayout stacklayout1 = new StackLayout();
                        stacklayout1.Children.Add(label);
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) =>
                        {
                            await Browser.OpenAsync("https://ikumatransport.mypressonline.com/turismo/doturismo.php?criapdf=" + label.Text.ToString());
                        };
                        stacklayout1.GestureRecognizers.Add(tapGestureRecognizer);
                        StackLayout stackLayout2 = new StackLayout();
                        stackLayout2.Orientation = StackOrientation.Horizontal;
                        stackLayout2.Padding = 20;
                        stackLayout2.HorizontalOptions = LayoutOptions.Center;

                        StackLayout stackLayout3 = new StackLayout();
                        Image image = new Image();
                        image.Source = "aceitar.png";
                        image.WidthRequest = 30;
                        image.HeightRequest = 30;
                        stackLayout3.Children.Add(image);
                        var tapGestureRecognizer1 = new TapGestureRecognizer();
                        tapGestureRecognizer1.Tapped += (s, e) =>
                        {
                            Navigation.PushModalAsync(new Chat(arrayfromEntry2[0]));
                        };
                        stackLayout3.GestureRecognizers.Add(tapGestureRecognizer1);

                        StackLayout stackLayout4 = new StackLayout();
                        Image image1 = new Image();
                        image1.Source = "cancelar.png";
                        image1.WidthRequest = 30;
                        image1.HeightRequest = 30;
                        stackLayout4.Children.Add(image1);
                        var tapGestureRecognizer2 = new TapGestureRecognizer();
                        tapGestureRecognizer2.Tapped += async (s, e) =>
                        {
                            Uri uri1 = new Uri("https://ikumatransport.mypressonline.com/turismo/doturismo.php");
                            var postData1 = new List<KeyValuePair<string, string>>
          {
              new KeyValuePair<string, string>("cencelaagendamento", ""),
              new KeyValuePair<string, string>("id", arrayfromEntry2[0]),
              new KeyValuePair<string, string>("tipo", "agendamentosviagem")
          };

                            HttpRequestMessage req1 = new HttpRequestMessage(HttpMethod.Post, uri1);
                            req1.Content = new FormUrlEncodedContent(postData1);
                            HttpClient client1 = new HttpClient();
                            var response1 = await client1.SendAsync(req1);
                            var contents = await response1.Content.ReadAsStringAsync();
                            
                            if (minhalingua.Equals("ENG"))
                            {
                                DisplayAlert("Cancelling an appointment", "When cancelling an appointment, " +
                                "will be discounted if the provider has already accepted it.", "Ok");
                            }

                            if (minhalingua.Equals("CH"))
                            {
                                DisplayAlert("取消約會時","取消約會時，" +
                                "如果提供者已經接受","將打折");
                            }

                            if (minhalingua.Equals("ESP"))
                            {
                                DisplayAlert("Cancelar una cita", "Al cancelar una cita",  
                                "se descontará si el proveedor ya lo ha aceptado.", "Ok");
                            }

                            if (minhalingua.Equals("FR"))
                            {
                                DisplayAlert(" Annulation d’un rendez - vous ", " Lors de l’annulation d’un rendez - vous, " +
                                " sera réduit si le fournisseur l’a déjà accepté. ", " Ok ");
                            }

                            if (minhalingua.Equals("PT"))
                            {
                                DisplayAlert("Cancelamento do agendamento", "Ao cancelar um agendamento, " +
                                "será descontado caso o provedor já o tenha aceite.", "Ok");
                            }
                            buscaagendamentos();
                        };
                        stackLayout4.GestureRecognizers.Add(tapGestureRecognizer2);

                        stackLayout2.Children.Add(stackLayout3);
                        stackLayout2.Children.Add(stackLayout4);

                        stackLayout.Children.Add(stacklayout1);
                        if (!arrayfromEntry2[10].Contains("realizado") || !arrayfromEntry2[10].Contains("cancelado") || !arrayfromEntry2[10].Contains("suspenso"))
                        { stackLayout.Children.Add(stackLayout2); }
                        frame.Content = stackLayout;

                        agendamentosviagem.Children.Add(frame);
                    }
                }

            }
        }*/


        protected override bool OnBackButtonPressed()
        {
            // Alguma lógica de tratamento
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return true;
        }


        int abreomenu = 0;
        public void abremenu(Object sender, EventArgs e)
        {

            Frame frame = new Frame();
            frame.AnchorX = 60;
            frame.CornerRadius = 30;
            frame.BackgroundColor = Color.FromHex("#CCEAEAEA");
                           frame.Padding = 10;
            frame.HorizontalOptions = LayoutOptions.End;
            //"0,-200,20,200"
            StackLayout stackLayout = new StackLayout();
            stackLayout.Orientation = StackOrientation.Vertical;
            stackLayout.Margin = 10;

            Label label = new Label();
            label.Text = "Aceitar";
            label.FontFamily = "Noir Pro";
            label.FontSize = 12;
            label.TextColor = Color.FromHex("#0B2B41");
            label.FontAttributes = FontAttributes.Bold;
            label.HorizontalTextAlignment = TextAlignment.Center;

            Label label1 = new Label();
            label1.Text = "Cancelar";
            label1.FontFamily = "Noir Pro";
            label1.FontSize = 12;
            label1.TextColor = Color.FromHex("#0B2B41");
            label1.FontAttributes = FontAttributes.Bold;
            label1.HorizontalTextAlignment = TextAlignment.Center;

            Label label2 = new Label();
            label2.Text = "Agendar";
            label2.FontFamily = "Noir Pro";
            label2.FontSize = 12;
            label2.TextColor = Color.FromHex("#0B2B41");
            label2.FontAttributes = FontAttributes.Bold;
            label2.HorizontalTextAlignment = TextAlignment.Center;

            stackLayout.Children.Add(label);
            stackLayout.Children.Add(label1);
            stackLayout.Children.Add(label2);

            frame.Content = stackLayout;
            poemmenu.Children.Add(frame);
          /*  < Frame
                   CornerRadius = "30"
       BackgroundColor = "#CCEAEAEA"
                           Padding = "10"
                    HorizontalOptions = "End"
                    Margin = "0,-200,20,200" >
                        < StackLayout  Orientation = "Vertical" Margin = "10" >
                            < Label Text = "Perfil" FontFamily = "Noir Pro" FontSize = "12" TextColor = "#0B2B41" FontAttributes = "Bold"
                       HorizontalTextAlignment = "Center" />
                            < Label Text = "Guardar" FontFamily = "Noir Pro" FontSize = "12" TextColor = "#0B2B41" FontAttributes = "Bold"
                       HorizontalTextAlignment = "Center" />
                            < Label Text = "Pausar" FontFamily = "Noir Pro" FontSize = "12" TextColor = "#0B2B41" FontAttributes = "Bold"
                       HorizontalTextAlignment = "Center" />
                            < Label Text = "Encerrar" FontFamily = "Noir Pro" FontSize = "12" TextColor = "#0B2B41" FontAttributes = "Bold"
                       HorizontalTextAlignment = "Center" />
                        </ StackLayout >
                    </ Frame >*/
        }



        public void negar(Object sender, EventArgs e)
        {
            osolicitante.IsVisible = false;
        }

        public void abreagenda(Object sender, EventArgs e)
        {
           /* < DatePicker  x: Name = "nascimento" Margin = "30,0,30,0"
        TextColor = "#0B2B41" FontSize = "18" WidthRequest = "300"  FontFamily = "Noir Pro Regular" />*/
        }


    }
}