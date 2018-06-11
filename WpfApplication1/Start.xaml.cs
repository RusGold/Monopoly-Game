using System;
using System.Collections.Generic;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Start : Window
    {
        int NumberOfImage = 0;
        List<Image> Images = new List<Image>();
        
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
            if (s == "White")
                return (Color)ColorConverter.ConvertFromString("#ffffff");
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

            List<int> NumberOfPlayers = new List<int>() { 1, 2, 3 };
            PlayersBox.ItemsSource = NumberOfPlayers;

            DirectoryInfo Faces = new DirectoryInfo("WpfApplication1/faces");
            foreach (var face in Faces.EnumerateFiles("*.png"))
            {
                Image i = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/" + face.FullName))
                };
                Images.Add(i);
            }
            AvatarImage.Content = Images[NumberOfImage];

            List<string> NameOfColours = new List<string>() {"Yellow", "Green", "Red", "Black", "Blue", "Purple", "Orange", "White", "Pink", "Coral", "Dark blue", "Dark green" };
            ColoursBox.ItemsSource = NameOfColours;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfImage != 0)
                AvatarImage.Content = Images[NumberOfImage - 1];
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfImage != Images.LastIndexOf(Images.Last()))
                AvatarImage.Content = Images[NumberOfImage + 1];
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Player User = new Player(NicknameBox.Text, SetTheColor(ColoursBox.SelectedItem.ToString())) { face = (Image)AvatarImage.Content};
            // give information about number of players to game from PlayersBox.Item (where?)
        }
    }
}
