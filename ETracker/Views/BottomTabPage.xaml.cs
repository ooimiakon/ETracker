using ETracker.ViewModels;
namespace ETracker;


public partial class BottomTabPage
{
	public BottomTabPage()
	{
		InitializeComponent();
    }

    private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
    {
        if (this.CurrentPage is NavigationPage navigationPage)

            if(navigationPage.CurrentPage is MainPage mainPage)
            {
                var mainPageViewModel = mainPage.BindingContext as MainPageViewModel;
                mainPageViewModel?.Init();
            }
            else if (navigationPage.CurrentPage is StaticPage staticPage)
            {
                var staticViewModel = staticPage.BindingContext as StaticViewModel;
                staticViewModel?.update();
            }
    }

}
