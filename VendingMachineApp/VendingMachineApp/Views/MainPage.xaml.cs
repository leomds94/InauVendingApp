using System;
using System.Collections.Generic;
using VendingMachineApp.Services;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VendingMachineApp
{
    public partial class MainPage : ContentPage
    {
        protected string qrID;

        //Page Elements -> First element is the indicator


        public MainPage()
        {
            InitializeComponent();
            
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {

            var scanPage = new ZXingScannerPage();

            RestService service = new RestService();

            await App.NavigationPage.Navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.NavigationPage.Navigation.PopAsync();

                    LoadingScreen(true);

                    qrID = result.Text;
                    //string qrParcel = qrID.Substring(0, 56);
                    if (qrID  == "http://vendinginautec.azurewebsites.net/api/machinesapi/1")
                    {
                        service.Url = qrID;

                        await service.RefreshDataAsync();

                        await App.NavigationPage.Navigation.PushAsync(new BuyProductPage());
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Este QR code não corresponde com a de uma de nossas Vending Machines.", "OK");
                    }
                        
                    LoadingScreen(false);
                });
            };
        }

        void LoadingScreen(Boolean b)
        {
            if(b == true)
            {
                mainScn.IsVisible = true;
                actIndicator.IsRunning = true;
                actScreen.IsVisible = true;
            }
            else
            {
                mainScn.IsVisible = false;
                actIndicator.IsRunning = false;
                actScreen.IsVisible = false;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            LoadingScreen(true);

            RestService service = new RestService();
            service.Url = "http://vendinginautec.azurewebsites.net/api/machinesapi/1";
            await service.RefreshDataAsync();

            await App.NavigationPage.Navigation.PushAsync(new BuyProductPage());

            LoadingScreen(false);
        }
    }
}
