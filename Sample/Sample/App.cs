
using Xamarin.Forms;
using Sample.Pages;

namespace Sample
{
    public class App : Application
    {
        public const string MessageOnStart = "OnStart";
        public const string MessageOnSleep = "OnSleep";
        public const string MessageOnResume = "OnResume";

        public App()
        {
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            MessagingCenter.Send<App>(this, MessageOnStart);
        }

        protected override void OnSleep()
        {
            MessagingCenter.Send<App>(this, MessageOnSleep);
        }

        protected override void OnResume()
        {
            MessagingCenter.Send<App>(this, MessageOnResume);
        }
    }
}

