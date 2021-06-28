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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Jitbit.Utils;


namespace kvalV2
{
    /// <summary>
    /// Логика взаимодействия для ListDrivers.xaml
    /// </summary>
    public partial class ListDrivers : Window
    {
        CsvExport myExport = new CsvExport();
        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable Drivers;
        public ListDrivers()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //подключено БД
            }
            catch (SqlException)
            {
                MessageBox.Show("Ошибка подключения БД");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * from Drivers where idStatus=1;";
            SqlConnection connection = null;

            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter(sql, connection);
            connection.Open();
            SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(adapter as SqlDataAdapter);
            adapter.UpdateCommand = myCommandBuilder.GetUpdateCommand();
            Drivers = new DataTable();
            adapter.Fill(Drivers); //загрузка данных
            AllDr.ItemsSource = Drivers.DefaultView; //привязка к DataGrid
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            new MainMenu().Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AllDr.SelectedItem != null)
            {
                string id= Drivers.Rows[AllDr.SelectedIndex]["id"].ToString().Trim();
                string sql2 = "Update Drivers SET idStatus= 2 where id=" + id + ";";
                SqlConnection connection = null;

                connection = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand(sql2, connection);
                connection.Open();
                int num = command.ExecuteNonQuery();
                if (num != 0)
                {
                    MessageBox.Show("Водитель успешно удалён успешно удалена");
                }
                else MessageBox.Show("Ошибка удаления");
            }
            else MessageBox.Show("Вы не выбрали строку для удаления");
        }
        void Save_Table(string id,string name, string surname, string middlename,/* string serial, string number, */string postcode, string address,/* string life,*/ string  company, string jobname, string phone, string email,string description, string Global_ind)
        {
            myExport.AddRow();
            myExport["Код Водителя"]= id;
            myExport["Имя"] = name;
            myExport["Фамилия"] = surname;
            myExport["Отчество"] = middlename;
           // myExport["Серия"] = serial;
           // myExport["Номер"] = number;
            myExport["Код"] = postcode;
            myExport["Адрес регистрации"] = address;
           // myExport["Адрес проживания"] = life;
            myExport["Компания"] = company;
            myExport["Должность"] = jobname;
            myExport["Номер телефона"] = phone;
            myExport["email"] = email;
            myExport["Заметки"] = description;
            myExport["GUIT"] = Global_ind;
           // MessageBox.Show("Данные успешно экспортированы");


        }
        private void CSV_Click(object sender, RoutedEventArgs e)
        {
          //  string filePath= @"C:\Users\orvis\Desktop\Архивы\Arhiv.csv";
            for(int i=0;i< Drivers.Select().Length; i++)
            {
                Save_Table(Drivers.Rows[i]["id"].ToString().Trim(), 
                    Drivers.Rows[i]["name_"].ToString().Trim(), 
                    Drivers.Rows[i]["surname"].ToString().Trim(), 
                    Drivers.Rows[i]["middlename"].ToString().Trim(), 
                    //Drivers.Rows[i]["[passport serial]"].ToString().Trim(), 
                    //Drivers.Rows[i]["[passport number]"].ToString().Trim(),
                    Drivers.Rows[i]["postcode"].ToString().Trim(), 
                    Drivers.Rows[i]["address"].ToString().Trim(),
                    //Drivers.Rows[i]["[address life]"].ToString().Trim(),
                    Drivers.Rows[i]["company"].ToString().Trim(),
                    Drivers.Rows[i]["jobname"].ToString().Trim(),
                    Drivers.Rows[i]["phone"].ToString().Trim(),
                    Drivers.Rows[i]["email"].ToString().Trim(),
                    Drivers.Rows[i]["description"].ToString().Trim(),
                    Drivers.Rows[i]["Global_ind"].ToString().Trim());
                
            }
            myExport.ExportToFile(@"C:\Users\orvis\Desktop\Архивы\Arhiv.csv");//сохранить в таблицу
            MessageBox.Show("Данные успешно экспортированы");
        }
    }
}
