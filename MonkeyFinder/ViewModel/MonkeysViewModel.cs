namespace MonkeyFinder.ViewModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.Services;


public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = new ObservableCollection<Monkey>();
    // Dependency Injection, set in builder
    IConnectivity connectivity;
    IGeolocation geolocation;

    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "MONKE";
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetClosestMonkeyAsync()
    {
        if (IsBusy || Monkeys.Count == 0) return;

        try
        {
            Location location = await geolocation.GetLastKnownLocationAsync();
            if (location is null)
            {
                location = await geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Low,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
            }

            if (location is null) return;

            Monkey first = Monkeys.OrderBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers)).FirstOrDefault();

            if (first is null) return;
            await Shell.Current.DisplayAlert("Closest", $"{first.Name} in {first.Location}", "OK");

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("GeoLocation", "Cannot GeoLocate", "OK");
        }

    }

    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        if (IsBusy) return;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Net Issue", "Check your internet works", "OK");
            return;
        }

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
            await Shell.Current.DisplayAlert("Error", "Cannot Fetch", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey == null) return;

        // main way to nav
        // uri + query params
        //await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={monkey.Name}");
        await Shell.Current.GoToAsync(nameof(DetailsPage), true,
            new Dictionary<string, object>
            {
                {"Monkey", monkey}
            });
    }
}
