using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
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
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    //shortcut lng fo
    public class shortcut
    {
       private readonly FirebaseClient fbc = new FirebaseClient("https://groupmate-8d778-default-rtdb.asia-southeast1.firebasedatabase.app/");
       public async Task<bool> LoginAsync(string email, string password)
        {
            var users = await fbc
                .Child("Users") 
                .OnceAsync<User>();

            return users.Any(u => u.Object.Email == email && u.Object.Password == password);
        }

    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        

        if (email==null || password == null)
        {
            await DisplayAlert("Error", "Please enter email and password", "OK");
            return;
        }
        //realshit
        shortcut firebase = new shortcut();

        bool valid = await firebase.LoginAsync(email, password);
        if (valid) 
        {
            Application.Current.MainPage = new MainPage();
        }
        else
        {
            await DisplayAlert("Error", "Wrong email and password", "OK");
            return;
        }



        

    }
    private async void OnSignUpTapped(object sender, EventArgs e)
    {

    }

}