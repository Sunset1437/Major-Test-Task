using MajorTestTask.ViewModels;

namespace MajorTestTask.Views;

public partial class NewApplicationPage : ContentPage
{
	NewApplicationVM viewModel;
	public NewApplicationPage()
	{
		InitializeComponent();
		BindingContext = viewModel=new NewApplicationVM();
	}
}