using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        LoginContext db;
        public SqlDataAdapter dataAdapter;
        int ID;

        public Catalog()
        {
            InitializeComponent();
            db= new LoginContext();
            List();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {

        }


        public void List()
        {
            var books = db.Books.ToList();
            listView.ItemsSource = books;
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            ID = 1;
            Button button = (Button)sender;
            var idBook = button.Tag;
            User user = db.Users.Find(ID);
            Book book = db.Books.Find(idBook);
            book.Users.Add(user);
            db.SaveChanges();
            MessageBox.Show("товар добавлен в корзину", "В корзине");
        }

        private void Bag_Click(object sender, RoutedEventArgs e)
        {
            Bag bag = new Bag(ID);
            bag.Owner = Application.Current.MainWindow;
            bag.ShowDialog();
        }
    }
}
