namespace groupmate;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnForgotPasswordTapped(object sender, TappedEventArgs e)
    {
        
        await DisplayAlert("Forgot Password", "Forgot Password Clicked!", "OK");

        
    }

    private async void OnSignUpTapped(object sender, TappedEventArgs e)
    {

        await DisplayAlert("Sign Up", "Sign Up Clicked!", "OK");

     
    }
}