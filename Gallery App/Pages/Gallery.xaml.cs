using Gallery_App.Classes;

namespace Gallery_App.Pages;

public partial class Gallery : ContentPage
{
    APIConn conn = new APIConn();
    List<Kategori> categories = new List<Kategori>();
    List<string> categoryNames = new List<string>();
    List<Gallery_Class> billeder = new List<Gallery_Class>();
    List<Gallery_Class> bListe = new List<Gallery_Class>();

    int catId = -1;
    int pageCounter = 0;
    private string url = "http://10.130.54.78:8000";

    public void setImageList()
    {
        bListe.Clear();
        if (catId == -1)
        {
            for (int i = 0; i < billeder.Count; i++)
            {
                bListe.Add(billeder[i]);
            }
        }
        else
        {
            for(int i = 0;i < billeder.Count; i++)
            {
                if (billeder[i].kategori_id == catId)
                {
                    bListe.Add(billeder[i]);
                }
            }
        }
        setImage();
    }

    public void setImage()
    {
        galleryImage0.Source = null;
        galleryImage1.Source = null;
        galleryImage2.Source = null;
        galleryImage3.Source = null;
        for (int i = 0 + pageCounter; i < 4 + pageCounter; i++)
        {
            var imageSource = ImageSource.FromUri(new Uri(url + bListe[i].billedet));
            if (i > bListe.Count)
            {
                break;
            }
            if (i == 0 + pageCounter)
            {
                galleryImage0.Source = imageSource;
            }
            else if (i == 1 + pageCounter)
            {
                galleryImage1.Source = imageSource;
            }
            else if (i == 2 + pageCounter)
            {
                galleryImage2.Source = imageSource;
            }
            else if (i == 3 + pageCounter)
            {
                galleryImage3.Source = imageSource;
            }
        }

    }

    public Gallery()
	{
		InitializeComponent();
        billeder = conn.getAllBilleder();
        categories = conn.getCategories();
        categoryNames.Add("Alle");
        for (int i = 0; i < categories.Count; i++)
        {
            categoryNames.Add(categories[i].navn);
        }
        categoryPicker.ItemsSource = categoryNames;
        setImageList();
        setImage();
    }



}