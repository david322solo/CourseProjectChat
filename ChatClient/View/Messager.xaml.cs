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
using System.Windows.Shapes;
using ChatClient.ServiceChat;
using wcf_chat;
namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Messager.xaml
    /// </summary>
    public partial class Messager : Window, IServiceChatCallback
    {
        ServiceChatClient client;
        public static Frame GlobalFrame;
        public static Frame GlobalBodyFrame;
        public static Canvas GlobalCanvas;
        public static Frame GlobalOptions;
        private Users _user;
        public Messager(string login)
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            
            Users user = client.GetUser(login);
            _user = user;
            InitializeComponent();
            Menu.NavigationService.Navigate(new LeftMenu(user));
            GlobalFrame = Menu;
            GlobalBodyFrame = ChatMessage;
            GlobalCanvas = Canvas;
        }

        public void MsgCallback(string msg)
        {
            MessageBox.Show(msg);
        }

        private void Wind_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.Disconnect(_user.IdLogin);
        }
    }
}
