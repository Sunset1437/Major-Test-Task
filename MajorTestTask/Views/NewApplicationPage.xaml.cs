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
            PickerStatus.Items.Add("�����");
            PickerStatus.Items.Add("�������� �� ����������");
            PickerStatus.Items.Add("���������");
            PickerStatus.Items.Add("��������");
            PickerStatus.SelectedIndex = Convert.ToInt32(viewModel.ApplicationStatus);
        }
        else
        {
            PickerStatus.Items.Add("�����");
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
            1 => "��������� ���, �� ������ ���� �� 0.1 �� 10000 ��",
            2 => "��������� ��� �����, ��� ������ ���� ����������������, ����������, � ����� �� ������ ��������� 1000 ��",
            3 => "��������� ������������ ���������� ������ � ����� ��� �����",
            _ => "����������� ������"
        };

        await DisplayAlert("������", message, "OK");
    }

    private async void ButtonClicked()
    {
        SaveBtn.BackgroundColor = Color.FromHex("#A1A1A1");
        await Task.Delay(100);
        SaveBtn.BackgroundColor = Color.FromHex("#FFFFFF");
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlert("����� �����������", "����� ����������� - ��� ��������. ��������: �.������, ��.�������� ���, 33", "��");
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        DisplayAlert("����� ����������", "����� ���������� - ��� ��������. ��������: �.������, ��.����� �����, 15", "��");
    }

    private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        DisplayAlert("��� �����", "��� ����� - ��������������� �������������� �����.\n" +
            "��� ����� � �� - �� ����� ���� ������ 0,1 ��, � ����� ��������� 10000 ��.\n" +
            "�����, ������ � ������ ����� � �� -  �� ����� ���� ������������� ��� �������, � ����� ��������� 1000 ��.", "��");
    }
    #endregion
}