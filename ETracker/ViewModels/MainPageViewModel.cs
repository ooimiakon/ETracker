using System;
using System.Text.Json;
using System.Net.Http;
using ETracker.Models;

namespace ETracker.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private List<ExpenseItem> _expenseList;
        private Profile _profile;
        private Total _total;
        private readonly HttpClient _client;
        private string _result;


        public MainPageViewModel()
        {
            _client = new HttpClient();
            Init();
        }

        public Total total
        {
            get => _total;
            set
            {
                if (_total == value) return;
                _total = value;
                OnPropertyChanged(nameof(total));
            }

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

        public Profile profile
        {
            get => _profile;
            set
            {
                if (_profile == value) return;
                _profile = value;
                OnPropertyChanged(nameof(profile));
            }

        }

        public List<ExpenseItem> expenseList
        {
            get => _expenseList;

            set
            {
                if (_expenseList == value) return;
                _expenseList = value;
                OnPropertyChanged(nameof(expenseList));
            }
        }
        private async void Init()
        {

            //expenseList = new List<ExpenseItem>(); // 实例化 expenseList
            /*profile*/
            var response1 = _client.GetStringAsync("http://118.89.122.162/api/User/GetProfile?user_id=1").Result;
            Result = response1;

            // 解析 JSON 字符串为对象
            var jsonDocument = JsonDocument.Parse(Result);
            var userInfoElement = jsonDocument.RootElement.GetProperty("data").GetProperty("userInfo");

            // 获取 avatarUrl 和 name 的值
            var resavatarUrl = userInfoElement.GetProperty("avatarUrl").GetString();
            var resname = userInfoElement.GetProperty("name").GetString();

            // 创建 Profile 对象
            profile = new Profile()
            {
                name = resname,
                avatarUrl = resavatarUrl,
            };

            /*total*/
            string response2 = await _client.GetStringAsync("http://118.89.122.162/api/Stats/GetTotal?user_id=1");

            // 解析 JSON 字符串为对象
            var jsonDocument2 = JsonDocument.Parse(response2);
            var totalElement = jsonDocument2.RootElement.GetProperty("data").GetProperty("total");

            var expense = totalElement.GetProperty("expense").GetDouble();
            var income = totalElement.GetProperty("income").GetDouble();
            var surplus = totalElement.GetProperty("surplus").GetDouble();

            // 创建 Total 对象
            total = new Total()
            {
                expense = expense,
                income = income,
                surplus = surplus,
            };

            /*
            total = new Total()
            {
                expense = 1000,
                income = 10000,
                surplus = 10000 - 1000,
            };
            */

            /*expenseList*/
            // 解析 JSON 字符串为对象
            string jsonString = await _client.GetStringAsync("http://118.89.122.162/api/Record/GetAllRecord?user_id=1");

            var response3 = JsonSerializer.Deserialize<ExpenseListResponse>(jsonString);

            // 提取记录列表并转换为 ExpenseItem 对象列表
            expenseList = response3.data.record_list;

            /*
            expenseList.Add(new ExpenseItem()
            {
                amount = 1000,
                title = "工资",
                category = "salary",
                isExpense = false,
                time = "2023-3-12",
                content = "3月收入工资",
            });

            expenseList.Add(new ExpenseItem()
            {
                amount = 15,
                title = "晚饭",
                category = "food",
                isExpense = true,
                time = "2023-3-13",
                content = "食堂",
            });
            */
        }

        public class ExpenseListData
        {
            public List<ExpenseItem> record_list { get; set; }
        }

        public class ExpenseListResponse
        {
            public int errorCode { get; set; }
            public bool status { get; set; }
            public ExpenseListData data { get; set; }
        }
    }
}
