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


    private void PickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (PickerStatus.SelectedItem.ToString() != "�����" && viewModel.IsOnEdit)
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
    #region label-buttons
    private async void Button_Clicked(object sender, EventArgs e) // ��������� ��� ������
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