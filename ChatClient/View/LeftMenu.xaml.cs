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
    /// Логика взаимодействия для LeftMenu.xaml
    /// </summary>
    public partial class LeftMenu : Page, IServiceChatCallback
    {
        public static wcf_chat.Users user;
        public static LeftMenu GLeftMenu;
        private ServiceChatClient client;
        private bool _flag = false;
        public LeftMenu(wcf_chat.Users _user)
        {
            InitializeComponent();
            user = _user;
            GLeftMenu = this;
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            List<wcf_chat.Contacts> contacts = client.GetContacts(user.IdLogin).ToList();
            foreach (wcf_chat.Contacts contact in contacts)
            {
                listBox.Items.Add(contact.LoginUserContact);
            }
            settings.MouseLeftButtonDown += Settings_MouseLeftButtonDown;
        }
       

        private void GlobalBodyFrame_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(Messager.GlobalFrame, 0);
            if (_flag)
            {
                Messager.GlobalFrame.Content = new LeftMenu(user);
                Messager.GlobalCanvas.Visibility = Visibility.Hidden;
                _flag = false;
                Messager.GlobalCanvas.MouseLeftButtonDown -= GlobalBodyFrame_MouseLeftButtonDown;
            }
        }
        private void Settings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
                Messager.GlobalFrame.Navigate(new Menu(user.IdLogin));
                Panel.SetZIndex(Messager.GlobalFrame, 2);
                Messager.GlobalCanvas.Visibility = Visibility.Visible;
                _flag = true;
                Messager.GlobalCanvas.MouseLeftButtonDown += GlobalBodyFrame_MouseLeftButtonDown;
        }
      

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }
       
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Messager.GlobalBodyFrame.NavigationService.Navigate(new ChatFrame(user.IdLogin, listBox.SelectedItem.ToString()));
          
        }

       
    }
}
