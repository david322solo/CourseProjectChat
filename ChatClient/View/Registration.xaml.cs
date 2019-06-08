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
namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window,IServiceChatCallback
    {
        public Registration()
        {
            InitializeComponent();
        }
        ServiceChatClient client;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            bool flag1 = true;
            bool flag2 = true;
            if (!HashPassword.ComputeSha256Hash(PasswordRegister.Password).Equals(HashPassword.ComputeSha256Hash(RepeatePasswordRegister.Password)))
            {
                MessageBox.Show("Пароли не совпадают");
                flag1 = false;
            }
            if(string.IsNullOrWhiteSpace(PasswordRegister.Password))
            {
                MessageBox.Show("пароль не может быть пустым");
                flag2 = false;
            }
            if(flag1 && flag2)
            {
                string answerServer = client.Registration(NameRegister.Text, SurnameRegister.Text, LoginRegister.Text, HashPassword.ComputeSha256Hash(PasswordRegister.Password),PhoneRegister.Text);
                if(answerServer.Equals("SHORTDATA"))
                {
                    MessageBox.Show("проверьте правильность данных");
                }
                if(answerServer.Equals("BADPHONE"))
                {
                    MessageBox.Show("неправильный номер телефона");
                }
                if (answerServer.Equals("TRUE1"))
                {
                    MessageBox.Show("пользователь с таким логином существует");
                }
                if(answerServer.Equals("TRUE2"))
                {
                    MessageBox.Show("пользователь с таким номером существует");
                }
                if(answerServer.Equals("CANCEL"))
                {
                    MessageBox.Show("подключение не установлено");
                }
                if(answerServer.Equals("REGISTER"))
                {
                    Messager messager = new Messager(LoginRegister.Text);
                    messager.Show();
                    Close();
                }
            }
        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(new object(), new RoutedEventArgs());
            }
        }

        private void PasswordRegister_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordRegister.Password != "" || placeholder.IsMouseOver)
            {
                placeholder.Visibility = Visibility.Hidden;
            }
            else
            {
                placeholder.Visibility = Visibility.Visible;
            }
            
            if (RepeatePasswordRegister.Password != "" || placeholder_Copy.IsMouseOver)
            {
                placeholder_Copy.Visibility = Visibility.Hidden;
            }
            else
            {
                placeholder_Copy.Visibility = Visibility.Visible;
            }
        }
    }
}
