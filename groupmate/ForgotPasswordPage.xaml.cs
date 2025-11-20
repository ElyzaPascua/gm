namespace groupmate;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage()
	{
		InitializeComponent();
	}
    private async void SignIn_Tapped(object sender, TappedEventArgs e)
    {
        
        await Shell.Current.GoToAsync("///SignInPage"); 
    }
}