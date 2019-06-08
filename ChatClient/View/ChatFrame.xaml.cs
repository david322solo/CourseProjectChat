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
using wcf_chat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для ChatFrame.xaml
    /// </summary>
    public partial class ChatFrame : Page, IServiceChatCallback
    {
        private ServiceChatClient client;
        private string _LoginSecond;
        private string _LoginFirst;
       



        public ChatFrame(string LoginFirst,string LoginSecond)
        {
            InitializeComponent();
           _LoginFirst = LoginFirst;
            _LoginSecond = LoginSecond;
           

            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            client.Connect(LoginFirst);
            client.CreateConversation(LoginFirst, LoginSecond);
            ChatUsers.Content = "чат с " + LoginSecond;
            List<string> ChatAllMessage = client.GetAllMessage(LoginFirst, LoginSecond).ToList();
            foreach(string message in ChatAllMessage)
            {
                GetMessage.Items.Add(message);
                GetMessage.ScrollIntoView(GetMessage.Items[GetMessage.Items.Count - 1]);
            }
        }

       

        public void MsgCallback(string msg)
        {
            GetMessage.Items.Add(msg);
            GetMessage.ScrollIntoView(GetMessage.Items[GetMessage.Items.Count - 1]);

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            client.SendMsg(Message.Text, _LoginFirst, _LoginSecond);
            Message.Text = null;
        }

       

       

        private void Send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click_1(new object(), new RoutedEventArgs());
            }
        }
    }
}
