using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hackathon
{
    /// <summary>
    /// Interaction logic for AuthenticationBar.xaml
    /// </summary>
    public partial class AuthenticationBar : UserControl
    {
        public AuthenticationBar()
        {
            InitializeComponent();
        }

        public void SetAuthentication(bool auth, string name)
        {
            if(auth)
            {
                background.Background = new SolidColorBrush(Colors.Green);
                authenticationText.Text = name + " Authenticated";
            }
            else
            {
                background.Background = new SolidColorBrush(Colors.Red);
                authenticationText.Text = "User Not Authenticated";
            }
        }
    }
}
