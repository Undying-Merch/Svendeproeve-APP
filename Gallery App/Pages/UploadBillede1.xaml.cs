namespace Gallery_App.Pages;

public partial class UploadBillede1 : ContentPage
{
	public UploadBillede1()
	{
		InitializeComponent();
	}

	public void uploadCaptured(object sender, EventArgs e)
	{
		Navigation.PushAsync(new UploadBillede2());
	}
}