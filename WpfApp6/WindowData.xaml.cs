using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp6.Database;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для WindowData.xaml
    /// </summary>
    public partial class WindowData : Window
    {
        public WindowData()
        {
            InitializeComponent();
            DGridAsync();
        }


        private async Task DGridAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                dgrid.ItemsSource = db.Users.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        private async Task Refresh()
        {

            await DGridAsync();
        }
    }
}
