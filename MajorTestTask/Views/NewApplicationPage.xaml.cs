using MajorTestTask.ViewModels;

namespace MajorTestTask.Views;

public partial class NewApplicationPage : ContentPage
{
	NewApplicationVM viewModel;
    public NewApplicationPage()
	{
		InitializeComponent();
        BindingContext = viewModel = new NewApplicationVM();
        viewModel.DisplayErrorAction = DisplayError;
        viewModel.ButtonClickedAction = ButtonClicked;
    }
	private void InitPickerItems()
	{
        PickerStatus.Items.Clear();
        if (viewModel.IsOnEdit)
		{
            PickerStatus.Items.Add("Новая");
            PickerStatus.Items.Add("Передано на выполнение");
            PickerStatus.Items.Add("Выполнено");
            PickerStatus.Items.Add("Отменено");
            PickerStatus.SelectedIndex = Convert.ToInt32(viewModel.ApplicationStatus);
        }
        else
        {
            PickerStatus.Items.Add("Новая");
            PickerStatus.SelectedIndex = 0;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        InitPickerItems();
    }
    #region label-buttons
    private async void DisplayError(int errorCode)
    {
        string message = errorCode switch
        {
            1 => "Проверьте вес, он должен быть от 0.1 до 10000 кг",
            2 => "Проверьте МГХ груза, они должны быть неотрицательными, ненулевыми, а также не должны превышать 1000 см",
            3 => "Проверьте правильность заполенных данных в полях МГХ груза",
            _ => "Неизвестная ошибка"
        };

        await DisplayAlert("Ошибка", message, "OK");
    }

    private async void ButtonClicked()
    {
        SaveBtn.BackgroundColor = Color.FromHex("#A1A1A1");
        await Task.Delay(100);
        SaveBtn.BackgroundColor = Color.FromHex("#FFFFFF");
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlert("Адрес отправителя", "Адрес отправителя - кто отсылает. Например: Г.Москва, ул.Земляной Вал, 33", "ОК");
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        DisplayAlert("Адрес получателя", "Адрес получателя - кто получает. Например: Г.Москва, ул.Новый Арбат, 15", "ОК");
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        DisplayAlert("МГХ Груза", "МГХ груза - массогабаритные характеристики груза.\n" +
            "Вес места в кг - не может быть меньше 0,1 кг, а также превышать 10000 кг.\n" +
            "Длина, ширина и высота места в см -  не может быть отрицательной или нулевой, а также превышать 1000 см.", "ОК");
    }
    #endregion
}