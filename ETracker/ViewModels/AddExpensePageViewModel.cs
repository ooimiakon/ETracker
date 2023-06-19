using System.ComponentModel;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Newtonsoft.Json;
using ETracker;
using ETracker.Views;


namespace ETracker.ViewModels;

public class AddExpensePageViewModel : BaseViewModel
{
    public AddExpensePageViewModel()
    {
        _client = new HttpClient();
        SaveCommand = new Command(async () => await Save());
    }

    private readonly HttpClient _client;
    private bool _isExpense;
    public bool IsExpense
    {
        get { return _isExpense; }
        set
        {
            if (_isExpense != value)
            {
                _isExpense = value;
                OnPropertyChanged(nameof(IsExpense));
                UpdateCategories();
            }
        }
    }


    public List<string> Categories { get; private set; }
    public string SelectedCategory { get; set; }


    private void UpdateCategories()
    {
        Categories = IsExpense ? new List<string> { "food", "shop", "play", "else" } : new List<string> { "salary", "gift", "else" };
        OnPropertyChanged(nameof(Categories));
        OnPropertyChanged(nameof(SelectedCategory));
    }

    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            if (_title == value) return;
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private double _amount;
    public double Amount
    {
        get => _amount;
        set
        {
            if (_amount == value) return;
            _amount = value;
            OnPropertyChanged(nameof(Amount));
        }
    }

    private DateTime _time;
    public DateTime Time
    {
        get => _time;
        set
        {
            if (_time == value) return;
            _time = value;
            OnPropertyChanged(nameof(Time));
        }
    }


    private string _content;
    public string Content
    {
        get => _content;
        set
        {
            if (_content == value) return;
            _content = value;
            OnPropertyChanged(nameof(Content));
        }
    }

    public ICommand SaveCommand { get; set; }

    private async Task Save()
    {
        // 构建JSON数据对象
        var jsonData = new
        {
            CategoryName = SelectedCategory,
            UserId = 1, // 根据需求设置正确的用户ID
            RecordTitle = Title,
            RecordAmount = Amount,
            RecordIsExpensrecordIsExpensrecordIsExpens = IsExpense,
            RecordContent = Content
        };


        // 将JSON对象序列化为字符串
        var json = JsonConvert.SerializeObject(jsonData);

        // 创建HttpClient实例并发送请求
        using (var client = new HttpClient())
        {
            var url = "http://118.89.122.162/api/Record/AddRecord";

            // 构建HttpContent对象
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 发送POST请求
            var response = await client.PostAsync(url, content);

            // 处理响应
            if (response.IsSuccessStatusCode)
            {
                // 成功处理响应的逻辑
                await App.Current.MainPage.DisplayAlert("成功", "保存成功", "确定");
                // 清空数据
                Title = string.Empty;
                Amount = 0;
                Time = DateTime.Now;
                Content = string.Empty;

                // 打开新的 contentpage 并移除当前的 contentpage
                //App.Current.MainPage = new BottomTabPage();
                //MainPage mainPage = (MainPage)App.Current.MainPage.FindByName("mainpage");
                //var mainPageViewModel = mainPage.BindingContext as MainPageViewModel;
                //mainPageViewModel.Init();
            }
            else
            {
                // 失败处理响应的逻辑
                await App.Current.MainPage.DisplayAlert("失败", "保存失败", "确定");
            }
        }

    }

}
