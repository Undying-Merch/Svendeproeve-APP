using Gallery_App.Classes;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Gallery_App.Pages;

public partial class OpretBruger : ContentPage
{
	APIConn conn = new APIConn();
	
	public OpretBruger()
	{
		InitializeComponent();
	}

	public async void creteUser(object sender, EventArgs e)
	{
		Bruger brugercheck = new Bruger(opretBrugernavn.Text);
		string test = opretNavn.Text;
		if (opretNavn.Text == null || opretBrugernavn.Text == null || opretPassword.Text == null || opretMail.Text == null)
		{
			await DisplayAlert("Fejl", "Udfyld venligst alle felter.", "OK");
		}
        else if (opretPassword.Text != opretPassword2.Text)
        {
            await DisplayAlert("Fejl", "Kodeord matcher ikke", "OK");
        }
		else if (isMailValid(opretMail.Text.ToString()) == false)
		{
			await DisplayAlert("Fejl", "Indtastet mail er ikke valid", "OK");
		}
		else if (conn.exsistingUser(brugercheck))
		{
			await DisplayAlert("Fejl", "Brugernavn allerede i brug, prøv et andet.", "OK");
		}
		else
		{
            Bruger bruger = new Bruger(opretNavn.Text.ToString(), opretBrugernavn.Text.ToString(), opretPassword.Text.ToString(), opretMail.Text.ToString());

			bool isSucces = false;
			isSucces = conn.createUser(bruger);

            if (isSucces)
            {
				await DisplayAlert("Succes", "Din bruger er oprettet, fortsætter til Login", "OK");
				await Navigation.PushAsync(new Login(bruger.brugernavn, bruger.password));
				await Navigation.PopAsync();
            }
			else
			{
				await DisplayAlert("Fejl", "Noget gik galt, kontakt udvikler (information på vores hjemmeside)", "OK");
			}
        }
    }

	public bool isEntryEmpty(string key)
	{
		if (key == null) { return true; }
		else { return false; }
	}

	public bool isMailValid(string email)
	{
        var emailValidation = new EmailAddressAttribute();
        return emailValidation.IsValid(email);
    }

	
}