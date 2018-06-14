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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Build.xaml
    /// </summary>
        public class Prop
        {
            public string name { get; set; }
            public int index { get; set; }
            public Image flag { get; set; }
            public Rectangle street { get; set; }
            public Image bld { get; set; }
            public Prop()
            { }
        }
    public partial class Build : Window
    {

        
        public Build()
        {
            InitializeComponent();

        }

        private void newSelection(object sender, SelectionChangedEventArgs e)
        {
            if (bProperty.SelectedItem != null)
            {
                var s = (Prop)bProperty.SelectedItem;
                Cost.Visibility = Visibility.Visible;
                Cost.Content = "$" + Convert.ToString(((Country)MainWindow.cdata.cells[s.index].prop).cost_to_build);
                AddHouse.IsEnabled = (((Country)MainWindow.cdata.cells[s.index].prop).houses < 5);
            }
            else
            {
                Cost.Visibility = Visibility.Collapsed;
            }
        }

        private void Bld_init(object sender, EventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Add_house(object sender, RoutedEventArgs e)
        {
            var s = (Prop)bProperty.SelectedItem;
            ((Country)MainWindow.cdata.cells[s.index].prop).houses++;
            var ih = (Image)MainWindow.cdata.wnd.FindName(s.name + "3");
            ih.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/home" + Convert.ToString(((Country)MainWindow.cdata.cells[s.index].prop).houses)+".png"));
            AddHouse.IsEnabled = false;
            s.bld.Source = ih.Source;
            MainWindow.cdata.wnd.Build.IsEnabled = false;
            Close();
        }

        private void newSelection(object sender, MouseButtonEventArgs e)
        {
            if (bProperty.SelectedItem != null)
            {
                var s = (Prop)bProperty.SelectedItem;
                Cost.Visibility = Visibility.Visible;
                Cost.Content = "$" + Convert.ToString(((Country)MainWindow.cdata.cells[s.index].prop).cost_to_build);
                AddHouse.IsEnabled = (((Country)MainWindow.cdata.cells[s.index].prop).houses < 5);
            }
            else
            {
                Cost.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
