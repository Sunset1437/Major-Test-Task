using MajorTestTask.ViewModels;

namespace MajorTestTask.Views;

public partial class NewApplicationPage : ContentPage
{
	NewApplicationVM viewModel;
    public NewApplicationPage()
	{
		InitializeComponent();
        BindingContext = viewModel = new NewApplicationVM();
    }
	private void InitPickerItems()
	{
        PickerStatus.Items.Clear();
        if (viewModel.IsOnEdit)
		{
            PickerStatus.Items.Add("Передано на выполнение");
            PickerStatus.Items.Add("Выполнено");
            PickerStatus.Items.Add("Отменено");
        }
		else
			PickerStatus.Items.Add("Новая");
		PickerStatus.SelectedIndex = 0;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        switch (viewModel.StatusCode)
        {
            case 1:
                await DisplayAlert("Ошибка", "Проверьте вес, он должен быть от 0.1 до 10000 кг", "OK");
                break;
            case 2:
                await DisplayAlert("Ошибка", "Проверьте МГХ груза, они должны быть неотрицательными, ненулевыми, а также не должны превышать 1000 см", "OK");
                break;
            case 3:
                await DisplayAlert("Ошибка", "Проверьте правильность заполенных данных в полях МГХ груза", "OK");
                break;
        }
        (sender as Button).BackgroundColor = Color.FromHex("#A1A1A1");
        await Task.Delay(100);
        (sender as Button).BackgroundColor = Color.FromHex("#FFFFFF");
    }

    private void PickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PickerStatus.SelectedItem.ToString() == "Отменено")
            ReasonEditor.IsVisible = true;
        else
            ReasonEditor.IsVisible = false;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        InitPickerItems();
    }
}