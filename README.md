<p align="center">
<img src="Alohacode.png" width="300">
</p>

# Aloha.Barcodes

## What is this?

Aloha.Barcodes is a **Microsoft MAUI & Xamarin.Forms view for scanning barcodes**.
This repository is a fork of the archived [Rb.Forms.Barcode](https://github.com/rebuy-de/rb-forms-barcode), originally developed by reBuy reCommerce GmbH.

It provides continuous scanning, aims to give high control to the user combined with high stability.

[Available via Nuget](https://www.nuget.org/packages/Aloha.Barcodes).

**Please note** that the library currently supports Android and iOS.

### Features

* Fully Xamarin.Forms compatible. Add elements on top and adapt the UI to your needs.
* Lots of configuration options, bindable properties and events: Torch control, rotation support, preview freezing and other fine grained controls.
* Build for continuous scanning!
* Utilizing [Google Play Services Vision API](https://developers.google.com/vision/) on Android for best possible barcode scanning performance.
* iOS scanner uses the AVFoundation.

## Setup

1. Install the [package via nuget](https://www.nuget.org/packages/Aloha.Barcodes) into your PCL and platform specific projects.
2. [Set the appropriate Android permissions](http://developer.android.com/guide/topics/media/camera.html#manifest) to allow your app to access the camera and flash if need be.
4. Add the registration call `BarcodeScannerRenderer.Init();` to your platform specific Main class.
4. Use the `BarcodeScanner` class in your c# or xaml code.

Example Android implementation of the Init call:


```
// MainActivity
protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);
    Forms.Init(this, bundle);

    var config = new Configuration {
        Zoom = 5
    };

    BarcodeScannerRenderer.Init(config);

    LoadApplication(new App());
}
```

Example iOS implementation of the Init call:


```
// AppDelegate
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    Forms.Init();
    BarcodeScannerRenderer.Init(
        new Configuration { 
            Barcodes = Barcode.BarcodeFormat.Ean13 | Barcode.BarcodeFormat.Ean8
        }
    );

    LoadApplication(new App());

    return base.FinishedLaunching(app, options);
}
```

## Usage

1. Create an instance of the `BarcodeScanner` class. Dont forget to give it a height and width.
2. Register an EventHandler for the `BarcodeScanner.BarcodeChanged` event to receive the detected barcodes.

For a hands-on experience it is recommended to [take a look at the sample application](#sample).

### Please note

**Since beta 0.5.0** the library only handles the most basic camera controls. The scanning starts as soon as the element is visible on screen and stops when the view element gets removed from the stack.

Given the complexity of apps there are a lot of combinations that prevent a reasonable automatic control of the camera. For example when [sleeping](Sample/Sample/Pages/ScannerPage.xaml.cs#L18)/[resuming](Sample/Sample/Pages/ScannerPage.xaml.cs#L19) the device, when the [page gets disposed](Sample/Sample/Pages/ScannerPage.xaml.cs#L74-L76) without notifying the view or another page get pushed onto the stack.

Thats why you should weave in camera control code into the logic of your app by utilizing the offered bindings. Not doing so might lead to bad performance or unexpected camera exceptions.

Android Do's:

* Disable the preview when you add a page to the navigation stack.
* Disable the camera when the page gets removed from the stack.
* Disable the camera when sleeping the device.
* Ensure that only one instance at at time is active.

### Bindable properties and events

All events are also available as `Command`s, the appropriate fields are suffixed accordingly. E.g. the command for `BarcodeChanged` event would be `BarcodeChangedCommand`.

What | Type | Description
---- | ---- | -----------
`BarcodeScanner.BarcodeChanged` | EventHandler | Raised only when the barcode text changes.
`BarcodeScanner.BarcodeDecoded` | EventHandler | Raised every time when a barcode is decoded from the preview, even if the value is the same as the previews one.
`BarcodeScanner.PreviewActivated` | EventHandler | Raised after the preview image gets active.
`BarcodeScanner.PreviewDeactivated` | EventHandler | Raised after the preview image gets deactivated.
`BarcodeScanner.CameraOpened` | EventHandler | Raised after the camera was obtained.
`BarcodeScanner.CameraReleased` | EventHandler | Raised after the camera was released.
`BarcodeScanner.Barcode` | Property | Holds the value of the last found barcode.
`BarcodeScanner.IsEnabled` | Property | If `true` opens the camera and activates the preview. `false` deactivates the preview and releases the camera.
`BarcodeScanner.PreviewActive` | Property | If `true` the preview image gets updated. `false` no preview for you!
`BarcodeScanner.BarcodeDecoder` | Property | If `true` the decoder is active and tries to decode barcodes out of the image. `false` turns the decoder off, the preview is still active but barcodes will not be decoded.
`BarcodeScanner.Torch` | Property | Controls the camera flashlight if available and accessible. `true` sets the camera to torch mode (always on), `false` turns the flashlight off.

### Configuration

Configuration can be applied by passing a `Configuration` object to the `BarcodeScannerRenderer.Init()` method. As the available options are platform specific, the configuration has to be done in the according platform solution. The corresponding [Android](Aloha.Barcodes.Droid/Configuration.cs) class documentation should give you a solid understanding of the available options.

By default the compatibility mode is enabled to ensure the highest device compatibility.

Simple example:

    var config = new Configuration
    {
        // Some devices, mostly samsung, stop auto focusing as soon as one of the advanced features is enabled.
        CompatibilityMode = Build.Manufacturer.Contains("samsung")
    };

    BarcodeScannerRenderer.Init(config);

### Debugging

Aloha.Barcodes provides you with a tremendous amount of debug information, so check your application log if anything goes wrong:

```
[Aloha.Barcodes] [BarcodeScannerRenderer] OnElementChanged
[Aloha.Barcodes] [BarcodeScannerRenderer] OnElementPropertyChanged
[Aloha.Barcodes] [BarcodeScannerRenderer] SurfaceCreated
[Aloha.Barcodes] [BarcodeScannerRenderer] SurfaceChanged
[Aloha.Barcodes] [CameraConfigurator] Focus Mode [continuous-picture]
[Aloha.Barcodes] [CameraConfigurator] Scene Mode [auto]
[Aloha.Barcodes] [CameraConfigurator] Metering area [True]
[Aloha.Barcodes] [CameraConfigurator] Focusing area [True]
[Aloha.Barcodes] [CameraConfigurator] Video stabilization [True]
[Aloha.Barcodes] [CameraConfigurator] White balance [auto]
[Aloha.Barcodes] [BarcodeScannerRenderer] OnElementPropertyChanged
[ScannerView] OnBarcodeChanged [886970911399 - UpcA]
[ScannerView] OnBarcodeDecoded [886970911399 - UpcA]
Decoded barcode [886970911399 - UpcA]
[Aloha.Barcodes] [BarcodeScannerRenderer] SurfaceDestroyed
[Aloha.Barcodes] [BarcodeScannerRenderer] OnElementPropertyChanged
[Aloha.Barcodes] [BarcodeScannerRenderer] Enabled [False]
[Aloha.Barcodes] [BarcodeScannerRenderer] OnElementPropertyChanged
[Aloha.Barcodes] [BarcodeScannerRenderer] Disposing
```

### FAKE options / Tasks

Execute `bin/fake <taskname>` to run a task or `bin/fake --<optionname>` for fake cli options. First run `bin/fake install`.

(We are currently only supporting OSX to build the solution with FAKE. Feel free to add support for other platforms)

Available tasks:

```
* Restore
  Clean solution and afterwards restore all packages

* Gradlew
  Build Camera Source with Gradle

* Build
  Build all projects of the solution

```

# Build the aar files

On Windows, use cmd.exe

```
SET JAVA_HOME="C:\Program Files\Java\jdk1.8.0_211"
SET ANDROID_HOME=C:\Users\YOURUSERNAME\AppData\Local\Android\Sdk
gradlew clean build
```

## Sample

There is a [full working sample](Sample/) in the github repository that should give you a headstart. The relevant code is included in the PCL part of the project. The sample is part of the project solution.

![Android sample](sample.jpg)

---

## Attributions

The [palm tree icon](https://www.flaticon.com/free-icon/palm-tree_3802150) was made by [Freepik](https://www.freepik.com) from [Flaticon](https://www.flaticon.com/).
