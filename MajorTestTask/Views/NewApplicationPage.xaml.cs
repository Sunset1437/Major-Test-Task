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


    private void PickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PickerStatus.SelectedItem.ToString() != "Новая" && viewModel.IsOnEdit)
        {
            ESendAddress.IsReadOnly = true;
            ERecieveAddress.IsReadOnly = true;
            DatePicker.IsEnabled = false;
            EWeight.IsReadOnly = true;
            ELength.IsReadOnly = true;
            EWidth.IsReadOnly = true;
            EHeight.IsReadOnly = true;
            InfoStack.IsVisible = true;
        }
        else
        {
            ESendAddress.IsReadOnly = false;
            ERecieveAddress.IsReadOnly = false;
            DatePicker.IsEnabled = true;
            EWeight.IsReadOnly = false;
            ELength.IsReadOnly = false;
            EWidth.IsReadOnly = false;
            EHeight.IsReadOnly = false;
            InfoStack.IsVisible = false;
        }
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
    #region label-buttons
    private async void Button_Clicked(object sender, EventArgs e) // обработка для ошибок
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