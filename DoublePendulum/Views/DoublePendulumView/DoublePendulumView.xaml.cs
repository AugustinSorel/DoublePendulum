using System.Windows.Controls;

namespace DoublePendulum
{
    /// <summary>
    /// Interaction logic for DoublePendulumView.xaml
    /// </summary>
    public partial class DoublePendulumView : UserControl
    {
        public DoublePendulumView()
        {
            InitializeComponent();
            DataContext = new DoublePendulumViewModel();
        }

        private void ButtonPause_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ButtonStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ButtonStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}