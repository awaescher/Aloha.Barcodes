using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Sample.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : ContentPage
    {
        public RootPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, true);
        }

        private async void gotoScannerPage(Object sender, EventArgs e)
        {
//            var cameraPermission = new CameraPermission(CrossPermissions.Current);
//            await cameraPermission.RequestCameraPermissionIfNeeded();
            Navigation.PushAndRemovePrevious(ScannerPageControl.Instance.CreateScannerPage(), 1);
        }

    }
}
