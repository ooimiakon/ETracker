using ETracker.ViewModels;
using Newtonsoft.Json;

namespace ETracker;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _client;
    private MainPageViewModel mainPageViewModel;

    public MainPage()
    {
        _client = new HttpClient();
        InitializeComponent();
        mainPageViewModel = new MainPageViewModel();
        BindingContext = mainPageViewModel;
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        // 获取要删除的记录ID
        Button button = (Button)sender;
        long recordId = (long)button.CommandParameter;

        // 构造删除记录的URL
        var url = $"http://118.89.122.162/api/Record/DeleteRecord?record_id={recordId}";
        // 发送删除请求
        HttpResponseMessage response = await _client.DeleteAsync(url);

        // 读取响应内容
        string responseBody = await response.Content.ReadAsStringAsync();

        // 解析响应JSON
        var responseObject = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

        // 检查是否删除成功
        if (responseObject.errorCode == 200 && responseObject.status == true)
        {
            // 删除成功，根据需要执行其他操作
            await DisplayAlert("成功", "删除成功", "确定");
            mainPageViewModel.Init();
        }
        else
        {
            await DisplayAlert("失败", "删除失败", "确定");
        }

    }
}




