using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace wpfClient
{
    public partial class Lobby : Page
    {
      //  public ObservableCollection<Message> Msg { get; set; }
        
        private List<PitField> gameMapping = new List<PitField>();
       
        
        public ObservableCollection<string> Names { get; set; }

        public Lobby()
        {
            Names = new ObservableCollection<string>();

            InitializeComponent();
            DataContext = this;
            sendMessage.Click += Send_Click;
            back.Click += Back_OnClick;
            SingletonClient.Singleton.getClient().DataReceived += client_DataReceived;
            SingletonClient.Singleton.getClient().Disconnected += client_Disconnected;
            SingletonClient.Singleton.getClient().Connected += client_Connected;


            gameMapping.Add(new PitField(pit0, 6, 0));
            gameMapping.Add(new PitField(pit1, 6, 1));
            gameMapping.Add(new PitField(pit2, 6, 2));
            gameMapping.Add(new PitField(pit3, 6, 3));
            gameMapping.Add(new PitField(pit4, 6, 4));
            gameMapping.Add(new PitField(pit5, 6, 5));
            gameMapping.Add(new PitField(pit6, 6, 6));
            gameMapping.Add(new PitField(pit7, 6, 7));
            gameMapping.Add(new PitField(pit8, 6, 8));
            gameMapping.Add(new PitField(pit9, 6, 9));
            gameMapping.Add(new PitField(pit10, 6, 10));
            gameMapping.Add(new PitField(pit11, 6, 11));
            gameMapping.Add(new PitField(pit12, 6, 12));
            gameMapping.Add(new PitField(pit13, 6, 13));

            foreach (PitField pit in gameMapping)
            {
                pit.pit.Click += ChooseField;
            }

            foreach (PitField pit in gameMapping)
            {
                if (pit.pitNumber >= 6)
                    pit.pit.IsEnabled = false;
            }
           
        }
        
        
        private void Send_Click(object sender, RoutedEventArgs e)
        {
           SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("SEND_MESSAGE " + textMessage.Text));
         
        }

        private void ChooseField(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int pitNumberClicked = 0;
            foreach (PitField pit in gameMapping)
            {
                if (pit.pit.Name.Equals(btn.Name))
                {
                    pitNumberClicked = pit.pitNumber;
                    
                }

                pit.pit.IsEnabled = false;
            }
            
            SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("KALAHA " + pitNumberClicked));
            

        }

        void client_DataReceived(byte[] Data, string ID)
        {

            string[] stringsplit = SingletonClient.Singleton.ConvertBytesToString(Data).Split(new string[] { " " }, 2, StringSplitOptions.None );

            switch (stringsplit[0])
            {
                case "NEW_CHATROOM_CONFIRMED":
                    break;
                case "KALAHA":
                    updateFields(stringsplit[1].Split(new string[] {" "}, 16, StringSplitOptions.None));
                  
                    break;
                case "USER_JOINED":
                    Names.Add(SingletonClient.Singleton.ConvertBytesToString(Data));
                    listBox.ScrollIntoView(listBox.Items[listBox.Items.Count-1]);
                    
                    break;
                case "PLAYER_NAMES":
                    string[] tempNames =  stringsplit[1].Split(new string[] {" "}, 2, StringSplitOptions.None);
                    player1_name.Content = tempNames[0];
                    player2_name.Content = tempNames[1];
                    if (SingletonClient.Singleton.getUsername().Equals(player2_name.Content))
                    {
                        foreach (PitField pit in gameMapping)
                        {
                            pit.pit.IsEnabled = false;
                        }
                    }
                    break;
                    
                case "SEND_MESSAGE":
                    Names.Add(stringsplit[1]);
                    listBox.ScrollIntoView(listBox.Items[listBox.Items.Count-1]);
                    break;
                default:
                    break;
               
                
            }
          
       
        }

        public void gameOver(string winner)
        {
            if (!winner.Equals("undecided"))
            {
                MessageBox.Show("Has won the game "+ winner);
            }
        }
        public void updateFields(string[] temp)
        {
            string playerTurn = temp[0];
            string winner = temp[1];
            
            for(int i = 2; i<temp.Length; i++)
            {
                int tempt = int.Parse(temp[i]);
                if(i-2 !=6 && i-2 != 13){   
                    if (int.Parse(temp[i]) <= 8)
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("kalaha"+tempt);
                        
                    } 
                    else
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("kalahaMore");
                    }
                }
                else
                {
                    if (int.Parse(temp[i]) <= 8)
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("goal"+tempt);
                        
                    } 
                    else
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("goalMore");
                    }
                    
                } 
                gameOver(winner);
                  
            }

            if(SingletonClient.Singleton.getUsername().Equals(playerTurn))
            {
                if (SingletonClient.Singleton.getUsername().Equals(player1_name.Content))
                {
                    foreach (PitField pit in gameMapping)
                    {
                        if (pit.pitNumber < 6 && pit.pit.Content != FindResource("kalaha0"))
                        {
                            pit.pit.IsEnabled = true;
                        }
                    }
                }else if (SingletonClient.Singleton.getUsername().Equals(player2_name.Content))
                {
                    foreach (PitField pit in gameMapping)
                    {
                        if (pit.pitNumber > 6 &&  pit.pitNumber < 13 && pit.pit.Content != FindResource("kalaha0"))
                        {
                            pit.pit.IsEnabled = true;
                        }
                            
                    }
                }

                     
                       
            }
        }
      
        
        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            SingletonClient.Singleton.getClient().SendData(SingletonClient.Singleton.ConvertStringToBytes("LEAVE_CHATROOM"));
            
            MenuPage menuPage = new MenuPage();
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(menuPage);

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
