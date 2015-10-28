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
using Windows.Storage;
using Windows.Media;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;

namespace VerbHunt
{
  
    public sealed partial class Game : Page
    {
        public Game()
        {
            this.InitializeComponent();

            kelimeVer();         
        }

        public static MobileServiceClient MobileService = new MobileServiceClient(
        "https://verbhunt.azure-mobile.net/",
        "itvAkqxENhoccOaCheEfjpUPLEYhRK38"
        );
        public static IMobileServiceTable<verbhuntb> tableClient = MobileService.GetTable<verbhuntb>();
        public static ObservableCollection<string> gamers = new ObservableCollection<string>();

        public async void AddAzure(String skor)
        {

            var gamer = new verbhuntb()
            {
                name = _ad.Text,
                score = _Score.Text,
            };

            await tableClient.InsertAsync(gamer);
            gamers.Add(gamer.ToString());

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            time = 60;
            refreshTime = new DispatcherTimer();
            refreshTime.Interval = new TimeSpan(0, 0, 1);
            refreshTime.Tick += new EventHandler<object>(refreshTime_Tick);
            refreshTime.Start();
         
            String _name = e.Parameter as String;
            if (_name != null)
            {
                _ad.Text = _name;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            refreshTime.Stop();

            AddAzure(Name);
        }

        public class Sender
        {
            public string Name { get; set; }
            public string Score { get; set; }
        }

        DispatcherTimer refreshTime;
        int time = 60;
        public void refreshTime_Tick(object sender, object e)
        {
            Timer.Text = time.ToString();
            time--;

            if (Timer.Text == "0")
            {
                refreshTime.Stop();

                Sender send = new Sender { Name = _ad.Text, Score = _Score.Text };
                this.Frame.Navigate(typeof(Score), send);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            puan = 0;
            _Score.Text = string.Empty;
            _Score.Text = "0";
        }

        string[] verb_1 = { "awake", "be", "beat", "become", "begin", "bend", "bet", "bid", "bite", "blow", "break", "bring", "broadcast", "burst", "burn", "buy", "catch", "choose", "come", "cost", "creep", "cut", "deal", "dig", "do", "draw", "dream", "drive", "drink", "eat", "fall", "feed", "feel", "fight", "find", "flee", "fly", "forbid", "forget", "forgive", "freeze", "get", "give", "go", "grow", "hang", "have", "hear", "hide", "hit", "hold", "hurt", "keep", "know", "lay", "lead", "learn", "leave", "lend", "lie", "lose", "make", "mean", "meet", "pay", "put", "read", "ride", "ring", "rise", "run", "say", "see", "sell", "send", "show", "shut", "sing", "sit", "sleep", "speak", "spend", "stand", "swim", "take", "teach", "tear", "tell", "think", "throw", "understand", "wake", "wear", "win", "write" };
        string[] verb_2 = { "awoke", "was/were", "beat", "became", "began", "bent", "bet", "bid", "bit", "blew", "broke", "brought", "broadcast", "built", "burst", "burnt", "bought", "caught", "chose", "came", "cost", "crept", "cut", "dealt", "dug", "did", "drew", "dreamt", "drove", "drank", "ate", "fell", "fed", "felt", "fought", "found", "fled", "flew", "forbade", "forgot", "forgave", "froze", "got", "gave", "went", "grew", "hung", "had", "heard", "hid", "hit", "held", "hurt", "knew", "laid", "led", "learnt", "left", "lent", "let", "lay", "lost", "made", "meant", "met", "paid", "put", "read", "rode", "rang", "rose", "ran", "said", "saw", "sold", "sent", "showed", "shut", "sang", "sat", "slept", "spoke", "spent", "stood", "swam", "took", "tore", "told", "thought", "threw", "understood", "woke", "wore", "won", "wrote" };
        string[] verb_3 = { "awoken", "been", "beaten", "become", "begun", "bent", "bet", "bid", "bitten", "blown", "broken", "brought", "broadcast", "built", "burst", "burnt", "bought", "caught", "chosen", "come", "cost", "crept", "cut", "dealt", "dug", "done", "drawn", "dreamed/dreamt", "driven", "drunk", "eaten", "fallen", "fed", "felt", "fought", "found", "fled", "flown", "forbidden", "forgotten", "forgiven", "frozen", "gotten", "given", "gone", "grown", "hung", "had", "heard", "hidden", "hit", "held", "hurt", "kept", "known", "laid", "led", "learned/learnt", "left", "lent", "let", "lain", "lost", "made", "meant", "met", "paid", "put", "read", "ridden", "rung", "risen", "run", "said", "seen", "sold", "sent", "shown", "shut", "sung", "sat", "slept", "spoken", "spent", "stood", "swum", "taken", "taught", "torn", "told", "thought", "thrown", "understood", "woken", "worn", "won", "written" };

        private void kelimeVer()
        {

            int diziElemanSayisi = verb_1.Length;
            Random rasgeleSayi = new Random();
            int randVerbs = rasgeleSayi.Next(0, diziElemanSayisi);
            int randVerb = rasgeleSayi.Next(1, 4);
            Verb.Text = randVerb.ToString();

            Random rasgeleSayiDiger = new Random();
            int rasgeleSalla = rasgeleSayiDiger.Next(1, 4);
            if (rasgeleSalla == 1) 
            {
                Verbs.Text = verb_1[randVerbs];
            }
            else if (rasgeleSalla == 2) 
            {
                Verbs.Text = verb_2[randVerbs];
            }
            else
            {
                Verbs.Text = verb_3[randVerbs];
            }
        }

        int puan = 0;
        private void True_Click(object sender, RoutedEventArgs e)
        {

            string sorulanKelime = Verbs.Text;
            int sorulanHal = Convert.ToInt32(Verb.Text);
            puan = Convert.ToInt32(_Score.Text);
            if (sorulanHal == 1)
            {
                if (verb_1.Contains(sorulanKelime))
                {
                    _Score.Text = (puan + 100).ToString();
                    TrueSound.Play();
                }
                else
                {
                    _Score.Text = (puan - 100).ToString();
                    FalseSound.Play();
                }
            }
            else if (sorulanHal == 2)
            {
                if (verb_2.Contains(sorulanKelime))
                {
                    _Score.Text = (puan + 100).ToString();
                    TrueSound.Play();
                }
                else
                {
                    _Score.Text = (puan - 100).ToString();
                    FalseSound.Play();
                }
            }
            else
            {
                if (verb_3.Contains(sorulanKelime))
                {
                    _Score.Text = (puan + 100).ToString();
                    TrueSound.Play();
                }
                else
                {
                    _Score.Text = (puan - 100).ToString();
                    FalseSound.Play();
                }
            }

            kelimeVer();
        }

        private void False_Click(object sender, RoutedEventArgs e)
        {

            string sorulanKelime = Verbs.Text;
            int sorulanHal = Convert.ToInt32(Verb.Text);
            int puan = Convert.ToInt32(_Score.Text);
            if (sorulanHal == 1)
            {
                if (verb_1.Contains(sorulanKelime))
                {
                    _Score.Text = (puan - 100).ToString();
                    FalseSound.Play();
                }
                else
                {
                    _Score.Text = (puan + 100).ToString();
                    TrueSound.Play();
                }
            }
            else if (sorulanHal == 2)
            {
                if (verb_2.Contains(sorulanKelime))
                {
                    _Score.Text = (puan - 100).ToString();
                    FalseSound.Play();
                }
                else
                {
                    _Score.Text = (puan + 100).ToString();
                    TrueSound.Play();
                }
            }
            else
            {
                if (verb_3.Contains(sorulanKelime))
                {
                    _Score.Text = (puan - 100).ToString();
                    FalseSound.Play();
                }
                else
                {
                    _Score.Text = (puan + 100).ToString();
                    TrueSound.Play();
                }
            }
            kelimeVer();
           
        }

       // public static string Score = puan;
        public object _name { get; set; }


    }
}

