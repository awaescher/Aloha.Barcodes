using System;

namespace Rb.Forms.Barcode
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
