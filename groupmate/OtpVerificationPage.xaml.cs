namespace groupmate;

public partial class OtpVerificationPage : ContentPage
{
	public OtpVerificationPage()
	{
		InitializeComponent();
	}
    private void Resend_Tapped(object sender, TappedEventArgs e)
    {
        
        DisplayAlert("Code Sent", "A new verification code has been sent.", "OK");
    }
    private async void BackButton_Clicked(object sender, EventArgs e)
    {
       
        await Shell.Current.GoToAsync("..");
    }
}