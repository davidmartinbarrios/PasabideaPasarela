using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantik.Pasarela.Helpers
{
    public static class Settings
    {
        #region propiedades
        public static string BD_DP4
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["BD_DP4"] != null)
                    return ConfigurationManager.ConnectionStrings["BD_DP4"].ConnectionString;
                else
                    return string.Empty;
            }
        }
        public static string BD_PASARELA
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["BD_PASARELA"] != null)
                    return ConfigurationManager.ConnectionStrings["BD_PASARELA"].ConnectionString;
                else
                    return string.Empty;
            }
        }
        public static string BD_DB2
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["BD_DB2"] != null)
                    return ConfigurationManager.ConnectionStrings["BD_DB2"].ConnectionString;
                else
                    return string.Empty;
            }
        }
        public static string BD_GENE
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["BD_GENE"] != null)
                    return ConfigurationManager.ConnectionStrings["BD_GENE"].ConnectionString;
                else
                    return string.Empty;
            }
        }
        public static string BD_INFRA
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["BD_INFRA"] != null)
                    return ConfigurationManager.ConnectionStrings["BD_INFRA"].ConnectionString;
                else
                    return string.Empty;
            }
        }
        public static string BD_CARGA
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["BD_CARGA"] != null)
                    return ConfigurationManager.ConnectionStrings["BD_CARGA"].ConnectionString;
                else
                    return string.Empty;
            }
        }
        #endregion
    }
}
