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
        
       

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            frame1.Content = new StartPage();
      
       
  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }




     }
}


