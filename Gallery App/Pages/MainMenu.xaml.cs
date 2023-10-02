namespace Gallery_App.Pages;

public partial class MainMenu : ContentPage
{
	int userId = 0;

	public MainMenu(int id)
	{
		InitializeComponent();
		userId = id;
	}

	public void gotoUploadBillede(object sender, EventArgs e)
	{
		Navigation.PushAsync(new UploadBillede1(userId));
	}
}