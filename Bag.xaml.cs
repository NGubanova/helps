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
    public partial class Bag : Window
    {
        string connectionString;
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter adapter;
        DataSet data;

        public Bag(int id)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["BD.connectionString"].ConnectionString;
            List(id);
        }

        public void List(int id) {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("select * from Books INNER JOIN UserBooks ON Books.id = UserBooks.bookId where UserBooks.userId = "+id, connection);
            adapter = new SqlDataAdapter(command);
            data = new DataSet();
            adapter.Fill(data, "Books");
            Book book = new Book();
            IList<Book> books = new List<Book>();
            foreach (DataRow dr in data.Tables[0].Rows) {
                books.Add(new Book
                {
                    id = Convert.ToInt32(dr[0].ToString()),
                    name = dr[0].ToString(),
                    author= dr[1].ToString(),
                    price = dr[2].ToString(),
                });
                BagList.ItemsSource = books;
            }
        }

        private void delBnt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
