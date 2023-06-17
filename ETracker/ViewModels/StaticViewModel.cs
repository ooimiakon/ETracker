using System.Collections.ObjectModel;

namespace ETracker.ViewModels;


public class StaticViewModel : BaseViewModel
{
    private List<DailyExpenseItem> _dailyExpenseList;
    private readonly HttpClient _client;
    private string _result;
    public ObservableCollection<CategoricalData> Data { get; set; }

    private static ObservableCollection<CategoricalData> GetCategoricalData()
    {
        var data = new ObservableCollection<CategoricalData>  {
            new CategoricalData { Category = "A", Value = 0.63 },
            new CategoricalData { Category = "B", Value = 0.85 },
            new CategoricalData { Category = "C", Value = 1.05 },
            new CategoricalData { Category = "D", Value = 0.96 },
            new CategoricalData { Category = "E", Value = 0.78 },
        };

        return data;
    }


    public string Result
    {
        get => _result;
        set
        {
            if (_result == value) return;
            _result = value;
            OnPropertyChanged(nameof(Result));
        }
    }

    public StaticViewModel()
    {
        Init();
        this.Data = GetCategoricalData();
        _client = new HttpClient();
    }

    public List<DailyExpenseItem> dailyExpenseList
    {
        get => _dailyExpenseList;
        set
        {
            if (_dailyExpenseList == value) return;
            _dailyExpenseList = value;
            OnPropertyChanged(nameof(dailyExpenseList));
        }

    }

    public void Test()
    {
        var response = _client.GetStringAsync("http://47.96.94.96:3300/api/Finance/GetDonateCount").Result;
        Result= response;
    }

    private void Init()
    {
        dailyExpenseList = new List<DailyExpenseItem>(); // 实例化 expenseList

        dailyExpenseList.Add(new DailyExpenseItem(new DateTime(2023, 6, 1), 42));
        dailyExpenseList.Add(new DailyExpenseItem(new DateTime(2023, 6, 2), 42));
        dailyExpenseList.Add(new DailyExpenseItem(new DateTime(2023, 6, 3), 52));
        dailyExpenseList.Add(new DailyExpenseItem(new DateTime(2023, 6, 4), 12));
    }



    public class DailyExpenseItem
    {
        public DateTime day { get; set; }
        public double value { get; set; }

        public DailyExpenseItem(DateTime day, double value)
        {
            this.day = day;
            this.value = value;
        }
    }
}

public class CategoricalData
{
    public object Category { get; set; }
    public double Value { get; set; }
}