
using Firebase.Database;
using Firebase.Database.Query;
using MimeKit;

namespace groupmate;

public partial class OtpVerificationPage : ContentPage
{
    private readonly SignupPage.User _signupPage;

    public OtpVerificationPage(SignupPage.User user)
	{
		InitializeComponent();
        _signupPage = user;

    }
    private void Resend_Tapped(object sender, TappedEventArgs e)
    {
        

        DisplayAlert("Code Sent", "A new verification code has been sent.", "OK");
    }
    private async void BackButton_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new SignupPage());
    }
   
    private async void Verify_Clicked(object sender, EventArgs e)
    {

        string otp = OtpEntry.Text;
        
        if(otp == null)
        {
            await DisplayAlert("Error", "Please enter the verification code.", "OK");
            return;
        }
        
        if(otp == _signupPage.Code)
        {
            FirebaseClient fbc = new FirebaseClient("https://groupmate-8d778-default-rtdb.asia-southeast1.firebasedatabase.app/",
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult("LkOXorEute2tH7ok17hkJQT6kNM9Rb5hygdKGccV")
            });
            var user = new SignupPage.User
            {
                FirstName = _signupPage.FirstName,
                LastName = _signupPage.LastName,
                Email = _signupPage.Email,
                Course = _signupPage.Course,
                Password = _signupPage.Password
            };
            try
            {
                string clean = _signupPage.Email.Replace(".", "_");
                await fbc.Child("Users").Child(clean).PutAsync(user);

            }
            catch (Exception dbEx)
            {
                await DisplayAlert("Database Error", $"Failed to save user data: {dbEx.Message}", "OK");
                return;
            }


            Application.Current.MainPage = new LoginPage();

        }
        else
        {
            await DisplayAlert("Error", "Wrong verification code.", "OK");
            return;
        }
        
    }
}