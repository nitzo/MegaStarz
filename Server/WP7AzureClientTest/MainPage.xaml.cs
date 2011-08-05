using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Megastar.AzureWP7Client;
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
            var client = new WP7Client();

            var req = new GetTicketRequest();

            req.AccessToken = "148836425163954|2.AQCn5uljX2gtjJ9y.3600.1312560000.0-788734623|I7VdRze3hGBqBYj59T2VRJ4SUzY";

            client.GetTicketAsync(req, (response) => Dispatcher.BeginInvoke(() => textBox1.SelectedText = response.Star.FirstName));
        }
    }
}