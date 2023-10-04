using Gallery_App.Classes;

namespace Gallery_App.Pages;

public partial class Gallery_Picture : ContentPage
{
	APIConn conn = new APIConn();
    Gallery_Class picture = new Gallery_Class();
    Geo_Location location = new Geo_Location();
    private string url = "http://10.130.54.78:8000";

    public Gallery_Picture(Gallery_Class gallery_)
	{
		InitializeComponent();
		picture = gallery_;
		pictureTitle.Text = picture.titel;
		var imageSoure = ImageSource.FromUri(new Uri(url + picture.billedet));
		pictureImage.Source = imageSoure;
		pictureDescription.Text = picture.beskrivelse;
        location = conn.getLocation(picture.geo_id);
	}

    public async void gotoMaps(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Maps:", "Gå videre til Google Maps?", "Ja", "Nej");

        if (answer)
        {
            try
            {
                Uri uri = new Uri($"https://google.com/maps/search/{location.lattitude}+{location.longtitude}");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occurred. No browser may be installed on the device.
            }

        }
    }
}