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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace kvalV2
{
    /// <summary>
    /// Логика взаимодействия для ChangingPassword.xaml
    /// </summary>
    public partial class ChangingPassword : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable inspector;
        public string pas_;
        public string NewPas_;
        public string NewPas2_;
        public ChangingPassword()
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

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            string sql1 ;
            inspector = new DataTable();
            
            pas_ = Password.Text.Trim();
            NewPas_ =NewPassword.Text.Trim();
            NewPas2_ = NewPassword_Copy.Text.Trim();
            Error.Text = "";
            SqlConnection connection = null;
            if ((Password.Text == "") && (NewPassword.Text == "") && (NewPassword_Copy.Text == ""))
            {
                Error.Text = "Вы ничего не ввели";
            }
            else
                if (Password.Text == "")
            {
                Error.Text = "Вы не ввели старый пароль";
            }
            else
            if (NewPassword.Text == "")
            {
                Error.Text = "Вы не ввели новый пароль";
            }
            else
            if (NewPassword_Copy.Text == "")
            {
                Error.Text = "Вы не ввели новый пароль повторно";
            }
            else
                if (NewPas_ != NewPas2_)
            {
                Error.Text = "Неверно введёно поле повторного нового пароля";
            } else
                if ((NewPas_ == NewPas2_) && (NewPassword_Copy.Text != "")&& (NewPassword.Text != "")&& (Password.Text != "")) {
                sql1 = "UPDATE inspector SET Password='" + NewPas_ + "' where Password='" + pas_ + "';";
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sql1, connection);
                int number = command.ExecuteNonQuery();
                if (number !=0) 
                {
                    new MainWindow().Show();
                    this.Close();
                }
                //SqlDataReader reader = command.ExecuteReader();
                /* while (reader.Read())
                 {
                     new MainWindow().Show();
                     this.Close();
                     return;
                 }*/
                // reader.Close();
                //Error.Text = "Не удалось найти пользователя с таким паролем";
                Error.Text = "Обновлено объектов " + number ;
                Password.Text = "";
                NewPassword.Text = "";
                NewPassword_Copy.Text = "";
                connection.Close();
            }

        }
    }
}
