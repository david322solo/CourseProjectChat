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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page,IServiceChatCallback
    {
        private ServiceChatClient client;
        private Users user;
        public static Page GMenu;
        public Menu(string login)
        {
            
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            user = client.GetUser(login);
            InitializeComponent();
            GMenu = this;
            tSubname.Content = ("" + user.Name[0] + user.Surname[0]).ToUpper();
            NameSurname.Content = user.Name + " " + user.Surname;
            Phone.Content = user.Phone.Substring(0, 4) + " " + user.Phone.Substring(4, 2) + " " + user.Phone.Substring(6, 3) +" " + user.Phone.Substring(8, 4);

        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Messager.GlobalFrame.Content = new Contacts(user);
            //Panel.SetZIndex(Messager.GlobalFrame, 0);
            //Messager.GlobalFrame.Content = 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Messager.GlobalFrame.Content = new ChangeInfo(user);
        }

        //private void Contacts_Click(object sender, RoutedEventArgs e)
        //{
        //    List<Contacts> contacts = client.GetContacts(user.IdLogin).ToList();
        //    foreach (Contacts contact in contacts)
        //    {
        //        ListContacts.Items.Add(contact.LoginUserContact);
        //    }
        //}
    }
}
