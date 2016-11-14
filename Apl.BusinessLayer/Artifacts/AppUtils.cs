using System;

namespace Apl.BusinessLayer.Artifacts
{
    public class AppUtils
    {
        private const string BaseName = "Model";

        public static string AppFolder
        {
            get { return string.Format(@"{0}", AppDomain.CurrentDomain.BaseDirectory); }
        }

        public static string LogPath
        {
            get { return string.Format(@"{0}Logger\", AppFolder); }
        }

        public static string LogErrorName
        {
            get { return string.Format(@"{0}_{1:yyyy-MM-dd}.err", BaseName, DateTime.Now); }
        }
    }
}
