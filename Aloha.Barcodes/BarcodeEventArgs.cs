using System;

namespace Aloha.Barcodes
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
