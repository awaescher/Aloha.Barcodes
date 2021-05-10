using System;

using Android.App;
using Android.OS;
using Sample;
using Xamarin.Forms.Platform.Android;
using Maui.Barcode.Droid;
using Android.Content.PM;
using Xamarin.Forms;
using AndroidCamera = Android.Hardware.Camera;
using System.Collections.Generic;
using Android.Widget;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Permission = Android.Content.PM.Permission;
using System.Threading.Tasks;

namespace Sample.Droid
{
    [Activity(Label = "Sample.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            
            CrossCurrentActivity.Current.Init(this, bundle);

            var config = new Configuration {
                // Some devices, mostly samsung, stop auto focusing as soon as one of the advanced features is enabled.
                CompatibilityMode = Build.Manufacturer.Contains("samsung"),
                Zoom = 5
            };

            BarcodeScannerRenderer.Init(config);

            LoadApplication(new App());

            Device.BeginInvokeOnMainThread(() =>
            {
                GetPermission(); 

            });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async Task GetPermission()
        {
            var context = CrossCurrentActivity.Current.AppContext;
            try
            {
             
                var statusStorage = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                var statusCamera = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);
                var statusMedia = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.MediaLibrary);
                var status = (statusStorage == PermissionStatus.Granted) &&
                             (statusCamera == PermissionStatus.Granted) && (statusMedia == PermissionStatus.Granted);

                if (!status)
                {                    

                    if ((await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage)) || (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Camera)) || (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.MediaLibrary)) )
                    {
                        //Toast.MakeText(context, "Need Storage permission to access to your photos.", ToastLength.Long).Show();
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Storage, Plugin.Permissions.Abstractions.Permission.Camera, Plugin.Permissions.Abstractions.Permission.MediaLibrary });

                    statusStorage = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                    statusCamera = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);
                    statusMedia = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.MediaLibrary);
                    status = (statusStorage == PermissionStatus.Granted) &&
                             (statusCamera == PermissionStatus.Granted) && (statusMedia == PermissionStatus.Granted);

                    if (!status)
                    {
                        Toast.MakeText(context, "Permission Denied. Can not continue, try again.", ToastLength.Long).Show();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Toast.MakeText(context, "Error. Can not continue, try again.", ToastLength.Long).Show();
            }
        }
    }
}
