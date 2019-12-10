using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace wpfClient
{
    public partial class MenuPage : Page
    {
        
        public MenuPage()
        {
    
            InitializeComponent();
            join.Click += Join_OnClick;
            create.Click += Create_OnClick;
            lvLobby.MouseDoubleClick += Send_Click;
            refresh.Click += Refresh_OnClick;
         
            SingletonClient.Singleton.getClient().DataReceived += client_DataReceived;


            SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("REFRESH"));
         
            
        }
        
        private void listView_Click(object sender, RoutedEventArgs e)
        {

            LobbyList item = (LobbyList)lvLobby.SelectedItem;
               
            if(item != null)
            {
                SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("JOIN_CHATROOM "+ item.Lobby));
           
                Lobby lobby = new Lobby();
                NavigationService navService = NavigationService.GetNavigationService(this);
                navService.Navigate(lobby);
            }

            
        }

        public void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("REFRESH"));
        }
        
        
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            
            Debug.WriteLine("------------------------");
           
        }
        void client_DataReceived(byte[] Data, string ID)
        {
            string[] stringsplit = SingletonClient.Singleton.ConvertBytesToString(Data).Split(new string[] { " " }, 2, StringSplitOptions.None );
            switch (stringsplit[0])
            {
                case "CHATROOMS":
                    if (stringsplit.Length <= 1)
                    {
                        return;
                    }
                    string[] chatRoomMapping = stringsplit[1].Split(new string[] { " " },  StringSplitOptions.None );
                    updateChatRooms(chatRoomMapping);
                    break;
            }
            
        }

        public void updateChatRooms(string[] mapping)
        {
          
            List<LobbyList> lobbyList = new List<LobbyList>();
            for (int i = 0; i < mapping.Length-1; i = i+2)
            {
                lobbyList.Add(new LobbyList() {Lobby = mapping[i], Players = mapping[i+1]});
               
            }

            lvLobby.ItemsSource = lobbyList;
        }
        private void Join_OnClick(object sender, RoutedEventArgs e)

        {
      
            JoinLobby lobbyPage = new JoinLobby();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(lobbyPage);
        }
        
        private void Create_OnClick(object sender, RoutedEventArgs e)

        {
            CreateLobby createLobby = new CreateLobby();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(createLobby);
            navService.RemoveBackEntry();
        }

        public class LobbyList
        {
            public string Lobby { get; set; }
            public string Players { get; set; }
            
        }

        
    }
}
