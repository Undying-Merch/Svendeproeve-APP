using Gallery_App.Classes;

namespace Gallery_App.Pages;

public partial class Subscribe : ContentPage
{
	APIConn conn = new APIConn();
    List<Kategori> categories = new List<Kategori>();
    List<string> categoryNames = new List<string>();
    List<Gallery_Class> billeder = new List<Gallery_Class>();
    List<Gallery_Class> bListe = new List<Gallery_Class>();

    private string url = "http://10.130.54.78:8000";
    int catId = 0;
    string catString = "";
    private int userId = 0;

    public Subscribe(int brugerId)
	{
		InitializeComponent();
        userId = brugerId;
        categories = conn.getCategories();
        billeder = conn.getAllBilleder();

        for (int i = 0; i < categories.Count; i++)
        {
            categoryNames.Add(categories[i].navn);
        }
        catPicker.ItemsSource = categoryNames;
    }

    public void setCat(object sender, EventArgs e)
    {
        if (exampleLabel.IsVisible == false)
        {
            exampleLabel.IsVisible = true;
        }
        if (subscribeBTN.IsEnabled == false)
        {
            subscribeBTN.IsEnabled = true;
        }

        exampleImage1.Source = null; exampleImage2.Source = null;
        var imageSource = ImageSource.FromUri(new Uri(url));
        catString = catPicker.SelectedItem.ToString();

        for (int i = 0; i < categories.Count; i++)
        {
            if (catString == categories[i].navn)
            {
                catId = categories[i].Id;
            }
        }
        int images = 0;
        int counter = 0;
        while (images != 2)
        {
            if (imageSource != null) { imageSource = null; }

            if (billeder[counter].kategori_id == catId)
            {
                imageSource = ImageSource.FromUri(new Uri(url + billeder[counter].billedet));
            }

            if (imageSource != null && images == 0) 
            {
                exampleImage1.Source = imageSource;
                images++;
            }
            else if (imageSource != null && images == 1)
            {
                exampleImage2.Source = imageSource;
                images++;
            }

            counter++;
            if (counter == billeder.Count)
            {
                images = 2;
            }
        }
    }

    public async void subscribeFunction(object sender, EventArgs e)
    {
        Subscribe_Class sub = new Subscribe_Class(userId, catId);
        bool goOn = await DisplayAlert("Subscribe", $"Subscribe til {catString}?", "Ja", "Nej");

        if (goOn)
        {
            bool result = conn.subscribeTo(sub);
            if (result)
            {
                await DisplayAlert("Succes", $"Du er nu subscribed til {catString}", "OK");
                await Navigation.PopAsync();
            }
        }
    }

}