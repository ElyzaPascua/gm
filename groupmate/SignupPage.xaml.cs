
using Firebase.Auth; 
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;
using Microsoft.Maui.Controls;
namespace groupmate;

public partial class SignupPage : ContentPage
{
  
    string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    
    public SignupPage()
	{
		InitializeComponent();
	}
    
    public class User
    {
        

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
    public class shortcut
    {
        private readonly FirebaseClient fbc = new FirebaseClient("https://groupmate-8d778-default-rtdb.asia-southeast1.firebasedatabase.app/");
        public async Task<bool> LoginAsync(string email)
        {
            var users = await fbc
                .Child("Users")
                .OnceAsync<User>();

            return users.Any(u => u.Object.Email == email);
        }

    }

    public async void SignupClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string firstName = FirstNameEntry.Text;
        string lastName = LastNameEntry.Text;
        string course = CoursePicker.SelectedItem?.ToString();
        
        //Field fill
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
        
        shortcut firebase = new shortcut();
        bool ket = await firebase.LoginAsync(email);
        if (ket)
        {
            await DisplayAlert("Error", "Email is used.", "OK");
            return;
        }


        Random ran = new Random();
        int po = ran.Next(100000, 999999);
        string otpcode = po.ToString();

        var udb = new User
        {
            Code = otpcode,
        };

        MimeMessage mm = new MimeMessage();
        mm.From.Add(new MailboxAddress("GroupMate App", "lianzpascua08@gmail.com"));
        mm.To.Add(MailboxAddress.Parse(email));

        mm.Subject = "Verification OTP";
        mm.Body = new TextPart("plain")
        {
            Text = $"this is your verification code: {otpcode}" 
        };
        
        SmtpClient client = new SmtpClient();
        try
        {
            string pass = "fpoufewavoyrlrcn";
            client.Connect("smtp.gmail.com", 465,true);
            client.Authenticate("lianzpascua08@gmail.com", pass);
            client.Send(mm);

            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Email Error", $"Failed to send OTP email. Please ensure your email is valid: {ex.Message}", "OK");
            return;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
        //database

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Course = course,
            Password = password,
            Code = otpcode
        };
        Application.Current.MainPage = new OtpVerificationPage(user);

        
        


    }
    private async void Login_Tapped(object sender, TappedEventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }
}