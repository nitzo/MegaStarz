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

namespace WindowsPhoneHelloWorldApp
{
    public partial class MainPage : PhoneApplicationPage
    {

        private CaptureSource c;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            c = new CaptureSource();
            c.VideoCaptureDevice = CaptureDeviceConfiguration.GetAvailableVideoCaptureDevices().First();

            var vidBrush = new VideoBrush();
            vidBrush.SetSource(c);
            ViewPort.Fill = vidBrush;         
                        
        }

        private void Button_OnTap(object sender, GestureEventArgs e)
        {
            // Request webcam access and start the capturing
            if (CaptureDeviceConfiguration.RequestDeviceAccess())
            {
                c.Start();
            }
        }
    }
}