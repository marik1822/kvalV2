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

namespace kvalV2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            Error.Text = "";
            if ((Login.Text == "") && (Password.Text == ""))
            {
                Error.Text = "Вы ничего не ввели ";
            }
            else
            if (Login.Text == "") 
            {
                Error.Text = "Вы не ввели логин";
            } 
            else
                if (Password.Text == "")
            {
                Error.Text = "Вы не ввели пароль";
            }
        }
    }
}
