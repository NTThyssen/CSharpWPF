using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using NetComm;

namespace wpfClient
{
    public partial class StartPage : Page
    {
        
        public StartPage()
        {
            InitializeComponent();
            

            Button btn = new Button();
            btn.Name = "Next";
            btn.Click += Next_OnClick;
            SingletonClient.Singleton.getClient().Connected += new NetComm.Client.ConnectedEventHandler(client_Connected);

        }


        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
           
        //  SingletonClient.Singleton.getClient().Connect("10.16.187.247",3000, username.Text);
          //SingletonClient.Singleton.setUsername(username.Text);
          MenuPage menuPage = new MenuPage();
          NavigationService navService = NavigationService.GetNavigationService(this);
          navService.Navigate(menuPage);  

        }
        
        void client_DataReceived(byte[] Data, string ID)
        {
            //  Chat.AppendText(ID + ": " + ConvertBytesToString(Data) + Environment.NewLine); //Updates the log with the current connection state
        }
      

        void client_Disconnected()    
        {
            //    Chat.AppendText("Disconnected from host!" + Environment.NewLine); //Updates the log with the current connection state
        }

        void client_Connected()    
        {
            MenuPage menuPage = new MenuPage();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(menuPage);  
        }

    }
}
