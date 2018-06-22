using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
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
using WpfApplication1.Data;

namespace WpfApplication1
{
    /// <summary> 
    /// Interaction logic for Window1.xaml 
    /// </summary> 
    public partial class Start : Window
    {
        int NumberOfImage = 0;
        //private static CommonData cdata = MainWindow.cdata;
        List<Image> Images = new List<Image>();
        public ObservableCollection<lPlayer> pAll { get; set; }
        List<string> NameOfColours = new List<string>() { "Yellow", "Green", "Red", "Black", "Blue", "Purple", "Orange", "Gray", "Pink", "Coral", "Dark blue", "Dark green" };

        //

        public class lPlayer
        {
            public int num { get; set; }
            public string name { get; set; }
            public string color { get; set; }
            public Image face { get; set; }
        }

        private Color SetTheColor(string s)
        {
            if (s == "Yellow")
                return (Color)ColorConverter.ConvertFromString("#ffff00");
            if (s == "Green")
                return (Color)ColorConverter.ConvertFromString("#008000");
            if (s == "Red")
                return (Color)ColorConverter.ConvertFromString("#ff0000");
            if (s == "Black")
                return (Color)ColorConverter.ConvertFromString("#000000");
            if (s == "Blue")
                return (Color)ColorConverter.ConvertFromString("#42aaff");
            if (s == "Purple")
                return (Color)ColorConverter.ConvertFromString("#800080");
            if (s == "Orange")
                return (Color)ColorConverter.ConvertFromString("#ffa500");
            if (s == "Gray")
                return (Color)ColorConverter.ConvertFromString("#808080");
            if (s == "Pink")
                return (Color)ColorConverter.ConvertFromString("#ffc0cb");
            if (s == "Coral")
                return (Color)ColorConverter.ConvertFromString("#ff7f50");
            if (s == "Dark blue")
                return (Color)ColorConverter.ConvertFromString("#002137");
            if (s == "Dark green")
                return (Color)ColorConverter.ConvertFromString("#013220");
            return (Color)ColorConverter.ConvertFromString("#ffffff");
        }

        public Start()
        {
            InitializeComponent();
            pAll = new ObservableCollection<lPlayer>();
            AllPlayers.ItemsSource = pAll;
            DirectoryInfo faces = new DirectoryInfo("../../faces");
            foreach (var face in faces.EnumerateFiles("*.png"))
            {
                Image i = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/" + face.Name))
                };
                Images.Add(i);
            }
            AvatarImage.Content = Images[NumberOfImage];


            ColoursBox.ItemsSource = NameOfColours;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfImage > 0)
            {
                AvatarImage.Content = Images[NumberOfImage - 1];
                NumberOfImage--;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfImage < Images.LastIndexOf(Images.Last()))
            {
                AvatarImage.Content = Images[NumberOfImage + 1];
                NumberOfImage++;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (pAll.Count < 2)
            {
                MessageBox.Show("Нужно играть хотя бы вдвоём!");
            }
            else
            {
                //replace by Starter window logic
                MainWindow.cdata.players = new Player[pAll.Count];
                var i = 0;
                foreach (lPlayer l in pAll)
                {
                    Player tPl = new Player(i,"pl" + Convert.ToString(i) + "1", SetTheColor(l.color), l.face);
                    MainWindow.cdata.players[i] = tPl;
                    MainWindow.cdata.players[i].name = l.name+" (Player " + Convert.ToString(i + 1)+")";
                    var b = (Ellipse)MainWindow.cdata.wnd.Board.FindName(MainWindow.cdata.players[i].code+"1");
                    b.Fill = new SolidColorBrush(tPl.c);
                    b.Visibility = Visibility.Visible;
                    b = (Ellipse)MainWindow.cdata.wnd.Board.FindName(MainWindow.cdata.players[i].code + "0");
                    b.Visibility = Visibility.Visible;
                    i++;
                }
                ((Image)MainWindow.cdata.wnd.FindName("portrait")).Source = MainWindow.cdata.players[0].face.Source;
                MainWindow.cdata.cPlayer = MainWindow.cdata.players[0];
                MainWindow.cdata.wnd.PInfo.Text = MainWindow.cdata.cPlayer.name + Convert.ToChar(13) + Convert.ToString(MainWindow.cdata.cPlayer.budget);
                //////////////////////////////////////////////////////////////////////////////////
                Close();
            }

        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            var selectionisvalid = (ColoursBox.SelectedItem != null)&&(NicknameBox.Text!="");
            if (selectionisvalid)
            {
                pAll.Add(new lPlayer { num = pAll.Count + 1, name = NicknameBox.Text, color = ColoursBox.Text, face = Images[NumberOfImage] });
                var i = NameOfColours.FindIndex(x => x == ColoursBox.Text);
                NameOfColours.RemoveAt(i);
                ColoursBox.Items.Refresh();
                ColoursBox.SelectedItem = null;
                if (pAll.Count > 1) bStart.ToolTip = null;
                if (pAll.Count > 3) Add.IsEnabled = false;
                NicknameBox.Text = "";
            }
            else
            {
                MessageBox.Show((ColoursBox.SelectedItem == null?"Нельзя играть бесцветной фишкой :)":"Безымянные игроки в игру не допускаются :)"));
            }
        }
    }
}