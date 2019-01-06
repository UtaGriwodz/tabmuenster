using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace tabmuenster
{
    public class Task
    {
        public string UNID { get; set; }
        public string ID { get; set; }
        public string F7 { get; set; }
        public string F1 { get; set; }
        public string F3 { get; set; }
        public string F15 { get; set; }
        public string CDate { get; set; }
        public string LCMDate { get; set; }
        public string Score { get; set; }
        public string HasCommunity { get; set; }
        public string HasAttachment { get; set; }
        public string RA { get; set; }
        public string Attachments { get; set; }
        public string Distance { get; set; }
        public string Geo_LatLng { get; set; }
        public string PathDia { get; set; }
        public string CustViewField1 { get; set; }
        public string CustViewField2 { get; set; }
    }

    public class Aufgabe
    {
        public string Quartier { get; set; }
        public string Kategorie { get; set; }
        public string Aufgabenbeschreibung { get; set; }
        public string Kategoriebild { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Aufgabe> aufgaben { get; set; }

        public Aufgabe selektierteAufgabe;

        public string ConvertF1_to_Kategorie(string f1)
        {
            switch (f1)
            {

                // text abgeschnitten

                // Gartenarbeit 1
                // Einkaufsdienste / Botengänge 2  Bild 
                // Kleine Hilfen im Haushalt 3
                // Begleithilfe 4
                // Technik 5
                // Tierpflege 6 Bild
                // Veranstaltungshilfe 7 Bild
                // Sonstiges 0 Bild
                case "1":
                    return "Gartenarbeit";
                case "2":
                    return "Einkaufsdienste / Botengänge";
                case "3":
                    return "Kleine Hilfen im Haushalt";
                case "4":
                    return "Begleithilfe";
                case "5":
                    return "Technik";
                case "6":
                    return "Tierpflege";
                case "7":
                    return "Veranstaltungshilfe";
                case "0":
                    return "Sonstiges";
            }
            return "keine Kategorie gefunden";
        }

            public string ConvertF1_to_Kategoriebild(string f1)
            {
                switch (f1)
                {
                    case "1":
                        return "ic_spa_active.png";
                    case "2":
                        return "ic_shopping_cart_active.png";
                    case "3":
                        return "ic_home.png";
                    case "4":
                        return "ic_directions_walk_active.png";
                    case "5":
                        return "ic_desktop_mac_active.png";
                    case "6":
                        return "ic_pets_active.png";
                    case "7":
                        return "ic_supervisor_account_active.png";
                    case "0":
                         return "ic_timer.png";
            }
                return "ic_add.png";
        }

        public string ConvertF7_to_Quartier(string f7)
        {
            switch (f7)
            {

                // ALbachten, Angelmodde, Amelsbüren, Berg Fidel, Coerde, Gievenbeck, Gremmendorf, Handorf, Hiltrup, 
                //Innenstadt, Kinderhaus, Mauritz, Mecklenbeck, Nienberge, Roxel, Rumphorst, Sentruper Höhe, Südviertel, Wolbeck
                case "1":
                    return "Albachten";
                case "2":
                    return "Angelmodde";
                case "3":
                    return "Amelsbüren";
                case "4":
                    return "Berg Fidel";
                case "5":
                    return "Coerde";
                case "6":
                    return "Gievenbeck";
                case "7":
                    return "Gremmendorf";
                case "8":
                    return "Handorf";
                case "9":
                    return "Hiltrup";
                case "10":
                    return "Innenstadt";
                case "11":
                    return "Kinderhaus";
                case "12":
                    return "Mauritz";
                case "13":
                    return "Mecklenbeck";
                case "14":
                    return "Nienberge";
                case "15":
                    return "Roxel";
                case "16":
                    return "Rumphorst";
                case "17":
                    return "Sentruper Höhe";
                case "18":
                    return "Südviertel";
                case "19":
                    return "Wolbeck";
            }
            return "keine Quartier gefunden (Mappingliste unvollständig)";
        }

        public int CountChar(string s, char search)
        {
            int tmp = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == search)
                {
                    tmp = tmp + 1;
                }
            }
            return tmp;
        }
        
        void OnTap(object sender, ItemTappedEventArgs e)
        {
            //Aufgabe test = new Object e.Item;
           
           // DisplayAlert("Item Tapped", e.Item.ToString(), "Ok");

           

        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            selektierteAufgabe = (Aufgabe)e.SelectedItem;
        }

        private void ButtonCallClicked(object sender)
        {
            string phoneNumber = "025114917752"; // entryPhoneNumber.Text;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return;
            }
            // Following line used to display given phone number in dialer  
            Device.OpenUri(new Uri(String.Format("tel:{0}", phoneNumber)));
        }

        private void ButtonSMSClicked(object sender)
        {
            if (selektierteAufgabe == null)
            {
                DisplayAlert("Keine Aufgabe ausgewählt", "Bitte wähle eine Aufgabe aus, die du übernehmen möchtest.", "Ok");
            }
            else
            {
                string smsPhoneNumber = "017620985995"; 
                string smsText = "Ich habe Interesse, die Aufgabe '" + selektierteAufgabe.Aufgabenbeschreibung +
                       "' aus der Kategorie '" + selektierteAufgabe.Kategorie + "' zu übernehmen." +
                       Environment.NewLine + "Mein Name: " +
                       Environment.NewLine + "Meine Telefonnummer: " +
                       Environment.NewLine + "Meine Email-Adresse: ";

                if (string.IsNullOrEmpty(smsPhoneNumber))
                {
                    return;
                }
                // Following line used to open Messages app and populate below given details  
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Device.OpenUri(new Uri(String.Format("sms:{0}&body={1}", smsPhoneNumber, smsText)));
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    Device.OpenUri(new Uri(String.Format("sms:{0}?body={1}", smsPhoneNumber, smsText)));
                }
            }
        }

        private void ButtonMailClicked(object sender)
        {
            if (selektierteAufgabe == null)
            {
                DisplayAlert("Keine Aufgabe ausgewählt", "Bitte wähle eine Aufgabe aus, die du übernehmen möchtest.", "Ok");
            }
            else
            {
                string toEmail = "tab@muenster.de"; 
                string emailSubject = "Interesse an: " + selektierteAufgabe.Aufgabenbeschreibung; 
                string emailBody = "Ich habe Interesse, die Aufgabe '" + selektierteAufgabe.Aufgabenbeschreibung + 
                       "' aus der Kategorie '" + selektierteAufgabe.Kategorie + "' zu übernehmen." +
                       Environment.NewLine + "Mein Name: " +
                       Environment.NewLine + "Meine Telefonnummer: " +
                       Environment.NewLine + "Meine Email-Adresse: ";
                if (string.IsNullOrEmpty(toEmail))
                {
                    return;
                }
                Device.OpenUri(new Uri(String.Format("mailto:{0}?subject={1}&body={2}", toEmail, emailSubject, emailBody)));
            }
        }


        private void ButtonInfoClicked(object sender)
        {

            string ein_text = "Die Taschengeldbörse Münster – ein Gewinn für Jung und Alt" + Environment.NewLine +
                 Environment.NewLine +
"Ältere Menschen benötigen bei einfachen, ungefährlichen, haushaltsnahen Tätigkeiten gelegentlich Unterstützung zu kleinem Preis." + Environment.NewLine +
"Jugendliche suchen Möglichkeiten unkompliziert und ohne dauerhafte Verpflichtung ihr Taschengeld aufzubessern, um sich den einen oder anderen Wunsch erfüllen zu können." + Environment.NewLine +
"Die Taschengeldbörse Münster bringt Jung und Alt zusammen und bietet Jugendlichen und Seniorinnen und Senioren eine gemeinsame Plattform und Vermittlungsstelle." + Environment.NewLine +
 Environment.NewLine +
"Träger und Kooperationspartner: " + Environment.NewLine +
 "Die Taschengeldbörse ist ein Projekt der Stiftung Magdalenenhospital - Stadtteilinitiativen 'Von Mensch zu Mensch', Kooperationspartner ist die Kommunale Seniorenvertretung Münster." + Environment.NewLine +
 Environment.NewLine +
"Offene Aufgaben - Du möchtest einen der offenen Aufgaben übernehmen?" + Environment.NewLine +
"Dann" + Environment.NewLine +
"    ruf uns an 0251 / 14917752 (immer mit Vorwahl wählen und außerhalb der Sprechstunde gerne auf den AB sprechen)" + Environment.NewLine +
"    oder" + Environment.NewLine +
"    schreib uns eine SMS an 0176 / 20985995" + Environment.NewLine +
"    oder" + Environment.NewLine +
"    schreib uns eine E - Mail an tab@muenster.de" + Environment.NewLine +
"Wir melden uns daraufhin schnellstmöglich bei dir." + Environment.NewLine +
Environment.NewLine +
"Beachte: Sollten sich mehrere Jugendliche auf einen offenen Aufgaben melden, dann gilt das Prinzip 'wer zuerst kommt, mahlt zuerst', wobei wir natürlich darauf achten die Aufgaben gerecht unter euch zu verteilen." + Environment.NewLine +
Environment.NewLine +
"https://www.taschengeldboerse-muenster.de";
  
            DisplayAlert("Information zur Taschengeldbörse Münster", ein_text, "Ok");
      

        }



        private void ButtonImpressumClicked(object sender)
        {

            string ein_text = "Die Taschengeldbörse Münster – ein Gewinn für Jung und Alt" + Environment.NewLine +
                 Environment.NewLine +
                 "Träger und Kooperationspartner: " + Environment.NewLine +
            "Die Taschengeldbörse ist ein Projekt der Stiftung Magdalenenhospital - Stadtteilinitiativen 'Von Mensch zu Mensch', Kooperationspartner ist die Kommunale Seniorenvertretung Münster." + Environment.NewLine +
            Environment.NewLine +
            "https://www.taschengeldboerse-muenster.de" +
            Environment.NewLine +
            "Einige Icons der App sind von: https://icons8.de" +
             Environment.NewLine +
                 "Die Grundlagen zu dieser App stammen vom  Münsterhack 2018: https://www.muensterhack.de/";

            DisplayAlert("Information zur Taschengeldbörse Münster", ein_text, "Ok");

        }

        public MainPage()
        {
            InitializeComponent();

            try
            {
                string url = "https://cloud-11.datenbanken24.de/apps/tab/public.nsf/mobileRequest?openagent&callback=db24&FN=F3";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/jsonp";
                HttpWebResponse myResp = (HttpWebResponse)request.GetResponse();
                string responseText;

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                        int anzahl = CountChar(responseText, '{');
                        int endPosition = 1;
                        int startPosition = 0;
                        string word42 = "";

                        Task[] arrayt = new Task[anzahl - 1];
                        Aufgabe[] arraya = new Aufgabe[anzahl - 1];
                        int y = 0;
                        do
                        {
                            startPosition = responseText.IndexOf("{", endPosition) + 1;
                            endPosition = responseText.IndexOf("}", startPosition);
                            string word22 = responseText.Substring(startPosition, endPosition - startPosition);
                            string word32 = word22.Replace("\"", "\'");
                            word42 = "{ " + word32 + "}";
                            arrayt[y] = JsonConvert.DeserializeObject<Task>(word42);
                            arraya[y] = new Aufgabe { Quartier = ConvertF7_to_Quartier(arrayt[y].F7), Kategorie = ConvertF1_to_Kategorie(arrayt[y].F1), Aufgabenbeschreibung = arrayt[y].F3, Kategoriebild = ConvertF1_to_Kategoriebild(arrayt[y].F1) };
                            y++;
                        }
                        while
                        (y < anzahl - 1);

                        var layout1 = new StackLayout { Padding = new Thickness(5, 10) };
                        layout1.BackgroundColor = Color.FromHex("e8e8e8");
                         
                        var IconImage = new Image
                        {
                            Source = "ic_launcher_round.png",
                            WidthRequest = 65,
                            HeightRequest = 65,
                            MinimumHeightRequest = 65,
                            MinimumWidthRequest = 65,
                          //  VerticalOptions = LayoutOptions.Start,
                          //  HorizontalOptions = LayoutOptions.Start,
                         //  BackgroundColor = Color.White, //FromHex("94C123"),
                        };

                        //  layout1.HorizontalOptions = LayoutOptions.Fill;
                        var label1 = new Label { Text = "Taschengeldbörse", TextColor = Color.FromHex("#2D8B29"), FontFamily = "Cookie", FontSize = 30 , HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };


                        var ImpressumImageButton = new Image
                        {
                            Source = "icons8_information_100.png",
                            WidthRequest = 25,
                            HeightRequest = 25,
                            MinimumHeightRequest = 25,
                            MinimumWidthRequest = 25,
                         //  BackgroundColor = Color.FromHex("94C123"),
                        };
                        var tapGestureRecognizer5 = new TapGestureRecognizer();
                        tapGestureRecognizer5.Command = new Command(ButtonImpressumClicked);
                        ImpressumImageButton.GestureRecognizers.Add(tapGestureRecognizer5);

                        var horizontalLayout2 = new StackLayout() { }; //  BackgroundColor = Color.FromHex("94C123") };
                        horizontalLayout2.Orientation = StackOrientation.Horizontal;
                        horizontalLayout2.HorizontalOptions = LayoutOptions.Center;
                        horizontalLayout2.Children.Add(IconImage);
                        horizontalLayout2.Children.Add(label1);
                        horizontalLayout2.Children.Add(ImpressumImageButton);

                        layout1.Children.Add(horizontalLayout2);
                     //   layout1.Children.Add(label1);
                        aufgaben = new ObservableCollection<Aufgabe>();
                        ListView lstView = new ListView();
                        lstView.RowHeight = 100;
                        lstView.ItemTapped += OnTap;
                      
                        
                        lstView.ItemSelected += OnSelection;
                        this.Title = "Aufgaben der Taschengeldbörse Münster";
                        lstView.ItemTemplate = new DataTemplate(typeof(CustomAufgabeCell));
                     
                      

                        int i = 0;
                        do
                        {
                            aufgaben.Add(arraya[i]);
                            i++;
                        }
                        while
                        (i < anzahl - 1);
                        lstView.ItemsSource = aufgaben;
                        layout1.Children.Add(lstView);

                        var CallImageButton = new Image { Source = "icons8telefon100.png",
                             WidthRequest = 75,
                            HeightRequest = 75,
                            MinimumHeightRequest = 75,
                            MinimumWidthRequest = 75,
                            VerticalOptions = LayoutOptions.Center,
                           HorizontalOptions = LayoutOptions.Center,
                            BackgroundColor = Color.FromHex("94C123"),
                        };
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Command = new Command(ButtonCallClicked);
                        CallImageButton.GestureRecognizers.Add(tapGestureRecognizer);
                        
                        var SMSImageButton = new Image { Source = "icons8sms100.png",
                            WidthRequest = 75,
                            HeightRequest = 75,
                            MinimumHeightRequest = 75,
                            MinimumWidthRequest = 75,
                            BackgroundColor = Color.FromHex("94C123"),
                         };
                        var tapGestureRecognizer2 = new TapGestureRecognizer();
                        tapGestureRecognizer2.Command = new Command(ButtonSMSClicked);
                        SMSImageButton.GestureRecognizers.Add(tapGestureRecognizer2);

                        var MailImageButton = new Image { Source = "icons8nachricht100.png",
                            WidthRequest = 75,
                            HeightRequest = 75,
                            MinimumHeightRequest = 75,
                            MinimumWidthRequest = 75,
                            BackgroundColor = Color.FromHex("94C123"),
                         };
                        var tapGestureRecognizer3 = new TapGestureRecognizer();
                        tapGestureRecognizer3.Command = new Command(ButtonMailClicked);
                        MailImageButton.GestureRecognizers.Add(tapGestureRecognizer3);


                        var InfoImageButton = new Image
                        {
                            Source = "icons8_info_100_ugr.png",
                            WidthRequest = 75,
                            HeightRequest = 75,
                            MinimumHeightRequest = 75,
                            MinimumWidthRequest = 75,
                            BackgroundColor = Color.FromHex("94C123"),
                        };
                        var tapGestureRecognizer4 = new TapGestureRecognizer();
                        tapGestureRecognizer4.Command = new Command(ButtonInfoClicked);
                        InfoImageButton.GestureRecognizers.Add(tapGestureRecognizer4);

                        var horizontalLayout = new StackLayout() { BackgroundColor = Color.White };
                        horizontalLayout.Orientation = StackOrientation.Horizontal;
                        horizontalLayout.HorizontalOptions = LayoutOptions.Center;
                        horizontalLayout.Children.Add(MailImageButton);
                        horizontalLayout.Children.Add(CallImageButton);
                        horizontalLayout.Children.Add(SMSImageButton);
                        horizontalLayout.Children.Add(InfoImageButton);
                        layout1.Children.Add(horizontalLayout);

                        this.Content = layout1;
                    }
                }
            }

            catch (WebException exception)
            {
                string responseText;
                using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                    DisplayAlert(responseText, "Fehler", "OK");
                }
            }
        }

        public class CustomAufgabeCell : ViewCell
        {

            public CustomAufgabeCell()
            {
                //instantiate each of our views

                var image = new Image{ 
                            WidthRequest = 30,
                            HeightRequest = 30,
                            MinimumHeightRequest = 30,
                            MinimumWidthRequest = 30};

               var KategorieLabel = new Label();
                var QuartierLabel = new Label();
                var AufgabenbeschreibungLabel = new Label();
                var verticaLayout = new StackLayout();
                var horizontalLayout = new StackLayout() { BackgroundColor = Color.White, Margin = new Thickness(2) };

             //   var layout1 = new StackLayout { Padding = new Thickness(5, 10) };


                //set bindings
                KategorieLabel.SetBinding(Label.TextProperty, new Binding("Kategorie"));
                KategorieLabel.TextColor = Color.FromHex("2D8B29");
                KategorieLabel.FontFamily = "Cookie";
                QuartierLabel.SetBinding(Label.TextProperty, new Binding("Quartier"));
                QuartierLabel.FontFamily = "Raleway";
                QuartierLabel.TextColor = Color.FromHex("E30D2D");
                AufgabenbeschreibungLabel.SetBinding(Label.TextProperty, new Binding("Aufgabenbeschreibung"));
                AufgabenbeschreibungLabel.TextColor = Color.FromHex("444");
                AufgabenbeschreibungLabel.FontFamily = "Raleway";
                image.SetBinding(Image.SourceProperty, new Binding("Kategoriebild"));

                //Set properties for desired design
                horizontalLayout.Orientation = StackOrientation.Horizontal;
                horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
                image.HorizontalOptions = LayoutOptions.End;
                KategorieLabel.FontSize = 24;

                //add views to the view hierarchy
                verticaLayout.Children.Add(QuartierLabel);
                verticaLayout.Children.Add(KategorieLabel);
                verticaLayout.Children.Add(AufgabenbeschreibungLabel);
                horizontalLayout.Children.Add(image);
                horizontalLayout.Children.Add(verticaLayout);
                               
         
                // add to parent view
                View = horizontalLayout;

            }

           

        }

     }
 }
