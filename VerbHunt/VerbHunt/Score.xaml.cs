using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace VerbHunt
{
    public sealed partial class Score : Page
    {
        public Score()
        {
            this.InitializeComponent();
        }

        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {           
            Game.Sender send = e.Parameter as Game.Sender;
        
            if (send != null)
            {
                if (Convert.ToInt32(send.Score) >= 2500)
                {
                    _Ad.Text = "Harikasın " + send.Name + "!";
                    Puan.Text = send.Score.ToString();
                }
                else if (Convert.ToInt32(send.Score) >= 1000)
                {
                    _Ad.Text = "Daha iyisin " + send.Name + "!";
                    Puan.Text = send.Score.ToString();
                }
                else if (Convert.ToInt32(send.Score) < 1000)
                {
                    _Ad.Text = "Gayretli Ol " + send.Name + "!";
                    Puan.Text = send.Score.ToString();
                }
                else if (Convert.ToInt32(send.Score) <= -100)
                {
                    _Ad.Text = "Tekrar Şart " + send.Name + "!";
                    Puan.Text = send.Score.ToString();
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game));
            Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1); //delete page from navigation                    
        }

        private async void Rate_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

    }
}
