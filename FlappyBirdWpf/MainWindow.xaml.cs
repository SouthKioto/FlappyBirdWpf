using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlappyBirdWpf
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

        private void StartGame(object sender, RoutedEventArgs e)
        {
            GameWindow game = new GameWindow();
            game.Show();
            this.Close();
        }


        private void DisabledAlert(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("opcja nie dostępna", "Info", MessageBoxButton.OK);
        }
    }
}