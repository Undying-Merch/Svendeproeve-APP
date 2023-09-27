namespace Gallery_App.Pages;

public partial class MainMenu : ContentPage
{
	public MainMenu()
	{
		InitializeComponent();
	}

	public void gotoUploadBillede(object sender, EventArgs e)
	{
		Navigation.PushAsync(new UploadBillede1());
	}
}