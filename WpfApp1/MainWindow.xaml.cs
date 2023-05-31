using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        public LoginContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new LoginContext();
        }


        private void reg_Click(object sender, RoutedEventArgs e)
        {
            User user = new User { Login = login.Text, Password = password.Password, Role = "admin" };
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
