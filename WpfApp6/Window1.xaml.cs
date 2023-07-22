using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.Database;
using WpfApp6.Models;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Phone")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox == Phone_box)
                {
                    textBox.Text = "Phone";
                }

            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteAsync();
        }
        private async Task DeleteAsync()
        {
            bool checkphone = false;
            string pattern2 = @"^[+\d]+$";

            if (Regex.IsMatch(Phone_box.Text, pattern2))
            {
                checkphone = true;
            }


            if (checkphone)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    User u = db.Users.FirstOrDefault(p => p.Phone == long.Parse(Phone_box.Text));
                    if (u == null)
                    {
                        MessageBox.Show("Phone number not found");
                    }
                    db.Users.Remove(u);
                    await db.SaveChangesAsync();
                    MessageBox.Show("Deleted");
                }
            }
            else
            {
                MessageBox.Show("Invalid phone number");

            }
        }
        private void Phone_box_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
