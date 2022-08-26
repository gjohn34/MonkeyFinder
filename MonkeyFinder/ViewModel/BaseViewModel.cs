using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModel;

//INotifyPropertyChanged needed, triggers update after var change
public partial class BaseViewModel : ObservableObject
{
    // Observable pre-generated code
    // Triggers update w/ NotifyProp - pre-generated code
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    [ObservableProperty]
    string title;

    public bool IsNotBusy => !IsBusy;

    // Completely redundent, using CommunityToolkit's ObservableObject parent class
    // and pre-generated analyzer (Dependencies => net6.0 => Analyzers)

    //public bool IsBusy
    //{
    //    get => isBusy;
    //    set
    //    {
    //        if(isBusy == value)
    //            return;

    //        isBusy = value;
    //        // No param needed, see below.
    //        OnPropertyChanged();
    //        //OnPropertyChanged("IsBusy");
    //        //OnPropertyChanged(nameof(IsBusy));
    //        OnPropertyChanged(nameof(IsNotBusy));
    //    }
    //}

    //public string Title
    //{
    //    get => title;
    //    set
    //    {
    //        if (title == value)
    //            return;

    //        title = value;
    //        OnPropertyChanged();
    //    }
    //}
    //public event PropertyChangedEventHandler PropertyChanged;

    //// Calling OnPropertyChanged with no params, CallerMemberName retrieves the "caller" of the fnc
    //// very cool
    //public void OnPropertyChanged([CallerMemberName]string name = null)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    //}
}   
