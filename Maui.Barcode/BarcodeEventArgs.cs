using System;

namespace Maui.Barcode
{
   public class BarcodeEventArgs : EventArgs
   {
        public Barcode Barcode { get; private set; }
        
        public BarcodeEventArgs(Barcode barcode)
        {
            Barcode = barcode;
        }
   }
}
