using System.IO;

namespace User.Infra.Data.Settings
{
    public class Setting
    {
        private static string DiretorioBanco = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6).Replace("User.Api\\bin\\Debug\\netcoreapp2.1", "User.Infra.Data\\BancoSQLite\\Usuarios.db");

        public static string ConnectionSQLite = @"Data Source="+ DiretorioBanco + "; Version=3;";
    }
}