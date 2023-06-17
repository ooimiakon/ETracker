using System;
using ETracker.Models;

namespace ETracker.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private List<ExpenseItem> _expenseList;
        private Profile _profile;
        private Total _total;

        public MainPageViewModel()
        {
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
        private void Init()
        {
            expenseList = new List<ExpenseItem>(); // 实例化 expenseList

            expenseList.Add(new ExpenseItem()
            {
                amount = 1000,
                title = "工资",
                category= "salary",
                isExpense=false,
                time="2023-3-12",
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

            total = new Total()
            {
                expense = 1000,
                income = 10000,
                surplus = 10000 - 1000,
            };

            profile = new Profile()
            {
                avatarUrl = "https://yixun-picture.oss-cn-shanghai.aliyuncs.com/user_head/21.jpeg",
                name ="DimEveningTwilight"
            };


        }
    }
}

