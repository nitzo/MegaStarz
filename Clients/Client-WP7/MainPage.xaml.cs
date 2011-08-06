using System.Windows.Input;
using Megastar.Client.Library;
using Megastar.RestServices.Library.Entities;
using Microsoft.Phone.Controls;

namespace WP7AzureClientTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void GetTicketClick(object sender, GestureEventArgs e)
        {
            var client = new MegaStarzClient();

            var req = new GetTicketRequest();

            req.AccessToken = "148836425163954|2.AQCn5uljX2gtjJ9y.3600.1312560000.0-788734623|I7VdRze3hGBqBYj59T2VRJ4SUzY";

            client.GetTicketAsync(req, (response) => Dispatcher.BeginInvoke(() => textBox1.SelectedText = response.Star.FirstName));
        }
    }
}