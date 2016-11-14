using System;
using System.IO;
using System.Text;

namespace Apl.BusinessLayer.Artifacts
{
    public static class Logger
    {
        private static readonly string LogFilePath;

        static Logger()
        {
            LogFilePath = AppUtils.LogPath;
            if (!Directory.Exists(LogFilePath))
            {
                Directory.CreateDirectory(LogFilePath);
            }
        }

        private static string FormatError(ApplicationException exception)
        {
            var error = string.Format("{0}\n\nHora: {1:HH:mm:ss}\n\nModulo: {2}\n\nTipo: {3}\n\nMensaje: {4}\n\nMensaje Interno: {5}",
                string.Empty.PadLeft(30, '='),
                DateTime.Now,
                exception.Source,
                exception.GetType(),
                exception.Message,
                exception.InnerException == null ? @"Mensaje vacio" : exception.InnerException.Message
            );
            return error;
        }

        public static void Log(string message)
        {
            var logFileName = AppUtils.LogErrorName;
            var fLog = File.AppendText(string.Format(@"{0}{1}", LogFilePath, logFileName));
            fLog.WriteLine("{0}", message);
            fLog.Close();
        }

        public static void LogApplicationException(ApplicationException ApplicationException)
        {
            Log(FormatError(ApplicationException));
        }
    }

    public class StringLogger
    {
        private readonly StringBuilder _log;

        public StringLogger()
        {
            _log = new StringBuilder();
        }

        private static string FormatError(Exception exception)
        {
            var error = string.Format("{0}\n\nHora: {1:HH:mm:ss}\n\nModulo: {2}\n\nTipo: {3}\n\nMensaje: {4}\n\nMensaje Interno: {5}",
                string.Empty.PadLeft(30, '='),
                DateTime.Now,
                exception.Source,
                exception.GetType(),
                exception.Message,
                exception.InnerException == null ? @"Mensaje vacio" : exception.InnerException.Message
            );
            return error;
        }

        public void Log(string message)
        {
            _log.Append(string.Format("{0}\n\n", message));
        }

        public void OpenMark()
        {
            var mark = string.Format("Hora: {0:HH:mm:ss}\n\n", DateTime.Now);
            _log.Append(mark);
        }

        public void CloseMark()
        {
            var mark = string.Format("{0}\n\n", string.Empty.PadLeft(30, '='));
            _log.Append(mark);
        }

        public void LogException(Exception exception)
        {
           Log(FormatError(exception));
        }

        public string Get()
        {
            return _log.ToString();
        }

        public bool IsEmpty { get { return _log.Length == 0; }  }
    }
}
