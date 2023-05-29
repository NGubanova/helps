using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bolshit
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        string connectionString;
        public SqlDataReader dataReader;
        public SqlConnection connection;
        public SqlCommand command;

        public Registration()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["BD.connectionString"].ConnectionString;
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("select * from Users where login='"+login.Text+"'", connection);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            try
            {
                if (login.Text == (string)dataReader["login"]) {
                    MessageBox.Show("Этот логин уже есть в системе", "Error");
                }
            }
            catch {
                connection.Close();
                String hashcode;
                using (SHA256 sha256 = SHA256.Create()) {
                    hashcode = Encoding.UTF8.GetString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password.Password)));
                }
                    connection.Open();
                if (login.Text == "" || password.Password == "")
                {
                    MessageBox.Show("Заполните поле логин и пароль", "Error");
                } else if (Regex.IsMatch(password.Password, "[A-Za-z0-9]")){
                    command = new SqlCommand("Insert into Users (login, password, role) values ('"+login.Text+"', +'"+ hashcode+"', 'user')", connection);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    command = new SqlCommand("Select * from Users where login='" + login.Text + "' and password='" + hashcode + "'", connection);
                    dataReader = command.ExecuteReader();
                    dataReader.Read();
                    int id = Convert.ToInt32(dataReader["id"]);
                    String role = (dataReader["role"]).ToString();
                    connection.Close();
                    this.Hide();
                    MainWindow mainWindow = new MainWindow(Convert.ToInt32(id), role);
                    mainWindow.Show();
                }
            }
        }

        private void auth_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            this.Hide();
            authorization.Show();
        }
    }
}
