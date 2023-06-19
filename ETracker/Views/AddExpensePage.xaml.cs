namespace ETracker;
using ETracker.Models;
using ETracker.ViewModels;


public partial class AddExpensePage : ContentPage
{

    public AddExpensePage()
	{
		InitializeComponent();
        BindingContext =new AddExpensePageViewModel();
        IsExpenseSwitch.IsToggled = true; // Set the switch to "true" (expense) by default
    }

    private void ClearSelection_Clicked(object sender, EventArgs e)
    {
        CategoryPicker.SelectedItem = null;
    }


    private void IsExpenseSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        UpdateCategories();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateCategories();
    }

    private void UpdateCategories()
    {
        var viewModel = BindingContext as AddExpensePageViewModel;
        if (viewModel != null)
        {
            viewModel.IsExpense = IsExpenseSwitch.IsToggled;
        }
    }
}
