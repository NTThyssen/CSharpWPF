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


            gameMapping.Add(new PitField(pit0, label0, 0));
            gameMapping.Add(new PitField(pit1, label1, 1));
            gameMapping.Add(new PitField(pit2, label2, 2));
            gameMapping.Add(new PitField(pit3, label3, 3));
            gameMapping.Add(new PitField(pit4, label4, 4));
            gameMapping.Add(new PitField(pit5, label5, 5));
            gameMapping.Add(new PitField(pit6, label6, 6));
            gameMapping.Add(new PitField(pit7, label7, 7));
            gameMapping.Add(new PitField(pit8, label8, 8));
            gameMapping.Add(new PitField(pit9, label9, 9));
            gameMapping.Add(new PitField(pit10, label10, 10));
            gameMapping.Add(new PitField(pit11, label11, 11));
            gameMapping.Add(new PitField(pit12, label12, 12));
            gameMapping.Add(new PitField(pit13, label13, 13));

            foreach (PitField pit in gameMapping)
            {
                pit.pit.Click += ChooseField;
            }

            foreach (PitField pit in gameMapping)
            {
               
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
                    
                    turn.Content = "It is  " + player1_name.Content + "' turn";
                    if (SingletonClient.Singleton.getUsername().Equals(player1_name.Content))
                    {
                        foreach (PitField pit in gameMapping)
                        {
                            if (pit.pitNumber < 6)
                            {
                                pit.pit.IsEnabled = true;
                            }
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
                MessageBox.Show(winner + " Has won the game!" );
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
                    if (tempt <= 8)
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("kalaha"+tempt);
                        gameMapping.ElementAt(i - 2).value.Content = temp[i];

                    } 
                    else
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("kalahaMore");
                        gameMapping.ElementAt(i - 2).value.Content = temp[i];
                    }
                }
                else
                {
                    if ( tempt <= 8)
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("goal"+tempt);
                        gameMapping.ElementAt(i-2).value.Content = temp[i];
                        
                    } 
                    else
                    {
                        gameMapping.ElementAt(i-2).pit.Content = FindResource("goalMore");
                        gameMapping.ElementAt(i - 2).value.Content = temp[i];
                    }
                    
                } 
                
                turn.Content = "it is  " + playerTurn + " turn";

            }
            gameOver(winner);
           if(SingletonClient.Singleton.getUsername().Equals(playerTurn))
            {
                if (SingletonClient.Singleton.getUsername().Equals(player1_name.Content))
                {
                    foreach (PitField pit in gameMapping)
                    {
                        if (pit.pitNumber < 6 && !pit.value.Content.Equals("0"))
                        {    
                            pit.pit.IsEnabled = true;
                        }
                    }
                }else if (SingletonClient.Singleton.getUsername().Equals(player2_name.Content))
                {
                    foreach (PitField pit in gameMapping)
                    {
                        if (pit.pitNumber > 6 &&  pit.pitNumber < 13  && !pit.value.Content.Equals("0"))
                        {
                            pit.pit.IsEnabled = true;
                        }
                            
                    }
                }
                else
                {
                    foreach (PitField pit in gameMapping)
                    {
                        pit.pit.IsEnabled = false;
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
