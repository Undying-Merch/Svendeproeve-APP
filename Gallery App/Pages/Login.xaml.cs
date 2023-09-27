namespace Gallery_App.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

	public void gotoMain(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainMenu());
	}
}