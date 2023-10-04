using Gallery_App.Classes;
using Microsoft.Maui.Devices.Sensors;
using System.Text;

namespace Gallery_App.Pages;

public partial class UploadBillede2 : ContentPage
{

	FileResult photo;
	APIConn conn = new APIConn();
	List<Kategori> categories= new List<Kategori>();
	List<string> categoryNames = new List<string>();
	Location location = new Location();
	int userId = 0;

	public UploadBillede2(FileResult file, int uId)
	{
		InitializeComponent();
		photo = file;
		userId = uId;
		FileStream fs = File.OpenRead(photo.FullPath);
		var imgSource = ImageSource.FromStream(() => fs);
		imageInput.Source = imgSource;
		categories = conn.getCategories();
		for (int i = 0; i < categories.Count; i++)
		{
			categoryNames.Add(categories[i].navn);
		}
		categoryPicker.ItemsSource = categoryNames;
	}

	public async void uploadBillede(object sender, EventArgs e)
	{
		await GetCurrentLocation();
        decimal lattitude = (decimal)location.Latitude;
        decimal longitude = (decimal)location.Longitude;
		Geo_Location geoLocation = new Geo_Location(Math.Round(lattitude, 6).ToString(), Math.Round(longitude, 6).ToString());
		int geoId = conn.uploadGeoLocation(geoLocation);
		int regelId = 0;
		for (int i = 0;i < categories.Count;i++)
		{
			if (categories[i].navn == categoryPicker.SelectedItem.ToString())
			{
				regelId = categories[i].Id;
			}
		}
		Billede billede = new Billede(categoryTitle.Text, categoryDescription.Text, photo, geoId, regelId, userId);

		bool uploadResult = conn.uploadBillede(billede);

		if (uploadResult)
		{
			await DisplayAlert("Succes", "Dit billede er nu uploadet, returner til Main Siden", "OK");
			await Navigation.PopAsync();
			await Navigation.PopAsync();
		
		}
		else
		{
			await DisplayAlert("Error", "Noget gik galt, kontakt vores udviklere (Inforamtion på hjemmesiden)", "OK");
		}

    }

	public void onCategoryChanged(object sender, EventArgs e)
	{
		if (uploadBTN.IsEnabled == false)
		{
			uploadBTN.IsEnabled = true;
		}
	}

    public async Task GetCurrentLocation()
    {
        try
        {

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
        }

    }
}