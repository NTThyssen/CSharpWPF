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
            
            
            //Adding event handling methods for the client
          

            //Connecting to the host
           
            Button btn = new Button();
            btn.Name = "Next";
            btn.Click += Next_OnClick;


        }


        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
           SingletonClient.Singleton.getClient().Connect("10.16.177.126",3000, username.Text);


           if (SingletonClient.Singleton.getClient().isConnected)
           {
               MenuPage menuPage = new MenuPage();
               NavigationService navService = NavigationService.GetNavigationService(this);
               navService.Navigate(menuPage);   
           }
          
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
            // Chat.AppendText("Connected succesfully!" + Environment.NewLine); //Updates the log with the current connection state
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            //  client.SendData(ConvertStringToBytes(MessageTextBox.Text)); //Sends the message to the host
            // Chat.AppendText( MessageTextBox.Text + Environment.NewLine);
            // MessageTextBox.Clear(); //Clears the chatmessage textbox text
            
        }
        
        string ConvertBytesToString(byte[] bytes)
        {
            return ASCIIEncoding.ASCII.GetString(bytes);
        }

        byte[] ConvertStringToBytes(string str)
        {
            return ASCIIEncoding.ASCII.GetBytes(str);
        }
    }
}
