﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IkumaTransport.janelas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomView : ContentView
    {
        /// <summary>
        ///Variable to store country names
        /// </summary>
        Country _country;

        int i;
        public CustomView(Country country)
        {
            i = 0;
            _country = country;
            InitializeComponent();

            foreach (char count in country.CountryImage)
            {
                i++;
            }

            _country.CountryText = country.CountryImage.Remove(i - 4);

            switch (_country.CountryText)
            {
                case "Afeganistao": country.CountryCode = "+93"; break;
                case "AfricadoSul": country.CountryCode = "+27"; break;
                case "Albania": country.CountryCode = "+355"; break;
                case "Alemanha": country.CountryCode = "+49"; break;
                case "Andorra": country.CountryCode = "+376"; break;
                case "Angola": country.CountryCode = "+244"; break;
                case "Anguila": country.CountryCode = "+1"; break;
                case "Antigua": country.CountryCode = "+1"; break;
                case "ArabiaSaudita": country.CountryCode = "+966"; break;
                case "Argelia": country.CountryCode = "+213"; break;
                case "Argentina": country.CountryCode = "+54"; break;
                case "Armenia": country.CountryCode = "+374"; break;
                case "Aruba": country.CountryCode = "+297"; break;
                case "Australia": country.CountryCode = "+61"; break;
                case "Austria": country.CountryCode = "+43"; break;
                case "Azerbaijao": country.CountryCode = "+994"; break;
                case "Bahamas": country.CountryCode = "+1"; break;
                case "Bangladexe": country.CountryCode = "+880"; break;
                case "Barbados": country.CountryCode = "+1"; break;
                case "Barem": country.CountryCode = "+973"; break;
                case "Belgica": country.CountryCode = "+32"; break;
                case "Belize": country.CountryCode = "+501"; break;
                case "Benim": country.CountryCode = "+229"; break;
                case "Bermudas": country.CountryCode = "+1"; break;
                case "Bielorrússia": country.CountryCode = "+375"; break;
                case "Bolivia": country.CountryCode = "+591"; break;
                case "Bonaire": country.CountryCode = "+599"; break;
                case "BosniaeHerzegovina": country.CountryCode = "+387"; break;
                case "Botsuana": country.CountryCode = "+267"; break;
                case "Brasil": country.CountryCode = "+55"; break;
                case "Brunei": country.CountryCode = "+673"; break;
                case "Bulgaria": country.CountryCode = "+359"; break;
                case "BurquinaFasso": country.CountryCode = "+226"; break;
                case "Burundi": country.CountryCode = "+257"; break;
                case "Butao": country.CountryCode = "+975"; break;
                case "CaboVerde": country.CountryCode = "+238"; break;
                case "Camaroes": country.CountryCode = "+237"; break;
                case "Camboja": country.CountryCode = "+855"; break;
                case "Canada": country.CountryCode = "+1"; break;
                case "Catar": country.CountryCode = "+974"; break;
                case "Cazaquistao": country.CountryCode = "+997"; break;
                case "Chade": country.CountryCode = "+235"; break;
                case "Chile": country.CountryCode = "+56"; break;
                case "China": country.CountryCode = "+86"; break;
                case "Chipre": country.CountryCode = "+357"; break;
                case "Colombia": country.CountryCode = "+57"; break;
                case "Comores": country.CountryCode = "+269"; break;
                case "CongoBrazavile": country.CountryCode = "+242"; break;
                case "CongoQuinxassa": country.CountryCode = "+243"; break;
                case "CoreiadoNorte": country.CountryCode = "+850"; break;
                case "CoreiadoSul": country.CountryCode = "+82"; break;
                case "CostaRica": country.CountryCode = "+506"; break;
                case "CostadoMarfim": country.CountryCode = "+225"; break;
                case "Croacia": country.CountryCode = "+385"; break;
                case "Cuba": country.CountryCode = "+53"; break;
                case "Curacao": country.CountryCode = "+599"; break;
                case "Dinamarca": country.CountryCode = "+45"; break;
                case "Djibuti": country.CountryCode = "+253"; break;
                case "Dominica": country.CountryCode = "+1"; break;
                case "Egipto": country.CountryCode = "+211"; break;
                case "ElSalvador": country.CountryCode = "+503"; break;
                case "EmiradosArabesUnidos": country.CountryCode = "+971"; break;
                case "Equador": country.CountryCode = "+593"; break;
                case "Eritreia": country.CountryCode = "+291"; break;
                case "Eslovaquia": country.CountryCode = "+421"; break;
                case "Eslovenia": country.CountryCode = "+386"; break;
                case "Espanha": country.CountryCode = "+34"; break;
                case "Essuatini": country.CountryCode = "+268"; break;
                case "EstadosFederadosdaMicronesia": country.CountryCode = "+691"; break;
                case "EstadosUnidos": country.CountryCode = "+1"; break;
                case "Estonia": country.CountryCode = "+372"; break;
                case "Etiopia": country.CountryCode = "+251"; break;
                case "Fiji": country.CountryCode = "+679"; break;
                case "Filipinas": country.CountryCode = "+63"; break;
                case "Finlandia": country.CountryCode = "+358"; break;
                case "FlagofAscension": country.CountryCode = "+247"; break;
                case "Franca": country.CountryCode = "+33"; break;
                case "Gabao": country.CountryCode = "+241"; break;
                case "Gambia": country.CountryCode = "+220"; break;
                case "Gana": country.CountryCode = "+233"; break;
                case "Georgia": country.CountryCode = "+995"; break;
                case "Gibraltar": country.CountryCode = "+350"; break;
                case "Granada": country.CountryCode = "+1"; break;
                case "Grecia": country.CountryCode = "+30"; break;
                case "Groenlandia": country.CountryCode = "+299"; break;
                case "Guadalupe": country.CountryCode = "+590"; break;
                case "Guam": country.CountryCode = "+1"; break;
                case "Guatemala": country.CountryCode = "+502"; break;
                case "Guiana": country.CountryCode = "+592"; break;
                case "GuianaFrancesa": country.CountryCode = "+594"; break;
                case "Guine": country.CountryCode = "+224"; break;
                case "GuineBissau": country.CountryCode = "+245"; break;
                case "GuineEquatorial": country.CountryCode = "+240"; break;
                case "Haiti": country.CountryCode = "+509"; break;
                case "Honduras": country.CountryCode = "+504"; break;
                case "HongKong": country.CountryCode = "+852"; break;
                case "Hungria": country.CountryCode = "+36"; break;
                case "Iemen": country.CountryCode = "+967"; break;
                case "IlhaChristmas": country.CountryCode = "+61"; break;
                case "IlhaNorfolque": country.CountryCode = "+672"; break;
                case "IlhasCaima": country.CountryCode = "+1"; break;
                case "IlhasCocos": country.CountryCode = "+61"; break;
                case "IlhasCook": country.CountryCode = "+682"; break;
                case "IlhasFeroe": country.CountryCode = "+298"; break;
                case "IlhasGeorgiadoSul": country.CountryCode = "+500"; break;
                case "IlhasMalvinas": country.CountryCode = "+500"; break;
                case "IlhasMarianasdoNorte": country.CountryCode = "+1"; break;
                case "IlhasMarshall": country.CountryCode = "+692"; break;
                case "IlhasPitcairn": country.CountryCode = "+64"; break;
                case "IlhasSalomao": country.CountryCode = "+677"; break;
                case "IlhasVirgensAmericanas": country.CountryCode = "+1"; break;
                case "IlhasVirgensBritanicas": country.CountryCode = "+1"; break;
                case "India": country.CountryCode = "+91"; break;
                case "Indonesia": country.CountryCode = "+62"; break;
                case "Ira": country.CountryCode = "+98"; break;
                case "Iraque": country.CountryCode = "+964"; break;
                case "Irlanda": country.CountryCode = "+353"; break;
                case "Islandia": country.CountryCode = "+354"; break;
                case "Israel": country.CountryCode = "+972"; break;
                case "Italia": country.CountryCode = "+39"; break;
                case "Jamaica": country.CountryCode = "+1"; break;
                case "Japao": country.CountryCode = "+81"; break;
                case "Jordania": country.CountryCode = "+962"; break;
                case "Kosovo": country.CountryCode = "+383"; break;
                case "Kuwait": country.CountryCode = "+965"; break;
                case "Laos": country.CountryCode = "+856"; break;
                case "Lesoto": country.CountryCode = "+266"; break;
                case "Letonia": country.CountryCode = "+371"; break;
                case "Libano": country.CountryCode = "+961"; break;
                case "Liberia": country.CountryCode = "+231"; break;
                case "Libia": country.CountryCode = "+218"; break;
                case "Liechtenstein": country.CountryCode = "+423"; break;
                case "Lituania": country.CountryCode = "+370"; break;
                case "Luxemburgo": country.CountryCode = "+352"; break;
                case "Macau": country.CountryCode = "+853"; break;
                case "MacedoniadoNorte": country.CountryCode = "+389"; break;
                case "Madagascar": country.CountryCode = "+261"; break;
                case "Maiote": country.CountryCode = "+262"; break;
                case "Malasia": country.CountryCode = "+60"; break;
                case "Malaui": country.CountryCode = "+265"; break;
                case "Maldivas": country.CountryCode = "+960"; break;
                case "Mali": country.CountryCode = "+223"; break;
                case "Malta": country.CountryCode = "+356"; break;
                case "Marrocos": country.CountryCode = "+212"; break;
                case "Martinica": country.CountryCode = "+596"; break;
                case "Mauricia": country.CountryCode = "+230"; break;
                case "Mauritania": country.CountryCode = "+222"; break;
                case "Mexico": country.CountryCode = "+52"; break;
                case "Mianmar": country.CountryCode = "+95"; break;
                case "Mocambique": country.CountryCode = "+258"; break;
                case "Moldavia": country.CountryCode = "+273"; break;
                case "Monaco": country.CountryCode = "+377"; break;
                case "Mongolia": country.CountryCode = "+976"; break;
                case "Monserrate": country.CountryCode = "+1"; break;
                case "Montenegro": country.CountryCode = "+382"; break;
                case "Namibia": country.CountryCode = "+264"; break;
                case "Nauru": country.CountryCode = "+674"; break;
                case "Nepal": country.CountryCode = "+977"; break;
                case "Nicaragua": country.CountryCode = "+505"; break;
                case "Niger": country.CountryCode = "+227"; break;
                case "Nigeria": country.CountryCode = "+234"; break;
                case "Niue": country.CountryCode = "+683"; break;
                case "Noruega": country.CountryCode = "+47"; break;
                case "NovaCaledonia": country.CountryCode = "+687"; break;
                case "NovaZelandia": country.CountryCode = "+64"; break;
                case "Oma": country.CountryCode = "+968"; break;
                case "PaisesBaixos": country.CountryCode = "+31"; break;
                case "Palau": country.CountryCode = "+680"; break;
                case "Palestina": country.CountryCode = "+970"; break;
                case "Panama": country.CountryCode = "+507"; break;
                case "PapuaNovaGuine": country.CountryCode = "+675"; break;
                case "Paquistao": country.CountryCode = "+92"; break;
                case "Paraguai": country.CountryCode = "+595"; break;
                case "Peru": country.CountryCode = "+51"; break;
                case "PolinesiaFrancesa": country.CountryCode = "+689"; break;
                case "Polonia": country.CountryCode = "+48"; break;
                case "PortoRico": country.CountryCode = "+1"; break;
                case "Portugal": country.CountryCode = "+351"; break;
                case "Quenia": country.CountryCode = "+254"; break;
                case "Quirguistao": country.CountryCode = "+996"; break;
                case "Quiribati": country.CountryCode = "+686"; break;
                case "ReinoUnido": country.CountryCode = "+44"; break;
                case "RepúblicaCentroAfricana": country.CountryCode = "+236"; break;
                case "RepúblicaDominicana": country.CountryCode = "+1"; break;
                case "Reuniao": country.CountryCode = "+262"; break;
                case "Romenia": country.CountryCode = "+40"; break;
                case "Ruanda": country.CountryCode = "+250"; break;
                case "Rússia": country.CountryCode = "+7"; break;
                case "SaaraOcidental": country.CountryCode = "+212"; break;
                case "Saba": country.CountryCode = "+599"; break;
                case "Samoa": country.CountryCode = "+685"; break;
                case "SamoaAmericana": country.CountryCode = "+1"; break;
                case "SantaHelena": country.CountryCode = "+290"; break;
                case "SantaLúcia": country.CountryCode = "+1"; break;
                case "SantoEustaquio": country.CountryCode = "+599"; break;
                case "SaoBartolomeu": country.CountryCode = "+590"; break;
                case "SaoCristovaoeNeves": country.CountryCode = "+1"; break;
                case "SaoMarinho": country.CountryCode = "+378"; break;
                case "SaoMartinho": country.CountryCode = "+590"; break;
                case "SaoPedro": country.CountryCode = "+508"; break;
                case "SaoTomeePrincipe": country.CountryCode = "+239"; break;
                case "SaoVicenteeGranadinas": country.CountryCode = "+1"; break;
                case "Seicheles": country.CountryCode = "+248"; break;
                case "Senegal": country.CountryCode = "+221"; break;
                case "SeriLanca": country.CountryCode = "+94"; break;
                case "SerraLeoa": country.CountryCode = "+232"; break;
                case "Servia": country.CountryCode = "+381"; break;
                case "Singapura": country.CountryCode = "+65"; break;
                case "Siria": country.CountryCode = "+963"; break;
                case "Somalia": country.CountryCode = "+252"; break;
                case "Sudao": country.CountryCode = "+249"; break;
                case "SudaodoSul": country.CountryCode = "+212"; break;
                case "Suecia": country.CountryCode = "+46"; break;
                case "Suica": country.CountryCode = "+41"; break;
                case "Suriname": country.CountryCode = "+597"; break;
                case "Tailandia": country.CountryCode = "+66"; break;
                case "Taiwan": country.CountryCode = "+886"; break;
                case "Tajiquistao": country.CountryCode = "+992"; break;
                case "Tanzania": country.CountryCode = "+255"; break;
                case "Tchequia": country.CountryCode = "+420"; break;
                case "TerritorioAntarticoAustraliano": country.CountryCode = "+672"; break;
                case "TerritorioBritanicodoOceanoIndico": country.CountryCode = "+246"; break;
                case "TimorLeste": country.CountryCode = "+670"; break;
                case "Togo": country.CountryCode = "+228"; break;
                case "Tonga": country.CountryCode = "+676"; break;
                case "Toquelau": country.CountryCode = "+690"; break;
                case "TrindadeeTobago": country.CountryCode = "+1"; break;
                case "TristaodaCunha": country.CountryCode = "+290"; break;
                case "Tunisia": country.CountryCode = "+216"; break;
                case "TurCaicos": country.CountryCode = "+20"; break;
                case "Turcomenistao": country.CountryCode = "+993"; break;
                case "Turquia": country.CountryCode = "+90"; break;
                case "Tuvalu": country.CountryCode = "+688"; break;
                case "Ucrania": country.CountryCode = "+380"; break;
                case "Uganda": country.CountryCode = "+256"; break;
                case "Uruguai": country.CountryCode = "+598"; break;
                case "Uzbequistao": country.CountryCode = "+998"; break;
                case "Vanuatu": country.CountryCode = "+678"; break;
                case "Vaticano": country.CountryCode = "+379"; break;
                case "Venezuela": country.CountryCode = "+58"; break;
                case "Vietna": country.CountryCode = "+84"; break;
                case "WalliseFutuna": country.CountryCode = "+681"; break;
                case "Zambia": country.CountryCode = "+260"; break;
                case "Zimbabue": country.CountryCode = "+263"; break;

                default:
                    _country.CountryCode = "+244";
                    break;
            }
            this.BindingContext = _country;
        }
    }
}