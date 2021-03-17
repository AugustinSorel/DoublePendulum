using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

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
