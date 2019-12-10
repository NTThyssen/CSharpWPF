using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace wpfClient
{
    public partial class JoinLobby : Page
    {
        public JoinLobby()
        {
            InitializeComponent();
            joinCharByName.Click += Join_Click;
            back.Click += Back_OnClick;
        }
        
       
        private void Join_Click(object sender, RoutedEventArgs e)
        {
            SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("JOIN_CHATROOM "+ enteredChatRoom.Text));
           
            Lobby lobby = new Lobby();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(lobby);
            
        }
        
        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
           
            MenuPage menuPage = new MenuPage();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(menuPage);

        }
    }
    
    
    
}
