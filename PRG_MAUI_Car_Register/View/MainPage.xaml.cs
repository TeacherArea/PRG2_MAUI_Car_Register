using PRG_MAUI_Car_Register.ViewModel;
using System.Diagnostics;

namespace PRG_MAUI_Car_Register;

public partial class MainPage : ContentPage
{
    private MainPageViewModel vm;

    public MainPage()
    {
        vm = new MainPageViewModel();
        vm.ShowAlert = async (title, msg) => await DisplayAlert(title, msg, "OK");

        BindingContext = vm;
        InitializeComponent();
    }

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        vm.RegisterVehicle();
    }

    private void Radio_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value) return;
        var radio = sender as RadioButton;

        if (radio != null)
        {
            vm.SelectedCategory = radio.Content.ToString();
        }
    }

    private void OnSearchClicked(object sender, EventArgs e)
    {
        vm.SearchVehicle();
    }
}