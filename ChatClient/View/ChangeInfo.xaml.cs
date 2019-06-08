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
    /// Логика взаимодействия для ChangeInfo.xaml
    /// </summary>
    public partial class ChangeInfo : Page, IServiceChatCallback
    {
        private ServiceChatClient client;
        private Users _user;
        public ChangeInfo(Users user)
        {
            InitializeComponent();
            _user = user;
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string answer = client.ChangeInformationSetting(_user.IdLogin , ChangeName.Text, ChangeSurname.Text, HashPassword.ComputeSha256Hash(Password.Password), HashPassword.ComputeSha256Hash(NewPassword.Password));
            if(answer.Equals("SHORTDATA"))
            {
                MessageBox.Show("данные не могут быть пустыми");
            }
            if(answer.Equals("BADOLDPASSWORD"))
            {
                MessageBox.Show("неправильный старый пароль");
            }
            if(answer.Equals("TRUE"))
            {
                MessageBox.Show("данные изменены");
            }
        }

      

      
        private void CloseContacts_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Messager.GlobalFrame.Content = new Menu(_user.IdLogin);
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password.Password != "" || placeholder.IsMouseOver)
            {
                placeholder.Visibility = Visibility.Hidden;
            }
            else
            {
                placeholder.Visibility = Visibility.Visible;
            }
            if (NewPassword.Password != "" || placeholder1.IsMouseOver)
            {
                placeholder1.Visibility = Visibility.Hidden;
            }
            else
            {
                placeholder1.Visibility = Visibility.Visible;
            }
        }
    }
}
