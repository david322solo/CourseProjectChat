using ChatClient.ServiceChat;
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

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window, IServiceChatCallback
    {
        public Authorization()
        {
            InitializeComponent();
        }
        ServiceChatClient client;
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            string answer = client.Authorization(Login.Text, HashPassword.ComputeSha256Hash(Password.Password));
            if(answer.Equals("CANCEL"))
            {
                MessageBox.Show("отсутствует подключение");
            }
            if (answer.Equals("INCORRECT"))
            {
                MessageBox.Show("неправильный логин или пароль");
            }
            if(answer.Equals("AUTHORIZATION"))
            {
                Messager messager = new Messager(Login.Text);
                messager.Show();
                Close();
            }
        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }

       

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Button_Click(new object(), new RoutedEventArgs());
            }
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
        }
    }
}
