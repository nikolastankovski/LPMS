using System.Runtime.CompilerServices;

namespace LPMS.Infrastructure.Services.SharedServices
{
    public static class Logger
    {
        public enum LogLevel { Debug, Info, Critical }


        public static void Log(string message, LogLevel logLevel = LogLevel.Info, [CallerFilePath] string callerName = "", [CallerMemberName] string memberName = "")
        {
            string caller = string.Concat(callerName.Replace(".cs", "\\"), memberName);
            LogMessage(message, caller, logLevel); 
        }

        public static void Log(Exception exception, [CallerFilePath] string callerName = "", [CallerMemberName] string memberName = "")
        {
            string caller = string.Concat(callerName.Replace(".cs", "\\"), memberName);
            var message = string.IsNullOrEmpty(exception?.InnerException?.Message) ? exception?.Message : exception.InnerException?.Message;
            LogMessage(message, caller, LogLevel.Critical);
        }

        private static void LogMessage(string? message, string caller, LogLevel logLevel)
        {
            if (string.IsNullOrEmpty(message)) return;

            var filePath = GetFilePath();
            using (StreamWriter w = File.AppendText(filePath))
            {
                CreateLog(message, w, caller, logLevel);
                w.Close();
            }
        }

        private static void CreateLog(string logMessage, TextWriter w, string caller, LogLevel logLevel)
        {
            w.WriteLine("=============================================================");
            w.WriteLine("Log Entry: {0}", DateTime.Now.ToString("dd\\/MM\\/yyyy HH:mm:ss"));
            w.WriteLine("Caller: {0}", caller);
            w.WriteLine("Warning Level: {0}", logLevel.ToString());
            w.WriteLine("-------------------------------");
            w.WriteLine(logMessage);
            w.WriteLine("=============================================================");
            w.Flush();
        }

        private static string GetFilePath()
        {
            string fileName = $"log_{DateTime.Now.Date.ToString("yyyyMMdd")}.txt";

            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\Logs", fileName));

            if (!File.Exists(filePath)) File.Create(filePath).Close();

            return filePath;
        }
    }
}
