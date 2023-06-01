using Microsoft.EntityFrameworkCore;
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
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Bag.xaml
    /// </summary>
    public partial class Bag : Window
    {
        int ID;
        LoginContext db;

        public Bag(int id)
        {
            InitializeComponent();
            db = new LoginContext();
            ID = id;
            BagListOrder(ID);
        }

        public void BagListOrder(int id_user) {
            id_user = 1;
            User user = db.Users.Find(id_user);
            var books = db.Books.Where(p => EF.Functions.Like(p.Users.FirstOrDefault().Id.ToString(), id_user.ToString())).ToList();
            BagList.ItemsSource = books;
        }

        private void delBnt_Click(object sender, RoutedEventArgs e)
        {
            ID = 1;
            Button button = (Button)sender;
            var idBook = button.Tag;
            User user = db.Users.Find(ID);
            Book book = db.Books.Find(idBook);
            var userDelBook = db.Users.Include(a => a.Books).FirstOrDefault(a => a.Id == ID);
            userDelBook.Books.Remove(book);
            db.SaveChanges();
            BagListOrder(ID);

            // db.Users.Remove(user); обычное удаление
        }
    }
}
