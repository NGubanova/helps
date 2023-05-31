using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace bolshit
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        string connectionString;
        public SqlDataReader dataReader;
        public SqlConnection connection;
        public SqlCommand command;
        string id;
        string role;

        public Authorization()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["BD.connectionString"].ConnectionString;
        }

        private void auth_Click(object sender, RoutedEventArgs e)
        {
            String hashcode;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashcode = Encoding.UTF8.GetString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password.Password)));
            }
            connection = new SqlConnection(connectionString);
            connection.Open();
            try { 
            command = new SqlCommand("select * from Users where login='" + login.Text + "' and password='" + hashcode + "'", connection);
            dataReader = command.ExecuteReader();
            dataReader.Read();
                if ((string)dataReader["login"] != "") {
                    id = dataReader["id"].ToString();
                    role = dataReader["role"].ToString();
                    this.Hide();
                    switch (role) {
                        case "user":
                    MainWindow mainWindow = new MainWindow(Convert.ToInt32(id), role);
                    mainWindow.Show();
                            break;
                        case "admin":
                            Admin admin = new Admin();
                            admin.Show();
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка авторизации", "Error");
            }
            connection.Close();
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.Show();
        }
    }
}
