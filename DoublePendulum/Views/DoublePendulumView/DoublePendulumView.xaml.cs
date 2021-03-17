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
            DoublePendulumViewModel doublePendulumViewModel = new DoublePendulumViewModel(this);
            DataContext = doublePendulumViewModel;
        }
    }
}