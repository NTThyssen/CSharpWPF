using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace wpfClient
{
    public partial class Lobby : Page
    {
        public Lobby()
        {
            InitializeComponent();
            sendMessage.Click += Send_Click;
            SingletonClient.Singleton.getClient().DataReceived += client_DataReceived;
        }
        
        
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("SEND_MESSAGE " + textMessage.Text));
           
            Lobby lobbyView = new Lobby();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(lobbyView);
            
        }

        void client_DataReceived(byte[] Data, string ID)
        {
            chatView.Text = (SingletonClient.Singleton.ConvertBytesToString(Data) + Environment.NewLine); //Updates the log with the current connection state
        }
      

        void client_Disconnected()
        {
            //    Chat.AppendText("Disconnected from host!" + Environment.NewLine); //Updates the log with the current connection state
        }

        void client_Connected()
        {
            // Chat.AppendText("Connected succesfully!" + Environment.NewLine); //Updates the log with the current connection state
        }
    }
}
