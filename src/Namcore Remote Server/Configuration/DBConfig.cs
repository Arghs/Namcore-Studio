namespace Namcore_Remote_Server.Configuration
{
    public static class DBConfig
    {
        private static readonly Config config = new Config("./Configs/Server.conf");

        public static string RealmDBHost = config.Read("RealmDB.Host", "");
        public static int RealmDBPort = config.Read("RealmDB.Port", 3306);
        public static string RealmDBUser = config.Read("RealmDB.User", "");
        public static string RealmDBPassword = config.Read("RealmDB.Password", "");
        public static string RealmDBDataBase = config.Read("RealmDB.Database", "");

        public static string CharDBHost = config.Read("CharDB.Host", "");
        public static int CharDBPort = config.Read("CharDB.Port", 3306);
        public static string CharDBUser = config.Read("CharDB.User", "");
        public static string CharDBPassword = config.Read("CharDB.Password", "");
        public static string CharDBDataBase = config.Read("CharDB.Database", "");

        public static string BindIP = config.Read("Bind.IP", "127.0.0.1");
        public static int BindPort = config.Read("Bind.Port", 3000);

        public static bool MySqlPooling = config.Read("MySql.Pooling", false);
        public static int MySqlMinPoolSize = config.Read("MySql.MinPoolSize", 1);
        public static int MySqlMaxPoolSize = config.Read("MySql.MaxPoolSize", 30);

        public static LogType LogLevel = (LogType) config.Read<uint>("LogLevel", 0, true);
    }
}