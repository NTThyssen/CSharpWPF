using System;
using System.Text;
using System.Windows.Media.Animation;
using NetComm;

namespace wpfClient
{
    public class SingletonClient
    {
       private Client _client;
       private static SingletonClient instance;
       public string username
       {
           get => username; 
           set => username = value;
       }

       private SingletonClient() { }

        
        public static SingletonClient Singleton
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonClient();
                }

                return instance;
            }
         }

       
        public Client getClient()
        {
            if (_client == null)
            {
               _client = new Client();
            }

            return _client;
        }

        public string ConvertBytesToString(byte[] bytes)
        {
            return ASCIIEncoding.ASCII.GetString(bytes);
        }

        public  byte[] ConvertStringToBytes(string str)
        {
            return ASCIIEncoding.ASCII.GetBytes(str);
        }
    }
}