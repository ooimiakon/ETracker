namespace ETracker;
using ETracker.Models;
using ETracker.ViewModels;
using System.Net.Http;
using System.Net.Http.Json;

public partial class AddExpensePage : ContentPage
{
    public class RecordDetail
    {
        public string CategoryName { get; set; }
        public int UserId { get; set; }
        public string RecordTitle { get; set; }
        public double RecordAmount { get; set; }
        public bool RecordIsExpensrecordIsExpensrecordIsExpens { get; set; }
        public string RecordContent { get; set; }
    }

    public AddExpensePage()
	{
		InitializeComponent();
        BindingContext =new AddExpensePageViewModel();
        IsExpenseSwitch.IsToggled = true; // Set the switch to "true" (expense) by default
    }
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        // Create a new ExpenseItem object and populate its properties
        ExpenseItem expenseItem = new ExpenseItem
        {
            title = TitleEntry.Text,
            amount = Convert.ToDouble(AmountEntry.Text),
            time = TimePicker.Date.ToString("yyyy-MM-dd"),
            isExpense = IsExpenseSwitch.IsToggled,
            category = CategoryPicker.SelectedItem.ToString(),
            content = ContentEntry.Text
        };

        // Perform any additional logic or save the expense item to a database
        HttpClient client = new HttpClient();

        //HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
        //message.Content = JsonContent.Create<RecordDetail>(recordDetail);

        //HttpResponseMessage response = await client.SendAsync(message);


        // 清空输入框
        TitleEntry.Text = string.Empty;
        AmountEntry.Text = string.Empty;
        TimePicker.Date = DateTime.Now.Date;
        IsExpenseSwitch.IsToggled = false;
        CategoryPicker.SelectedItem = null;
        ContentEntry.Text = string.Empty;

        // 提示用户保存成功或执行其他操作
        DisplayAlert("成功", "支出记录保存成功", "确定");
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
