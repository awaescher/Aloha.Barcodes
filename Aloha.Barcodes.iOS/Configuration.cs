using System;
using System.Collections.Generic;
using RebuyBarcode = Aloha.Barcodes.Barcode;

namespace Aloha.Barcodes.iOS
{
    public class Configuration
    {
        /// <summary>
        /// List of barcode types the decoder should look out for.
        /// Its recommended to narrow the list down to increase decoder performance.
        /// If no format is specified all available formats will be registered.
        /// </summary>
        public IList<RebuyBarcode.BarcodeFormat> Barcodes { set; get; }

        public Configuration()
        {
            Barcodes = new List<RebuyBarcode.BarcodeFormat> {
                RebuyBarcode.BarcodeFormat.Code128, 
                RebuyBarcode.BarcodeFormat.Code39, 
                RebuyBarcode.BarcodeFormat.Code93, 
                RebuyBarcode.BarcodeFormat.DataMatrix,
                RebuyBarcode.BarcodeFormat.Ean13,
                RebuyBarcode.BarcodeFormat.Ean8, 
                RebuyBarcode.BarcodeFormat.Itf, 
                RebuyBarcode.BarcodeFormat.Pdf417,
                RebuyBarcode.BarcodeFormat.QrCode, 
                RebuyBarcode.BarcodeFormat.UpcE
            };
        }
    }
}

