using ETracker.ViewModels;

namespace ETracker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        BindingContext = new MainPageViewModel();
        InitializeComponent();
    }
}


