using Gallery_App.Classes;

namespace Gallery_App.Pages;

public partial class Gallery_Picture : ContentPage
{
	Gallery_Class picture = new Gallery_Class();

	public Gallery_Picture(Gallery_Class gallery_)
	{
		InitializeComponent();
		picture = gallery_;

	}
}