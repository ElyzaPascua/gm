using Firebase.Auth; 
using Firebase.Auth.Providers;
using Firebase.Database;
using System.Linq;
namespace groupmate;

public partial class SignupPage : ContentPage
{
    private const string FirebaseWebApiKey = "AIzaSyDukw9wb2wi5y9B1xm5_UUHJR_urVZHzsk"; 
    private const string FirebaseDbUrl = "https://groupmate-8d778-default-rtdb.asia-southeast1.firebasedatabase.app/";
    private readonly FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseWebApiKey));
    private readonly FirebaseClient firebaseClient = new FirebaseClient(FirebaseDbUrl);
    string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    public SignupPage()
	{
		InitializeComponent();
	}

    private async void SignupClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string firstName = FirstNameEntry.Text;
        string lastName = LastNameEntry.Text;
        string course = CoursePicker.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(firstName))
        {
            await DisplayAlert("Error", "Please enter your first name.", "OK");
            return;
        }
        if (string.IsNullOrEmpty(lastName))
        {
            await DisplayAlert("Error", "Please enter your last name.", "OK");
            return;
        }
        if (string.IsNullOrEmpty(email) || !System.Text.RegularExpressions.Regex.IsMatch(EmailEntry.Text, EmailRegex))
        {
            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
            return;
        }
        if(password.Length < 8){ 
            await DisplayAlert("Error", "Password should be more than 8.", "OK");
            return;
        
                }
        if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
        {
            await DisplayAlert("Validation Error", "Password must contain uppercase letters, lowercase letters, and numbers.", "OK");
            PasswordEntry.Focus();
            return;
        }
        if(course == null)
        {
            await DisplayAlert("Error", "Please select your course.", "OK");
            return;
        }

        Application.Current.MainPage = new OtpVerificationPage();
        

    }
    private async void Login_Tapped(object sender, TappedEventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }
}