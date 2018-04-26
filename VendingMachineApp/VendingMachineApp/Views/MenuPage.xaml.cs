using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace VendingMachineApp
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            menuItemsView.ItemsSource = new ObservableCollection<MenuItems>{
              new MenuItems{ DisplayName ="Formas de Pagamento" },
              new MenuItems{ DisplayName = "Histórico de Compras" },
              new MenuItems{ DisplayName ="Sobre" },
              new MenuItems{ DisplayName ="Ajuda" },
            };
        }

        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (menuItemsView.SelectedItem is MenuItems menu)
            {
                if (menu.DisplayName == "Formas de Pagamento")
                {
                    App.MenuIsPresented = false;
                    menuItemsView.SelectedItem = new object();
                    await App.NavigationPage.Navigation.PushAsync(new FuturePaymentsPage());
                }
                if (menu.DisplayName == "Histórico de Compras")
                {
                    App.MenuIsPresented = false;
                    menuItemsView.SelectedItem = new object();
                    await App.NavigationPage.Navigation.PushAsync(new HistoricPage());
                }
                if (menu.DisplayName == "Sobre")
                {
                    App.MenuIsPresented = false;
                    menuItemsView.SelectedItem = new object();
                    await DisplayAlert("Em construção", "Estamos trabalhando na implementação desta tela. Obrigado pela paciência!", "OK");
                }
                if (menu.DisplayName == "Ajuda")
                {
                    App.MenuIsPresented = false;
                    menuItemsView.SelectedItem = new object();
                    await DisplayAlert("Em construção", "Estamos trabalhando na implementação desta tela. Obrigado pela paciência!", "OK");
                }
            }
        }

    }

    public class MenuItems
    {
        public string DisplayName { get; set; }
    }
}
