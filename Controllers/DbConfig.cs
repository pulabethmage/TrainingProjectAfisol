using System.Configuration;

namespace AfiErpSystem.Controllers
{
    public static class DbConfig
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString;
            }
        }
    }
}
