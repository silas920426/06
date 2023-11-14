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

namespace _1114
{
    /// <summary>
    /// _04.xaml 的互動邏輯
    /// </summary>
    public partial class _04 : Window
    {
        public _04()
        {
            InitializeComponent();
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _04 _04 = new _04();
            _04.Show();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("open");
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("save");
        }
    }
}
