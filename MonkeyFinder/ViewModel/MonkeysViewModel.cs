namespace MonkeyFinder.ViewModel;

using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.Services;


public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = new ObservableCollection<Monkey>();
    // Dependency Injection, set in builder
    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "MONKE";
        this.monkeyService = monkeyService;
    }

    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            List<Monkey> monkeys = await monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
                Monkeys.Clear();

            // Don't do this later with larger sets
            foreach(Monkey monkey in monkeys) 
                Monkeys.Add(monkey);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            // Change this to interface to display. 
            await Shell.Current.DisplayAlert("Error", "Cannot Fetch", "Cancel");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
