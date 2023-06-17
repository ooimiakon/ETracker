using System.Collections.ObjectModel;
using System.Text.Json;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace ETracker.ViewModels;


public class StaticViewModel : BaseViewModel
{
    private readonly HttpClient _client;
    private ObservableCollection<CategoricalData> _date { get; set; }
    private ObservableCollection<CategoricalData> _income { get; set; }
    public ObservableCollection<DailyExpenseItem> _expense { get; set; }

    public StaticViewModel()
    {
        _client = new HttpClient();
        update();
    }


    private void update()
    {
        GetCategoricalData();
        GetExpenseData();
        GetCategoricalIncome();
    }

    public ObservableCollection<CategoricalData> Data
    {
        get => _date;
        set
        {
            if (_date == value) return;
            _date = value;
            OnPropertyChanged(nameof(Data));
        }
    }

    public ObservableCollection<CategoricalData> Income
    {
        get => _income;
        set
        {
            if (_income == value) return;
            _income = value;
            OnPropertyChanged(nameof(Income));
        }
    }

    public ObservableCollection<DailyExpenseItem> Expense
    {
        get => _expense;
        set
        {
            if (_expense == value) return;
            _expense = value;
            OnPropertyChanged(nameof(Expense));
        }
    }

    private async void GetExpenseData()
    {
        // 解析 JSON
        var response = await _client.GetStringAsync("http://118.89.122.162/api/Stats/GetExpenseByDate?user_id=1");
        var responseObject = JsonSerializer.Deserialize<ResponseObject<ResponseExpense>>(response);

        // 检查响应状态和错误码
        if (responseObject.status)
        {
            // 获取解析后的数据
            var result = responseObject.data.result;

            // 将解析后的数据转换为 List<CategoricalData>
            var expense = new ObservableCollection<DailyExpenseItem>();
            foreach (var item in result)
            {
                var dailyExpenseItem = new DailyExpenseItem
                {
                    Day = item.Day,
                    Amount = item.Amount
                };
                expense.Add(dailyExpenseItem);
            }
            Expense = expense;
        }

    }


    private async void GetCategoricalData()
    {
        // 解析 JSON
        var response = await _client.GetStringAsync("http://118.89.122.162/api/Stats/GetExpenseByCategory?user_id=1");
        var responseObject = JsonSerializer.Deserialize<ResponseObject<ResponseData>>(response);

        // 检查响应状态和错误码
        if (responseObject.status)
        {
            // 获取解析后的数据
            var result = responseObject.data.result;

            // 将解析后的数据转换为 List<CategoricalData>
            var data = new ObservableCollection<CategoricalData>();
            foreach (var item in result)
            {
                var categoricalData = new CategoricalData
                {
                    CategoryName = item.CategoryName,
                    Amount = item.Amount
                };
                data.Add(categoricalData);
            }
            Data = data;
        }
        /*
        var expense = new ObservableCollection<DailyExpenseItem>  {
            new DailyExpenseItem{Day="2023-6-17",Amount=30},
            new DailyExpenseItem{Day="2023-6-18",Amount=30},
            new DailyExpenseItem{Day="2023-6-19",Amount=30},
        };

        return expense;*/
    }

    private async void GetCategoricalIncome()
    {
        // 解析 JSON
        var response = await _client.GetStringAsync("http://118.89.122.162/api/Stats/getIncomeByCategory?user_id=1");
        var responseObject = JsonSerializer.Deserialize<ResponseObject<ResponseData>>(response);

        // 检查响应状态和错误码
        if (responseObject.status)
        {
            // 获取解析后的数据
            var result = responseObject.data.result;

            // 将解析后的数据转换为 List<CategoricalData>
            var income = new ObservableCollection<CategoricalData>();
            foreach (var item in result)
            {
                var categoricalData = new CategoricalData
                {
                    CategoryName = item.CategoryName,
                    Amount = item.Amount
                };
                income.Add(categoricalData);
            }
            Income = income;
        }
    }



}

public class CategoricalData
{
    public string CategoryName { get; set; }
    public double Amount { get; set; }
}

public class DailyExpenseItem
{
    public string Day { get; set; }
    public double Amount { get; set; }
}

// 定义用于解析 JSON 的对象模型
public class ResponseObject<T>
{
    public int errorCode { get; set; }
    public bool status { get; set; }
    public T data { get; set; }
}

public class ResponseData
{
    public List<CategoricalData> result { get; set; }
}

public class ResponseExpense
{
    public List<DailyExpenseItem> result { get; set; }
}

//private static ObservableCollection<DailyExpenseItem> GetExpenseData()
//{
//    var expense = new ObservableCollection<DailyExpenseItem>  {
//        new DailyExpenseItem{Day="2023-6-17",Amount=30},
//        new DailyExpenseItem{Day="2023-6-18",Amount=30},
//        new DailyExpenseItem{Day="2023-6-19",Amount=30},
//    };

//    return expense;
//}



