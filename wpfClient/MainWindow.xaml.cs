using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.SignalR.Client;

namespace wpfClient
{
    /// <summary> 
    /// SignalR client hosted in a WPF application. The client 
    /// lets the user pick a user name, connect to the server asynchronously 
    /// to not block the UI thread, and send chat messages to all connected  
    /// clients whether they are hosted in WinForms, WPF, or a web application. 
    /// For simplicity, MVVM will not be used for this sample. 
    /// </summary> 
    public partial class MainWindow : Window
    {
        /// <summary> 
        /// This name is simply added to sent messages to identify the user; this  
        /// sample does not include authentication. 
        /// </summary> 
        
        NetComm.Client client; //The client object used for the communication

        public MainWindow()
        {
            InitializeComponent();

            
            client = new NetComm.Client(); //Initialize the client object

            //Adding event handling methods for the client
            client.Connected += new NetComm.Client.ConnectedEventHandler(client_Connected);
            client.Disconnected += new NetComm.Client.DisconnectedEventHandler(client_Disconnected);
            client.DataReceived += new NetComm.Client.DataReceivedEventHandler(client_DataReceived);

            //Connecting to the host
            client.Connect("10.16.184.154", 3000, "Jack"); //Connecting to the host (on the same machine) with port 2020 and ID "Jack"
            frame1.Content = new StartPage();
            Button btn = new Button();
            btn.Name = "Next";
            btn.Click += Send_Click;
  
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
        
        
        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
            frame1.Content = new MenuPage(); //Show page2
            this.Close(); //this will close Page1
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


