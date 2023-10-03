using Gallery_App.Classes;

namespace Gallery_App.Pages;

public partial class Gallery : ContentPage
{
    APIConn conn = new APIConn();
    List<Kategori> categories = new List<Kategori>();
    List<string> categoryNames = new List<string>();
    List<Gallery_Class> billeder = new List<Gallery_Class>();
    int catId = 0;

    public void setImage()
    {
        if (catId == 0)
        {
            
        }
    }

    public Gallery()
	{
		InitializeComponent();
        billeder = conn.getAllBilleder();
        categories = conn.getCategories();
        for (int i = 0; i < categories.Count; i++)
        {
            categoryNames.Add(categories[i].navn);
        }
        categoryPicker.ItemsSource = categoryNames;
    }



}