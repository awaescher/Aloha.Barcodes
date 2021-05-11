using System;
using Aloha.Barcodes.Logger;

namespace Aloha.Barcodes.Droid.Logger
{
    public static class Log
    {
        public static void Debug(this ILog log, string message) {
            Debug(log, "{0}", message);
        }

        public static void Debug(this ILog log, string message, params object[] args) {

            var prefixedMessage = String.Format("[{0}] {1}", log.GetType().Name, message);

            global::Android.Util.Log.Debug("Aloha.Barcodes", prefixedMessage, args);
        }
    }
}

