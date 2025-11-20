namespace groupmate;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void OnNotificationClicked(object sender, EventArgs e)
    {
        DisplayAlert("Notification", "You clicked Notifications.", "OK");
    }

    private void OnProfileClicked(object sender, EventArgs e)
    {
        DisplayAlert("Profile", "You clicked Profile.", "OK");
    }

    private void OnGroup1Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlert("Group 1", "You selected Group IMP.", "OK");
    }

    private void OnGroup2Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlert("Group 2", "You selected Group App Dev.", "OK");
    }

    private void OnGroup3Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlert("Group 3", "You selected Group MoR.", "OK");
    }
}