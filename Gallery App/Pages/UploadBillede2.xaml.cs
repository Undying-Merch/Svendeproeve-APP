using Gallery_App.Classes;
using System.Text;

namespace Gallery_App.Pages;

public partial class UploadBillede2 : ContentPage
{

	FileResult photo;
	APIConn conn = new APIConn();
	List<Kategori> categories= new List<Kategori>();

	public UploadBillede2(FileResult file)
	{
		InitializeComponent();
		photo = file;
		FileStream fs = File.OpenRead(photo.FullPath);
		var imgSource = ImageSource.FromStream(() => fs);
		imageInput.Source = imgSource;
		categories = conn.getCategories();
	}
}