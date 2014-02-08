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
using System.Windows.Shapes;

namespace Hackathon
{
    /// <summary>
    /// Interaction logic for NamePrompt.xaml
    /// </summary>
    public partial class NamePrompt : Window
    {
        public bool Close = false;
        public string Text = null;

        public NamePrompt()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close = true;
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(userName.Text))
            {
                Text = userName.Text;
                Close = true;
            }
            Close = true;
        }
    }
}
