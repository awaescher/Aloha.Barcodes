using System;
using Android.Gms.Vision;
using Aloha.Barcodes;
using JObject = Java.Lang.Object;

namespace Aloha.Barcodes.Droid
{
    public class BarcodeTrackerFactory : JObject, MultiProcessor.IFactory
    {
        private readonly BarcodeScanner barcodeScanner;

        public BarcodeTrackerFactory(BarcodeScanner barcodeScanner)
        {
            this.barcodeScanner = barcodeScanner;
        }

        public Tracker Create(JObject barcode)
        {
            return new BarcodeTracker(barcodeScanner);
        }
    }
}
