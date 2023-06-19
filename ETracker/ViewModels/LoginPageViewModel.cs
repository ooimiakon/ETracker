using System;
using System.Text.Json;
using System.Windows.Input;

namespace ETracker.ViewModels
{
	public class LoginPageViewModel : BaseViewModel
    {
        private string _password;
        private int? _account;
        private readonly HttpClient _client;


        public string Password
        {
            get => _password;
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged(nameof(Password));
            }

        }

        public int? Account
        {
            get => _account;
            set
            {
                if (_account == value) return;
                _account = value;
                OnPropertyChanged(nameof(Account));
            }
        }


        //public ICommand ICommandNavToHomePage { get; set; }
        public ICommand LoginCommand { get; set; }


        public LoginPageViewModel()
        {
            _client = new HttpClient();
            LoginCommand = new Command(async () => await Login());
        }

        private void NavigateToHomePage()
        {
            App.Current.MainPage.Navigation.PushAsync(new BottomTabPage());
        }

        private async Task Login()
        {
            // 获取 Password 和 UserId 的值
            string password = Password;
            int? account = Account;

            // 调用 API，判断值是否匹配
            string response = await _client.GetStringAsync($"http://118.89.122.162/api/User/Login?login_number={account}&login_password={password}");

            var jsonObject = JsonDocument.Parse(response).RootElement;

            // 获取 status 和 isSuccess 值
            var status = jsonObject.GetProperty("status").GetBoolean();
            var isSuccess = jsonObject.GetProperty("data").GetProperty("isSuccess").GetBoolean();

            // 判断登录是否成功
            if (status && isSuccess)
            {
                // 提示用户登录成功
                await App.Current.MainPage.DisplayAlert("成功", "登录成功", "确定");                //跳转
                App.Current.MainPage=new BottomTabPage();
            }
            else
            {
                // 登录失败
                await App.Current.MainPage.DisplayAlert("失败", "登录失败，请重新输入", "确定");

                // 清空参数
                // 清空 login_number 和 login_password
                Account = null;
                Password = string.Empty;
            }
        }
    }
}

