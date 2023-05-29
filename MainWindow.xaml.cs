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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bolshit
{
    public partial class MainWindow : Window
    {
        string connectionString;
        public SqlDataReader dataReader;
        public SqlConnection connection;
        public SqlCommand command;
        public SqlDataAdapter adapter;
        DataSet dataSet;

        public MainWindow(int id, string role)
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["BD.connectionString"].ConnectionString;
            List();
        }

        private void NoABC(object sender, TextCompositionEventArgs e) {
            if (!Char.IsDigit(e.Text, 0)) { 
                e.Handled= true;
            }
        }

        public void List() {
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("select * from Books", connection);
            adapter = new SqlDataAdapter(command);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "Books");
            Books books = new Books();
            IList<Books> books1 = new List<Books>();

            foreach (DataRow dr in dataSet.Tables[0].Rows) {
                books1.Add(new Books
                {
                    id = Convert.ToInt32(dr[0].ToString()),
                    name = dr[1].ToString(),
                    author = dr[2].ToString(),
                });
                listView.ItemsSource = books1;
            }
        }

        public class Books {
            public int id { get; set; }
            public string name { get; set; }
            public string author { get; set; }
        }
    }
}
