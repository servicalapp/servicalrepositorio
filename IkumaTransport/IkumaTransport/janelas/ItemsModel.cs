using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IkumaTransport.janelas
{
    public class ItemsModel
    {
        public string Description { get; set; }
        public string Date { get; set; }
        [JsonIgnore]
        public string Key { get; set; }
    }


    public class Motoristas
    {
        public string Motorista { get; set; }
        public string Localizacao { get; set; }
        public string Data { get; set; }
        [JsonIgnore]
        public string Id { get; set; }
    }

    public class Country
    {
        public string CountryImage { get; set; }
        public string CountryText { get; set; }
        public string CountryCode { get; set; }
    }
}
