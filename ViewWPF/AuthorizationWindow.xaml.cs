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
using System.IO;
using System.Windows.Navigation;

namespace ViewWPF
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetToken.LoadCompleted += GetToken_DocumentCompleted;
            GetToken.Navigate("https://oauth.vk.com/authorize?client_id=" + 
                VKAPI.__APPID + "&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.52");
        }

        private void GetToken_DocumentCompleted(object sender, NavigationEventArgs e)
        {
            if (GetToken.Source.ToString().IndexOf("access_token=") != -1)
            {
                GetUserToken();
            }
        }

        private void GetUserToken()
        {
            char[] symbols = { '=', '&' };
            string[] URL = GetToken.Source.ToString().Split(symbols);
            File.WriteAllText("UserInf.txt", URL[1] + "\n");
            File.AppendAllText("UserInf.txt", URL[5]);
            this.Visibility = Visibility.Hidden;
        }
    }
}
