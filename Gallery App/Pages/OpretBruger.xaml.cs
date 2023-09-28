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

	public void creteUser(object sender, EventArgs e)
	{
		
		if (opretNavn.Text.GetType == null || opretBrugernavn.Text == null || opretMail.Text == null || opretPassword.Text == null)
		{
			DisplayAlert("Fejl", "Udfyld venligst alle felter.", "OK");
		}
        else if (opretPassword.Text != opretPassword2.Text)
        {
            DisplayAlert("Fejl", "Kodeord matcher ikke", "OK");
        }
		else if (isMailValid(opretMail.Text.ToString()) == false)
		{
			DisplayAlert("Fejl", "Indtastet mail er ikke valid", "OK");
		}
		else
		{
            Bruger bruger = new Bruger(opretNavn.Text.ToString(), opretBrugernavn.Text.ToString(), opretPassword.Text.ToString(), opretMail.Text.ToString());

        }
    }

	public bool isMailValid(string email)
	{
        var emailValidation = new EmailAddressAttribute();
        return emailValidation.IsValid(email);
    }

	
}