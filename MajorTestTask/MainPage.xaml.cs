using MajorTestTask.ViewModels;

namespace MajorTestTask;

public partial class MainPage : ContentPage
{
	MainPageVM viewModel;
	public MainPage()
	{
		InitializeComponent();
		BindingContext = viewModel = new MainPageVM();
	}

    private async void ToApplications_Clicked(object sender, EventArgs e)
    {
        ToApplications.BackgroundColor = Color.FromHex("#A1A1A1");
        await Task.Delay(1000);
        ToApplications.BackgroundColor = Color.FromHex("#FFFFFF");
    }
}

