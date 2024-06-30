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
            PickerStatus.Items.Add("�������� �� ����������");
            PickerStatus.Items.Add("���������");
            PickerStatus.Items.Add("��������");
        }
		else
			PickerStatus.Items.Add("�����");
		PickerStatus.SelectedIndex = 0;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        switch (viewModel.StatusCode)
        {
            case 1:
                await DisplayAlert("������", "��������� ���, �� ������ ���� �� 0.1 �� 10000 ��", "OK");
                break;
            case 2:
                await DisplayAlert("������", "��������� ��� �����, ��� ������ ���� ����������������, ����������, � ����� �� ������ ��������� 1000 ��", "OK");
                break;
            case 3:
                await DisplayAlert("������", "��������� ������������ ���������� ������ � ����� ��� �����", "OK");
                break;
        }
        (sender as Button).BackgroundColor = Color.FromHex("#A1A1A1");
        await Task.Delay(100);
        (sender as Button).BackgroundColor = Color.FromHex("#FFFFFF");
    }

    private void PickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PickerStatus.SelectedItem.ToString() == "��������")
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