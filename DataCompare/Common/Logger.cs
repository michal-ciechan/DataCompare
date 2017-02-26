using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompare.Common
{
    public class Log
    {
        public static ILogger Instance { get; set; } = new ConsoleLogger();

        public static void Warn(string msg)
        {
            Instance?.LogWarning(msg);
        }
    }

    public abstract class Logger : ILogger
    {
        public enum Level
        {
            Fatal = 0,
            Error = 1,
            Warning = 2,
            Info = 3,
            Debug = 4,
            Trace = 5,
            All = 6
        }
        public static Level LogLevel = Level.Warning;

        public abstract void Log(string message);

        public void LogInfo(string message)
        {
            if(LogLevel >= Level.Info)
                Log(message);
        }
        public void LogWarning(string message)
        {
            if(LogLevel >= Level.Warning)
                Log(message);
        }
    }

    class ConsoleLogger : Logger
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public interface ILogger
    {
        void LogWarning(string message);
        void LogInfo(string message);
    }
}
