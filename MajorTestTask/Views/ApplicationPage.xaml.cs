using MajorTestTask.ViewModels;

namespace MajorTestTask.Views;

public partial class ApplicationPage : ContentPage
{
	ApplicationVM viewModel;
	public ApplicationPage()
	{
		InitializeComponent();
		BindingContext = viewModel = new ApplicationVM();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        (sender as Button).BackgroundColor = Color.FromHex("#A1A1A1");
        await Task.Delay(1000);
        (sender as Button).BackgroundColor = Color.FromHex("#FFFFFF");
    }

}