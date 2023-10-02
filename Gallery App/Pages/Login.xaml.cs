using Gallery_App.Classes;

namespace Gallery_App.Pages;

public partial class Login : ContentPage
{
	APIConn conn = new APIConn();
	private bool passHidden = true;

	public Login()
	{
		InitializeComponent();

        if (Preferences.Default.ContainsKey("user") == true) { loginRemember.IsChecked = true; brugerEntry.Text = Preferences.Default.Get("user", ""); passEntry.Text = Preferences.Default.Get("pass", ""); }

    }
	public Login(string username, string password)
	{
		InitializeComponent();
		brugerEntry.Text = username;
		passEntry.Text = password;
	}

    public void gotoMain(object sender, EventArgs e)
	{
		if (brugerEntry.Text == null || passEntry.Text == null)
		{
			DisplayAlert("Manglende felt", "Et eller flere felter mangler at blive udfyldt", "OK");
		}
		else
		{
			bool isUser = false;
			
			Bruger bruger = new Bruger(brugerEntry.Text, passEntry.Text);
			isUser = conn.passwordCheck(bruger);

			if (isUser)
			{
                if (loginRemember.IsChecked == true)
                {
                    Preferences.Default.Set("user", brugerEntry.Text);
                    Preferences.Default.Set("pass", passEntry.Text);
                }
                else { Preferences.Default.Remove("user"); Preferences.Default.Remove("pass");
					brugerEntry.Text = ""; passEntry.Text = "";
				}

                Navigation.PushAsync(new MainMenu());
			}
			else
			{
				DisplayAlert("Fejl", "Kunne ikke finde en bruger med dette Brugernavn og Password. Prøv igen.", "OK");
			}
		}
	}
	public void hiddenPass(object sender, EventArgs e)
	{
		if (passHidden)
		{
			passEntry.IsPassword = false;
			passBTN.Text = "Skjul Kode";
			passHidden = false;
		}
		else 
		{
			passEntry.IsPassword = true;
			passBTN.Text = "Vis Kode";
			passHidden = true;
		}
	}
}