using Gallery_App.Classes;

namespace Gallery_App.Pages;

public partial class Gallery_Picture : ContentPage
{
	Gallery_Class picture = new Gallery_Class();
    private string url = "http://10.130.54.78:8000";

    public Gallery_Picture(Gallery_Class gallery_)
	{
		InitializeComponent();
		picture = gallery_;
		pictureTitle.Text = picture.titel;
		var imageSoure = ImageSource.FromUri(new Uri(url + picture.billedet));
		pictureImage.Source = imageSoure;
		pictureDescription.Text = picture.beskrivelse;
	}
}