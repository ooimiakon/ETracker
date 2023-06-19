using ETracker.Views;

namespace ETracker;


public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new LoginPage();
    }
}

