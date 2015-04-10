﻿using System;
using System.Collections.Generic;
using Rb.Forms.Barcode.Pcl.Logger;
using Rb.Forms.Barcode.Droid.Logger;

using AndroidCamera = Android.Hardware.Camera;

#pragma warning disable 618
namespace Rb.Forms.Barcode.Droid.Camera
{
    public class CameraConfigurator : ILog
    {
        private readonly RbConfig config;

        public CameraConfigurator(RbConfig config)
        {
            this.config = config;
            
        }

        public AndroidCamera Configure(AndroidCamera camera)
        {
            var parameters = camera.GetParameters();

            camera.CancelAutoFocus();

            var focusMode = determineFocusMode(camera);
            this.Debug("Focus Mode [{0}]", focusMode);
            parameters.FocusMode = focusMode;

            if (isPickyDevice()) {
                this.Debug("Used device is marked as picky. Skipping detailed configuration to ensure function compatibility.");
            }

            if (!isPickyDevice()) {
                if (config.SceneMode) {
                    var sceneMode = determineSceneMode(camera);
                    this.Debug("Scene Mode [{0}]", sceneMode);
                    parameters.SceneMode = sceneMode;
                }

                if (config.MeteringAreas && parameters.MaxNumMeteringAreas > 0) {
                    this.Debug("Metering area [{0}]", (parameters.MaxNumMeteringAreas > 0).ToString());
                    parameters.MeteringAreas = createAreas();
                }

                if (config.FocusAreas && parameters.MaxNumFocusAreas > 0) {
                    this.Debug("Focusing area [{0}]", (parameters.MaxNumFocusAreas > 0).ToString());
                    parameters.FocusAreas = createAreas();
                }

                if (config.VideoStabilization && parameters.IsVideoStabilizationSupported) {
                    this.Debug("Video stabilization [{0}]", parameters.IsVideoStabilizationSupported.ToString());
                    parameters.VideoStabilization = true;
                }

                if (config.WhiteBalance) {
                    var whiteBalance = determineWhiteBalance(camera);
                    this.Debug("White balance [{0}]", whiteBalance);
                    parameters.WhiteBalance = whiteBalance;
                }
            }

            camera.SetParameters(parameters);

            return camera;
        }

        private string determineFocusMode(AndroidCamera camera)
        {
            var p = camera.GetParameters();

            foreach (var mode in config.FocusModes) {
                if (p.SupportedFocusModes.Contains(mode)) {
                    return mode;
                }
            }

            return "";
        }

        private string determineSceneMode(AndroidCamera camera)
        {
            var p = camera.GetParameters();

            if (p.SupportedSceneModes.Contains(AndroidCamera.Parameters.SceneModeBarcode)) {
                return AndroidCamera.Parameters.SceneModeBarcode;
            }

            return AndroidCamera.Parameters.SceneModeAuto;
        }

        private string determineWhiteBalance(AndroidCamera camera)
        {
            var p = camera.GetParameters();

            if (p.SupportedWhiteBalance.Contains(AndroidCamera.Parameters.WhiteBalanceAuto)) {
                return AndroidCamera.Parameters.WhiteBalanceAuto;
            }

            return "";
        }

        private List<AndroidCamera.Area> createAreas()
        {
            var area = new AndroidCamera.Area(new global::Android.Graphics.Rect(-1000, -400, 1000, 400), 1000);

            return new List<AndroidCamera.Area>() {
                area
            };
        }

        private bool isPickyDevice()
        {
            return config.PickyDeviceDetection && Android.OS.Build.Manufacturer.Contains("samsung");
        }
    }
}
#pragma warning restore 618