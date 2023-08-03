using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IkumaTransport
{
    public class Registration
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string Dados { get; set; }

    }

    public class RegistraLingua
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string lingua { get; set; }

    }

    public class RegistraPerfil
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string Idonline { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Fone { get; set; }
    }
}
