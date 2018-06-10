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

        public Start()
        {
            InitializeComponent();
            List<int> NumberOfPlayers = new List<int>() { 1, 2, 3 };
            PlayersBox.ItemsSource = NumberOfPlayers;
            DirectoryInfo Faces = new DirectoryInfo("WpfApplication1/faces");
            foreach (var face in Faces.EnumerateFiles("*.png"))
                Images.Add(Image.FromFile(face.FullName)); // problem with uploading images (FromFile is not read)
            AvatarImage.Content = Images[NumberOfImage];
            Dictionary<string, object> Colours = new Dictionary<string, object>();
            // do like Tolya said
            ColoursBox.ItemsSource = Colours.Keys; // select key => select object (colour)
            // how add a colour to dicrtionary ?!?!?!
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
            // player.face = AvatarImage.Content
            // player.name = NicknameBox.Text
            // player.colour = take a key from ColourBox.Item => find an object by key in Dic Colours => select a value (colour)
            // give information about number of players to game from PlayersBox.Item
        }
    }
}
