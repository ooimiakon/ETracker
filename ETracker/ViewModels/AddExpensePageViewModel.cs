using System.ComponentModel;



namespace ETracker.ViewModels;

public class AddExpensePageViewModel : BaseViewModel
{
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


}
