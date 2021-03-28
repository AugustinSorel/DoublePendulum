using System.Windows.Controls;

namespace DoublePendulum
{
    /// <summary>
    /// Interaction logic for DoublePendulumView.xaml
    /// </summary>
    public partial class DoublePendulumView : UserControl
    {
        private DoublePendulumViewModel doublePendulumViewModel;
        public DoublePendulumView()
        {
            InitializeComponent();
            doublePendulumViewModel = new DoublePendulumViewModel();
            DataContext = doublePendulumViewModel;
        }

        private void ButtonPause_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            doublePendulumViewModel.Pause();
        }

        private void ButtonStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            doublePendulumViewModel.Stop();
        }

        private void ButtonStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            doublePendulumViewModel.Start();
        }

        private void buttonClean_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            doublePendulumViewModel.CleanData();
        }

        private void ButtonFullScreen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            doublePendulumViewModel.FullScreen();
        }
    }
}