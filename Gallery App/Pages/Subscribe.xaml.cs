using Gallery_App.Classes;
using Microsoft.Maui.Layouts;

namespace Gallery_App.Pages;

public partial class Subscribe : ContentPage
{
	APIConn conn = new APIConn();
    List<Kategori> categories = new List<Kategori>();
    List<string> categoryNames = new List<string>();
    List<Gallery_Class> billeder = new List<Gallery_Class>();
    List<Subscribe_Class> subbedItems = new List<Subscribe_Class>();

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
        subbedItems = conn.getSubscribedItems(userId);

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

        bool allreadySubbed = false;
        for (int i = 0;i < subbedItems.Count; i++)
        {
            if (subbedItems[i].kategori_id == catId)
            {
                allreadySubbed = true;
            }
        }
        if (allreadySubbed)
        {
            afmeldBTN.IsEnabled = true;
            subscribeBTN.IsEnabled = false;
        }
        else
        {
            afmeldBTN.IsEnabled= false;
            subscribeBTN.IsEnabled = true;
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
        bool goOn = await DisplayAlert("Subscribe", $"Subscribe til {catString}?", "Ja", "Nej");
        bool result = false;

        
        if (goOn)
        {
            Subscribe_Class sub = new Subscribe_Class(userId, catId);
            result = conn.subscribeTo(sub);
            
        }
        if (result)
        {
            await DisplayAlert("Succes", $"Du er nu subscribed til {catString}, går tilbage til Main", "OK");
            await Navigation.PopAsync();
        }
        else if (!result)
        {
            await DisplayAlert("Fejl", "Der skete en fejl, kontakt venligst vores udvikler hold.", "OK");
        }
    }

    public async void unsubscribeFunction(object sender, EventArgs e)
    {
        bool goOn = await DisplayAlert("Afmeld", $"Sikker på du vil afmelde {catString}?", "Ja", "Nej");

        if (goOn)
        {
            int itemId = 0;
            for (int i = 0; i < subbedItems.Count; i++)
            {
                if (catId == subbedItems[i].kategori_id)
                {
                    itemId = subbedItems[i].id;
                }
            }
            conn.unsubscribeItem(itemId);
            await DisplayAlert("Afmeldt", $"Du har nu afmeldt {catString}", "OK");
            await Navigation.PopAsync();
        }
    }

}