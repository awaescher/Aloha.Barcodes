using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Aloha.Barcodes.iOS;
using System.Collections.Generic;
using Aloha.Barcodes;

namespace Sample.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Forms.Init();
			BarcodeScannerRenderer.Init(
				new Configuration
				{
					Barcodes = new List<Barcode.BarcodeFormat> {
						Barcode.BarcodeFormat.Ean13,
						Barcode.BarcodeFormat.Ean8
					}
				}
			);

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
