using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Apl.BusinessLayer.Artifacts
{

    public class BusinesHelpersApplicationException : ApplicationException
    {
        public BusinesHelpersApplicationException(string message) : base(message) { }
    }

    public static class BusinesHelpers
    {
        static string _invalidRegStr;

        public static string MakeValidFileName(this string name)
        {
            if (_invalidRegStr == null)
            {
                var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
                _invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            }

            return Regex.Replace(name, _invalidRegStr, "_");
        }

        public static void CopyDirectory(DirectoryInfo origen, DirectoryInfo destino)
        {
            if (!destino.Exists)
            {
                destino.Create();
            }

            FileInfo[] files = origen.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(Path.Combine(destino.FullName, file.Name));
            }

            // Subcarpetas
            DirectoryInfo[] dirs = origen.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                string destinoDir = Path.Combine(destino.FullName, dir.Name);
                // llamada recusriva
                CopyDirectory(dir, new DirectoryInfo(destinoDir));
            }
        }

        public static decimal[] CalcLinearRegression(decimal[] x, decimal[] y)
        {
            var n = x.Count();
            if (n != y.Count()) throw new BusinesHelpersApplicationException("Entrada no valida");

            var fY = new List<decimal>();
            //y = bx +c
            var sumX = 0m;
            var sumY = 0m;
            var sumXeY = 0m;
            var sumXal2 = 0m;
            for (var i = 0; i < n; i++)
            {
                sumX += x[i];
                sumY += y[i];
                sumXeY += x[i] * y[i];
                sumXal2 += x[i] * x[i];
            }
            var b = (sumXeY - ((sumX*sumY)/n))/(sumXal2 - ((sumX*sumX)/n));

            var ybarra = sumY/n;
            var xbarra = sumX/n;

            var c = ybarra - (b * xbarra);


            for (var i = 0; i < n; i++)
            {
                fY.Add(decimal.Round(b * x[i] + c, 2, MidpointRounding.AwayFromZero));         
            }
            return fY.ToArray();
        }


        public static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.-_ ]", "-",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }


        public static string CleanInputForCmnt(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.-_$@!%^&*()_+<> ]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }


        public static string OnlyNumber(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\d$]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        public static bool IsValid(string strIn, string regularExpression)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, regularExpression);
        }

        public static bool StringToBool(string value)
        {
            if ((value.ToUpper() == "YES") || (value.ToUpper() == "SI")) return true;
            return false;
        }

    }
}
