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
using Microsoft.Phone.Controls;
using Megastar.Client.Library;
using Megastar.RestServices.Library.Entities;
using System.IO;

namespace Client_WP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string ticket;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void GetTicketClick(object sender, GestureEventArgs e)
        {
            var client = new MegaStarzClient();

            var req = new GetTicketRequest();

            req.AccessToken = "148836425163954|2.AQC6yDgbSMJCasPO.3600.1312725600.0-788734623|IqbfIqIVHI5JF3byke-I6jDjm7Y";

            client.GetTicketAsync(req, (response) =>
            {
                Dispatcher.BeginInvoke(() => textBox1.SelectedText = response.Star.FirstName);
                ticket = response.Ticket.ticket;
            }
                );
        }

        private void UploadFileClick(object sender, GestureEventArgs e)
        {
            var client = new MegaStarzClient();

            MemoryStream m = new MemoryStream();
            string a = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] b = System.Text.Encoding.UTF8.GetBytes(a);

            for (int i = 1; i < 1024; i++)
            {
                m.Write(b, 0, b.Length);
            }

            client.UploadRecordingAsync(ticket, "1", m, (response) =>
            {
                Dispatcher.BeginInvoke(() => textBox1.SelectedText = response.playUrl);

            });


        }
    }
}