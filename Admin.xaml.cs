using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace bolshit
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        string connectionString;
        public SqlConnection connection;
        public SqlDataReader dataReader;
        public SqlCommand command;
        public Admin()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["BD.connectionString"].ConnectionString;
            LoadDB();
        }

        private void LoadDB() {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("select id as Номер, name AS Название, author AS Автор, price as Цена from Books", connection);
            SqlCommand command2 = new SqlCommand("select id as Номер, login as Логин, role as Роль from Users", connection);
            connection.Open();
            DataTable dataTable = new DataTable();
            DataTable dataTable2 = new DataTable();
            dataTable.Load(command.ExecuteReader());
            dataTable2.Load(command2.ExecuteReader());
            BooksDg.DataContext = dataTable;
            UsersDg.DataContext = dataTable2;
        }

        private void BookDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksDg.SelectedItem != null) {
                ID_Book.Text = (BooksDg.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                Name_Book.Text = (BooksDg.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
                Author.Text = (BooksDg.SelectedItem as DataRowView).Row.ItemArray[2].ToString();
                Price.Text = (BooksDg.SelectedItem as DataRowView).Row.ItemArray[3].ToString();
            }
        }

        private void Add_book_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("Insert into Books (name, author, price) values ('"+Name_Book.Text+"', '"+Author.Text + "', '" +Price.Text + "'", connection);
            command.ExecuteNonQuery();
            connection.Close();
            LoadDB();
        }

        private void Edit_book_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("Update Books set name = '" + Name_Book.Text + "', author = '" + Author.Text + "', price = " + Price.Text +
                "where id = "+ID_Book.Text, connection);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            connection.Close();
            LoadDB();
        }

        private void Delete_book_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection =new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("delete from Books where id = "+ID_Book.Text, connection);
            command.ExecuteNonQuery();
            connection.Close();
            LoadDB();
        }

        private void UsersDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Add_user_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_user_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_user_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
