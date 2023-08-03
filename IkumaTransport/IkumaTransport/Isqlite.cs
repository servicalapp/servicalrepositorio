using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IkumaTransport
{
    public interface Isqlite
    {
        SQLiteConnection GetConnection();
    }
}
