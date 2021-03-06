using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ServiceChat.IServiceCallback
    {
        bool isConnected = false;
        ServiceClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                tbmsg.IsEnabled = false;
                client = new ServiceClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                Bconnect.Content = "Отключиться";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                tbmsg.IsEnabled = true;
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                tbmsg.IsEnabled = false;
                Bconnect.Content = "Подключиться";
                isConnected = true;
            }
        }

        private void Button_click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCallBack(string Message)
        {
            lbchat.Items.Add(Message);
            lbchat.ScrollIntoView(lbchat.Items[lbchat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser(); // делаем так, чтобы сервер нас отключал, когда мы нажимаем на крестик
        }

        private void tbmsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(tbmsg.Text, ID);
                    tbmsg.Text = string.Empty;
                }

            }
        }
    }
}
