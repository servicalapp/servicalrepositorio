using IkumaTransport;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using SQLitePCL;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQliteDroid))]
namespace IkumaTransport
{
    public class SQliteDroid : Isqlite
    {
        public SQLiteConnection GetConnection()
        {
            var dbase = "ikuma.db3";
            var dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbpath, dbase);
            var connection = new SQLiteConnection(path);
            return connection;

        }
    }
}
