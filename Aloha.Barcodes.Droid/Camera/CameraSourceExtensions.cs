using System;
using Android.Gms.Vision;
using Aloha.Barcodes.Droid.View;
using AndroidCamera = Android.Hardware.Camera;
using RebuyCameraSource = Com.Rebuy.Play.Services.Vision.CameraSource;


namespace Aloha.Barcodes.Droid.Camera
{
    public static class CameraSourceExtensions
    {
        public static bool AutoFocusModeEnabled(this RebuyCameraSource cameraSource)
        {
            return cameraSource.Camera?.GetParameters().FocusMode == AndroidCamera.Parameters.FocusModeAuto;
        }

        public static void AutoFocus(this RebuyCameraSource cameraSource, AutoFocusCallback callback)
        {
            cameraSource.Camera?.AutoFocus(callback);
        }
    }
}

