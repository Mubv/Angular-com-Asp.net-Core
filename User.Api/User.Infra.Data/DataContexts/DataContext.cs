using System;
using System.Data;
using System.Data.SQLite;
using User.Infra.Data.Settings;

namespace User.Infra.Data.DataContexts
{
    public class DataContext : IDisposable
    {
        public SQLiteConnection Connection;

        public DataContext()
        {
            Connection = new SQLiteConnection(Setting.ConnectionSQLite);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}