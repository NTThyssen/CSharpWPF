using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
        }
    }
}
