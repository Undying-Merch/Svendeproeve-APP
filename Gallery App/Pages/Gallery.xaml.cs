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
    int pic1Id = 0; int pic2Id = 0; int pic3Id = 0; int pic4Id = 0;

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

    public void backAndForthBTN()
    {
        if (pageCounter == 0)
        {
            previousBTN.IsEnabled = false;
        }
        else
        {
            previousBTN.IsEnabled = true;
        }
        if (bListe.Count - 4 <= pageCounter)
        {
            nextBTN.IsEnabled = false;
        }
        else
        {
            nextBTN.IsEnabled = true;
        }
    }

    public void prevoiusSlideBTN(object sender, EventArgs e)
    {
        pageCounter -= 4;
        backAndForthBTN();
        setImage();
    }
    public void nextSlideBTN(object sender, EventArgs e)
    {
        pageCounter += 4;
        backAndForthBTN();
        setImage();
    }

    public void onCategoryChanged(object sender, EventArgs e)
    {
        string categoryString = categoryPicker.SelectedItem.ToString();

        if (categoryString == "Alle")
        {
            catId = -1;
        }
        else
        {
            for (int i = 0;i < categories.Count;i++)
            {
                if (categoryString == categories[i].navn)
                {
                    catId = categories[i].Id;
                }
            }
        }
        setImageList();
    }

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
        pageCounter = 0;
        backAndForthBTN();
    }

    public void setImage()
    {
        galleryImage0.Source = null; galleryImage1.Source = null; galleryImage2.Source = null; galleryImage3.Source = null;
        galleryImage0.IsEnabled = false; galleryImage1.IsEnabled = false; galleryImage2.IsEnabled = false; galleryImage3.IsEnabled = false;
        for (int i = 0 + pageCounter; i < 4 + pageCounter; i++)
        {
            var imageSource = ImageSource.FromUri(new Uri(url));
            if (i < bListe.Count)
            {
                imageSource = ImageSource.FromUri(new Uri(url + bListe[i].billedet));
            }
            else
            {
                imageSource = null;
            }
            if (i == 0 + pageCounter)
            {
                galleryImage0.Source = imageSource;
                galleryImage0.IsEnabled = true;
                pic1Id = bListe[i].id;
            }
            else if (i == 1 + pageCounter)
            {
                galleryImage1.Source = imageSource;
                galleryImage1.IsEnabled = true;
                pic2Id = bListe[i].id;
            }
            else if (i == 2 + pageCounter)
            {
                galleryImage2.Source = imageSource;
                galleryImage2.IsEnabled = true;
                pic3Id = bListe[i].id;
            }
            else if (i == 3 + pageCounter)
            {
                galleryImage3.Source = imageSource;
                galleryImage3.IsEnabled = true;
                pic4Id = bListe[i].id;
            }
        }

    }

    public async void clickImage1(object sender, EventArgs e)
    {
        Gallery_Class picture = new Gallery_Class();
        for (int i = 0;i < bListe.Count;i++)
        {
            if (bListe[i].id == pic1Id) 
            {
                picture = bListe[i];
            }
        }
        await Navigation.PushAsync(new Gallery_Picture(picture));
    }

    



}