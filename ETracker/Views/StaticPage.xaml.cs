namespace ETracker;

using ETracker.ViewModels;

public partial class StaticPage : ContentPage
{
    private readonly StaticViewModel _viewModel;

    public StaticPage()
	{
		InitializeComponent();
        _viewModel= new StaticViewModel();
        BindingContext = _viewModel;
        _viewModel.Test();
    }

    private void ConfirmButton_Clicked(object sender, EventArgs e)
    {
        // 获取输入的年份和月份
        string year = YearEntry.Text;
        string month = MonthEntry.Text;

        // 执行相关操作，例如保存年份和月份，进行数据处理等

        // 清空输入框
        YearEntry.Text = string.Empty;
        MonthEntry.Text = string.Empty;
    }

}

