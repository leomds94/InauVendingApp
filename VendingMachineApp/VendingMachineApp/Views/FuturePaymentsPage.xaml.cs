using PayPal.Forms;
using PayPal.Forms.Abstractions.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VendingMachineApp
{

    public partial class FuturePaymentsPage : ContentPage
    {
        public static string clientMetaData;

        public FuturePaymentsPage()
        {
            InitializeComponent();
        }

        async private void PayPalFuturePaymentButton_Clicked(object sender, EventArgs e)
        {
            var result = await CrossPayPalManager.Current.RequestFuturePayments();
            if (result.Status == PayPalStatus.Cancelled)
            {
                Debug.WriteLine("Cancelled");
            }
            else if (result.Status == PayPalStatus.Error)
            {
                Debug.WriteLine(result.ErrorMessage);
            }
            else if (result.Status == PayPalStatus.Successful)
            {
                //Print Authorization Code
                Debug.WriteLine(result.ServerResponse.Response.Code);
                //Print Client Metadata Id
                Debug.WriteLine(CrossPayPalManager.Current.ClientMetadataId);
                clientMetaData = CrossPayPalManager.Current.ClientMetadataId;
            }
        }
    }
}
