using System;
using Rb.Forms.Barcode.Pcl;
using Rb.Forms.Barcode.Pcl.Logger;
using Rb.Forms.Barcode.Droid.Logger;
using Rb.Forms.Barcode.Droid.Decoder;

using AndroidCamera = Android.Hardware.Camera;
using ZXing;
using ApxLabs.FastAndroidCamera;

#pragma warning disable 618
namespace Rb.Forms.Barcode.Droid.View
{
    public class PreviewFrameCallback : Java.Lang.Object, INonMarshalingPreviewCallback, ILog
    {

        private readonly BarcodeDecoder barcodeDecoder;
        private readonly BarcodeScanner scanner;

        public PreviewFrameCallback(BarcodeDecoder decoder, BarcodeScanner scanner)
        {
            barcodeDecoder = decoder;
            this.scanner = scanner;
        }

        async public void OnPreviewFrame(IntPtr data, AndroidCamera camera)
        {

            var buffer = new FastJavaByteArray(data);
            var previewSize = camera.GetParameters().PreviewSize;

            var decoder = barcodeDecoder.DecodeAsync(buffer, previewSize.Width, previewSize.Height);

            if (null == decoder) {
                camera.AddCallbackBuffer(buffer);
                return;
            }

            var barcode = await decoder;
            camera.AddCallbackBuffer(buffer);

            if (!string.IsNullOrWhiteSpace(barcode)) {
                scanner.Barcode = barcode;
            }
        }
    }
}
#pragma warning restore 618
