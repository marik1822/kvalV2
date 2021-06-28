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
using Microsoft.Win32;
using System.IO;

namespace kvalV2
{
    /// <summary>
    /// Логика взаимодействия для AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        string connectionString;
        DataTable Drivers;
        public static string id { get; set; }
        public static Guid GU { get; set; }
        public static bool add { get; set; }
        public AddDriver()
        {
            InitializeComponent();
            //Photo.IsEnabled = false;
            add = false;
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

            }
            catch (SqlException)
            {
                MessageBox.Show("Ошибка подключения БД");
            }

        }

        private void Photo_Click(object sender, RoutedEventArgs e)
        {
            if (add == true)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Please select a photo";
                ofd.Filter = "Image Files |  *.JPG; *.PNG";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == true)
                {
                    MessageBox.Show("Выбран файл " + ofd.FileName);
                }
                /* try
                 {*/
                FileInfo fi = new FileInfo(ofd.FileName);
                int size = int.Parse(fi.Length.ToString());
                if (size < 2097152)
                {
                    ImageSource III1 = new BitmapImage(new Uri(ofd.FileName));
                    byte[] ImagetoByte = File.ReadAllBytes(ofd.FileName);
                    string sql = "UPDATE Drivers SET Photo_ =(SELECT * FROM OPENROWSET(BULK N'" + ofd.FileName + "', SINGLE_BLOB) AS image) WHERE id=" + id + ";";
                    // try
                    // {
                    SqlConnection connection = null;
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);

                    int num = command.ExecuteNonQuery();
                    if (num != 0)
                    {
                        MessageBox.Show("Картинка успешно добавлена");
                        t2.Text = "";
                        t3.Text = "";
                        t4.Text = "";
                        t5.Text = "";
                        t5_Copy.Text = "";
                        t6.Text = "";
                        t7.Text = "";
                        t8.Text = "";
                        t9.Text = "";
                        t10.Text = "";
                        t11.Text = "";
                        t12.Text = "";
                        t3.Text = "";
                        t3.Text = "";
                        t7_Copy.Text = "";
                        t6_Copy.Text = "";
                        GU = Guid.NewGuid();
                        t1.Text = GU.ToString();
                        // string sql = "UPDATE Drivers SET Photo_ =(SELECT * FROM OPENROWSET(BULK N'"+ofd.FileName+"', SINGLE_BLOB) AS image) WHERE id="+id+";";

                        /* Stream streamobg = new MemoryStream(ImagetoByte);
                         BitmapImage BitObj = new BitmapImage();
                         BitObj.BeginInit();
                         BitObj.StreamSource = streamobg;
                         BitObj.EndInit();
                         this.img.Source = BitObj;*/
                    }
                }
                else MessageBox.Show("Файл не должен превышать 2МБ");
                   // }
                   // catch { MessageBox.Show("Вы не добавили картинку для пользователя."); }
               /* }
                catch
                {
                    MessageBox.Show("Вы не выбрали фотографию");
                }*/
            }
            else MessageBox.Show("Вы не добавили водителя");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            new MainMenu().Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GU = Guid.NewGuid();
            t1.Text = GU.ToString();
            t1.IsEnabled = false;
        }

        private void AddV_Click(object sender, RoutedEventArgs e)
        {
            add = false;
            int num;
           // int num2;
            Drivers = new DataTable();
            SqlConnection connection = null;
            string sql1="Select * from IdMax;";
           if ((t2.Text=="")&&(t3.Text=="")&&(t4.Text=="")&&(t5.Text=="")&&(t5_Copy.Text == "") &&(t6.Text == "") &&(t7.Text == "") &&(t8.Text == "") && (t9.Text == "") && (t10.Text == "")&& (t11.Text == "") && (t12.Text == "") && (t7_Copy.Text == "") && (t6_Copy.Text == ""))
            {
                MessageBox.Show("Вы ничего не ввели");
            }
            if ((t2.Text != "") && (t3.Text != "") && (t4.Text != "") && (t5.Text != "") && (t5_Copy.Text != "") && (t6.Text != "") && (t7.Text != "") && (t8.Text != "") && (t9.Text != "") && (t10.Text != "") && (t11.Text != "") && (t12.Text != "") && (t7_Copy.Text != "") && (t6_Copy.Text != ""))
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql1, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    int idN = int.Parse(id) + 1;
                    id = idN.ToString();
                    string sql = "INSERT INTO Drivers(id,name_,surname,middlename,[passport serial],[passport number],address,[address life],company,jobname,phone,email,description,Global_ind,idStatus) VALUES(" + idN + ",'" + t3.Text + "','" + t2.Text + "','" + t4.Text + "'," + t5.Text + "," + t5_Copy.Text + ",'" + t6_Copy.Text + " " + t6.Text + "','" + t7_Copy.Text + " " + t7.Text + "','" + t8.Text + "','" + t9.Text + "','" + t10.Text + "','" + t11.Text + "','" + t12.Text + "','" + t1.Text + "',1)";
                    SqlCommand command1 = new SqlCommand(sql, connection);
                    reader.Close();
                    num = command1.ExecuteNonQuery();
                    if (num != 0)
                    {
                        MessageBox.Show("Водитель успешно добавлен, теперь Вы можете загрузить фотографию");
                        add = true;
                        /* t2.Text = "";
                         t3.Text = "";
                         t4.Text = "";
                         t5.Text = "";
                         t5_Copy.Text = "";
                         t6.Text = "";
                         t7.Text = "";
                         t8.Text = "";
                         t9.Text = "";
                         t10.Text = "";
                         t11.Text = "";
                         t12.Text = "";
                         t3.Text = "";
                         t7_Copy.Text = "";
                         t6_Copy.Text = "";
                         GU = Guid.NewGuid();
                         t1.Text = GU.ToString();*/

                    }
                    else
                        MessageBox.Show("Ошибка добавления водителя!!");
                    return;
                }
                reader.Close();

                //string sql="INSERT INTO Drivers";
                /*  num2 = command.ExecuteNonQuery();
                  if (num2 != 0)
                  {
                      MessageBox.Show("Водитель успешно добавлен");
                      t2.Text = "";
                      t3.Text = "";
                      t4.Text = "";
                      t5.Text = "";
                      t6.Text = "";
                      t7.Text = "";
                      t8.Text = "";
                      t9.Text = "";
                      t10.Text = "";
                      t11.Text = "";
                      t12.Text = "";
                      t3.Text = "";
                      t3.Text = "";
                      t7_Copy.Text = "";
                      t6_Copy.Text = "";

                  }*/
                //  else
                MessageBox.Show("Ошибка добавления водителя!!");
            }
        }

        private void t5_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void t5_Copy_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
