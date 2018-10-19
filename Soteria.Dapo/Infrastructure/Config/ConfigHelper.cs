namespace Soteria.DataComponents.Infrastructure
{
    public static class ConfigHelper
    {
        public static string GetAppSettingByKey(string appConfigKey)
        {
            string value = string.Empty;
            if (System.Configuration.ConfigurationManager.AppSettings[appConfigKey] != null)
                value = System.Configuration.ConfigurationManager.AppSettings[appConfigKey].ToString();
            return value;
        }
        public static string GetDefaultConnectionStringName()
        {
            return GetAppSettingByKey("DefaultConnectionString");
        }
        public static string GetConnectionStringByName(string connectionStringName)
        {
            string value = string.Empty;
            if (System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName] != null)
                value = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ToString();
            return value;
        }
        public static string GetDefaultConnectionString()
        {
            return GetConnectionStringByName(GetDefaultConnectionStringName());
        }
    }
}