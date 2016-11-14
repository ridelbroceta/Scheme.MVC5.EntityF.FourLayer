namespace Apl.UI.Facade
{
    public class WebConfigFacade
    {
        public static bool CanSend()
        {
            var rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            if (0 < rootWebConfig1.AppSettings.Settings.Count)
            {
                var customSetting =
                    rootWebConfig1.AppSettings.Settings["canSend"];
                if (null != customSetting) return bool.Parse(customSetting.Value);
                
            }
            return false;
        }

        public static string DomainPath()
        {
            var rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            if (0 < rootWebConfig1.AppSettings.Settings.Count)
            {
                var customSetting =
                    rootWebConfig1.AppSettings.Settings["domainPath"];
                if (null != customSetting) return customSetting.Value;
            }
            return "~/";
        }

        public static string TmpPath()
        {
            var rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            if (0 < rootWebConfig1.AppSettings.Settings.Count)
            {
                var customSetting =
                    rootWebConfig1.AppSettings.Settings["tmpPath"];
                if (null != customSetting) return customSetting.Value;
            }
            return "~/";
        }

        public static string Get(string key)
        {
            var rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            if (0 < rootWebConfig1.AppSettings.Settings.Count)
            {
                var customSetting =
                    rootWebConfig1.AppSettings.Settings[key];
                if (null != customSetting) return customSetting.Value;
            }
            return null;
        }


    }
}