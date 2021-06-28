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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace kvalV2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable inspector;
        string login_;
        string password_;
        public int LoginAttempts=0;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //Error.Text = "подключено БД";
            }
            catch (SqlException)
            {
                Error.Text = "Ошибка подключения БД!!!";
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            
           // LoginAttempts++;
            if (LoginAttempts >= 5) {
                new ChangingPassword().Show();
                this.Close();
            }
            string sql;
            inspector = new DataTable();
            login_ = Login.Text.Trim();
            password_ = Password.Text.Trim();
            Error.Text = "";
            SqlConnection connection = null;
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
            } else
                if ((Login.Text != "")&& (Password.Text != "")) {
                sql = "SELECT * from inspector where ((Login='" + login_ + "')and(Password='" + password_ + "'));";
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    new MainMenu().Show();
                    this.Close();
                    return;
                }
                reader.Close();
                LoginAttempts++;
                Error.Text = "Неверный логин или пароль";
                Login.Text = "";
                Password.Text = "";
                connection.Close();
            }
        }
    }
}
