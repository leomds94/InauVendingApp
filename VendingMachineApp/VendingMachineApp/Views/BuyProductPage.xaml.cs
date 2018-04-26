using PayPal.Forms;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using VendingMachineApp.Models;
using VendingMachineApp.Services;
using Xamarin.Forms;

namespace VendingMachineApp
{
    public partial class BuyProductPage : INotifyPropertyChanged

    {
        public static int screenWidth;
        public static int screenHeight;
        PayPalItem purchaseItem;

        public ProductMachine boughtProd;
        public ObservableCollection<ProductMachine> Products { get; set; }

        public BuyProductPage()
        {
            Products = new ObservableCollection<ProductMachine>(RestService.Products.OrderBy(o => o.ProductMachineIndex));

            InitializeComponent();

            //this.BindingContext = Products;

            //productsView.ItemsSource = Products;

            //boughtProd = Products[0];

            //productsView.ItemSelected += (sender, args) =>
            //{
            //    boughtProd = args.SelectedItem as ProductMachine;
            //    if (boughtProd == null)
            //        return;
            //};
        }


        async void ProductClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Id == Btn0.Id)
            {
                boughtProd = Products[0];
            }
            else if(btn.Id == Btn1.Id)
            {
                boughtProd = Products[1];
            }
            else if (btn.Id == Btn2.Id)
            {
                boughtProd = Products[2];
            }
            else if (btn.Id == Btn3.Id)
            {
                boughtProd = Products[3];
            }
            else if (btn.Id == Btn4.Id)
            {
                boughtProd = Products[4];
            }
            else if (btn.Id == Btn5.Id)
            {
                boughtProd = Products[5];
            }
            else if (btn.Id == Btn6.Id)
            {
                boughtProd = Products[6];
            }
            else if (btn.Id == Btn7.Id)
            {
                boughtProd = Products[7];
            }

            purchaseItem = new PayPalItem(
                    boughtProd.Product.ProductName,
                    boughtProd.ProductMachinePrice,
                    "BRL");

            RestService connectAPI = new RestService();

            var result = await CrossPayPalManager.Current.Buy(purchaseItem, new Decimal(0), null, PaymentIntent.Authorize);
            if (result.Status == PayPalStatus.Cancelled)
            {
                Debug.WriteLine("Cancelled");
                await DisplayAlert("Cancelado", "Sua compra foi cancelada.", "OK");
            }
            else if (result.Status == PayPalStatus.Error)
            {
                Debug.WriteLine(result.ErrorMessage);
                await DisplayAlert("Falha", "Não foi possível realizar o pagamento. Verifique o limite de seu cartão.", "OK");
            }
            else if (result.Status == PayPalStatus.Successful)
            {
                if (connectAPI.SendPendingCommand(boughtProd).Result)
                {
                    Debug.WriteLine(result.ServerResponse.Response.Id);
                    await DisplayAlert("Sucesso", "Pagamento Realizado com sucesso! Retire o seu produto da máquina.", "OK");
                    await App.NavigationPage.Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Falha", "A aplicação não conseguiu contato com a máquina! Tente novamente mais tarde. (Seu dinheiro será estornado)", "OK");
                    await App.NavigationPage.Navigation.PopAsync();
                }
            }
        }

    }
}
