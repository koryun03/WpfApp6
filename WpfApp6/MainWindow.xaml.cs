using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Database;
using WpfApp6.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
     
        private void ButtonInsert_Click(object sender, RoutedEventArgs e)
        {
            InsertAsync();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateAsync();
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.Show();
        }
        private void ButtonGetAll_Click(object sender, RoutedEventArgs e)
        {
            WindowData wd = new WindowData();
            wd.Show();
        }
      
        private bool RegexChecker()
        {
            bool checknameandsurname = false;
            string pattern = @"^[A-Za-z]+$";

            if (Name_box.Text != "Name" && Surname_box.Text != "Surname" && Regex.IsMatch(Name_box.Text, pattern) && Regex.IsMatch(Surname_box.Text, pattern))
            {
                checknameandsurname = true;
            }

            bool checkage = false;
            string pattern1 = @"^\d+$";

            if (Regex.IsMatch(Age_box.Text, pattern1))
            {
                checkage = true;
            }

            bool checkphone = false;
            string pattern2 = @"^[+\d]+$";

            if (Regex.IsMatch(Phone_box.Text, pattern2))
            {
                checkphone = true;
            }

            if (checknameandsurname && checkage && checkphone)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task InsertAsync()
        {
            bool a = RegexChecker();
            if (a)
            {
                User u = new User(Name_box.Text, Surname_box.Text, int.Parse(Age_box.Text), long.Parse(Phone_box.Text));
                using (ApplicationContext db = new ApplicationContext())
                {
                    bool t = false;
                    t = db.Users.Any(p => p.Phone == u.Phone);

                    if (t)
                    {
                        MessageBox.Show("This phone number is  already in use");
                    }
                    else
                    {
                        db.Users.AddAsync(u);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Inserted");
                    }
                }
            }
            else
            {
                MessageBox.Show("Try again");
            }
        }
        public async Task UpdateAsync()
        {
            bool a = RegexChecker();
            if (a)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User u = db.Users.FirstOrDefault(p => p.Phone == long.Parse(Phone_box.Text));
                    if (u==null)
                    {
                        MessageBox.Show("Phone number not found");

                    }
                    u.Name = Name_box.Text;
                    u.Surname = Surname_box.Text;
                    u.Age = int.Parse(Age_box.Text);
                    u.Phone = long.Parse(Phone_box.Text);
                    db.Users.Update(u);
                    await db.SaveChangesAsync();
                    MessageBox.Show("Updated");
                }
            }
            else
            {
                MessageBox.Show("Try again");
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Name" || textBox.Text == "Surname" || textBox.Text == "Age" || textBox.Text == "Phone")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == Name_box)
                {
                    textBox.Text = "Name";
                }
                else if (textBox == Surname_box)
                {
                    textBox.Text = "Surname";
                }
                else if (textBox == Age_box)
                {
                    textBox.Text = "Age";
                }
                else if (textBox == Phone_box)
                {
                    textBox.Text = "Phone";
                }
            }
        }


    }
}
