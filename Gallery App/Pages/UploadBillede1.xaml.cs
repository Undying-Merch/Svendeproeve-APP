namespace Gallery_App.Pages;

public partial class UploadBillede1 : ContentPage
{
	FileResult photo;

	public UploadBillede1()
	{
		InitializeComponent();
	}

	public bool photoCheck()
	{
		if (photo == null)
		{
			DisplayAlert("Fejl", "Der er ikke registreret noget billede, prøv igen.", "OK");
			return false;
		}
		else
		{
			return true;
		}
	}

	public async void uploadCaptured(object sender, EventArgs e)
	{
		photo = await MediaPicker.CapturePhotoAsync();
		bool check = photoCheck();
		if (check)
		{
			await Navigation.PushAsync(new UploadBillede2(photo));
		}
	}

	public async void uploadPicked(object sender, EventArgs e)
	{
        photo = await MediaPicker.PickPhotoAsync();
        bool check = photoCheck();
        if (check)
        {
            await Navigation.PushAsync(new UploadBillede2(photo));
        }
    }
}