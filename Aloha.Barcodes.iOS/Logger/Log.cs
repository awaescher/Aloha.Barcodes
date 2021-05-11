using System;
using Aloha.Barcodes.Logger;
using IosDebug = System.Diagnostics.Debug;

namespace Aloha.Barcodes.iOS.Logger
{
    public static class Log
    {
        public static void Debug(this ILog log, string message) 
        {
            Debug(log, "{0}", message);
        }

        public static void Debug(this ILog log, string message, params object[] args) 
        {

            var prefixedMessage = String.Format("Aloha.Barcodes [{0}] {1}", log.GetType().Name, message);
            IosDebug.WriteLine(prefixedMessage, args);
        }
    }
}

