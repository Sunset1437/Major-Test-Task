using MajorTestTask.ViewModels;

namespace MajorTestTask.Views;

public partial class NewApplicationPage : ContentPage
{
	NewApplicationVM viewModel;
	public NewApplicationPage()
	{
		InitializeComponent();
		BindingContext = viewModel=new NewApplicationVM();
		InitPickerItems();

    }
	private void InitPickerItems()
	{
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
    private void FieldVisibility()
    {
        ReasonEditor.IsVisible = false;
        if (PickerStatus.SelectedItem.ToString() == "��������")
        {
            ReasonEditor.IsVisible = true;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        (sender as Button).BackgroundColor = Color.FromHex("#A1A1A1");
        await Task.Delay(1000);
        (sender as Button).BackgroundColor = Color.FromHex("#FFFFFF");
    }

    private void PickerStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FieldVisibility();
    }
}