using System.ComponentModel;
using ETracker.ViewModels;

namespace ETracker.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginPageViewModel();
    }

}
