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
    /// Логика взаимодействия для Contacts.xaml
    /// </summary>
    public partial class Contacts : Page, IServiceChatCallback
    {
        private ServiceChatClient client;
        private Users _user;
        public Contacts(Users user)
        {
            InitializeComponent();
            _user = user;
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
        }

        



      

      

        private void Find_KeyDown(object sender, KeyEventArgs e)
        {
            List<Users> users = client.GetUsers(Find.Text).ToList();
            listBox.Items.Clear();
            foreach(Users user in users)
            {
                if(!(user.IdLogin ==_user.IdLogin))
                listBox.Items.Add(user.IdLogin);
            }
        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }

        private void AddToContacts_Click(object sender, RoutedEventArgs e)
        {
            if (!listBox.SelectedItem.ToString().Equals(_user.IdLogin))
            {
                string answer = client.AddContacts(_user.IdLogin, listBox.SelectedItem.ToString());
                if (answer.Equals("FALSE") || answer.Equals("TRUE1"))
                {
                    MessageBox.Show("такой пользователь уже добавлен");
                }
            }
            else
            {
                MessageBox.Show("невозможно добавить самого себя");
            }
        }

        private void CloseContacts_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Messager.GlobalFrame.Content = new Menu(_user.IdLogin);
        }
    }
}
