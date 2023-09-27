using Gallery_App.Pages;

namespace Gallery_App
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        public void gotoLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        public void gotoOpretBruger(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OpretBruger());
        }
    }
}