namespace groupmate;

public partial class SplashPage : ContentPage
{
	public SplashPage()
	{
		InitializeComponent();
	}
    private async void OnSplashPageTapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new SignupPage();
    }
}