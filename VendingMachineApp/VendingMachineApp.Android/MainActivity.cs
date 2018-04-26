using Android.App;
using Android.Content.PM;
using Android.OS;
using ZXing.Mobile;
using Android.Content;
using PayPal.Forms;
using PayPal.Forms.Abstractions.Enum;
using PayPal.Forms.Abstractions;
using System.Threading;
//using Xamarin.PayPal.Android;

namespace VendingMachineApp.Droid
{
    [Activity(Label = "VendingMachineApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Thread startQRReader;
        Thread startPayPal;


        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            startQRReader = new Thread(StartQRServer);
            startQRReader.Start();

            startPayPal = new Thread(StartPayPalServer);
            startPayPal.Start();

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            //PayPalManagerImplementation.Manager.Destroy();
            base.OnDestroy();
        }

        public void StartQRServer()
        {
            MobileBarcodeScanner.Initialize(Application);
        }

            public void StartPayPalServer()
        {
            CrossPayPalManager.Init(new PayPalConfiguration(
                   PayPalEnvironment.NoNetwork,
                   "ARK24blY4EAotzNgENVy9-xdcJsyaLCvpebQkbdfCRpDTUNs2DpgcTqED-HPOpJtCOuf0LU62vCEpqXk"
                   )
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "Inautec Company",
                //Your privacy policy Url
                MerchantPrivacyPolicyUri = "https://www.inautec.com.br/privacy",
                //Your user agreement Url
                MerchantUserAgreementUri = "https://www.inautec.com.br/legal",

                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                ShippingAddressOption = ShippingAddressOption.None,

                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "pt-br",

                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "55",
            });
        }
    }
}

