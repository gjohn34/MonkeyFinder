namespace MonkeyFinder.View;

public partial class MainPage : ContentPage
{
	public MainPage(MonkeysViewModel viewModel)
	{
		InitializeComponent();
		// tells the view what {Binding To} to look at
		BindingContext = viewModel;
	}
}

