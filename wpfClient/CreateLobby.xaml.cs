using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace wpfClient
{
    enum commands{
         
    }
    
    public partial class CreateLobby : Page
    {
        
        List<string> cmd = new List<string>();
        
        public CreateLobby()
        {
            InitializeComponent();
            createLobby.Click += Create_Click;
            cmd.Add("NEW_CHATROOM");
            back.Click += Back_OnClick;


        }
        
        
        private void Create_Click(object sender, RoutedEventArgs e)
        {
           // SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("NEW_CHATROOM "+ LobbyName.Text));
           
            Lobby lobbyView = new Lobby();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(lobbyView);
            
        }
        
        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
           
            MenuPage menuPage = new MenuPage();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(menuPage);

        }
    }
}
