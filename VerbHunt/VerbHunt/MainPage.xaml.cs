using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;


namespace VerbHunt
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //public static MobileServiceClient MobileService = new MobileServiceClient(
        //"https://verbhunt.azure-mobile.net/",
        //"itvAkqxENhoccOaCheEfjpUPLEYhRK38"
        //);
        //public static IMobileServiceTable<verbhuntb> tableClient = MobileService.GetTable<verbhuntb>();
        //public static ObservableCollection<string> gamers = new ObservableCollection<string>();

        private void Play_Click(object sender, RoutedEventArgs e)
        {

            String name = Ad.Text;

            if (name != String.Empty)
            {
                this.Frame.Navigate(typeof(Game), name);
                //AddAzure(Game._Score.Text);
            }
        }

        //public async void AddAzure(String skor)
        //{

        //    var gamer = new verbhuntb()
        //    {
        //        name = Ad.Text,
        //        score = _Score.Text,
        //    };

        //    await tableClient.InsertAsync(gamer); 
        //    gamers.Add(gamer.ToString());  

        //}
    }
}
