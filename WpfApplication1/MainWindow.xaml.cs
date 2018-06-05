using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.Data;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        
    {
/*        public static Card[] cards = new Card[60];
        public static MAP MainMap=new MAP();
        public static List<Player> players=new List<Player>();
        private int ActivePlayers = 4;*/
        public static CommonData cdata;
        public GameManager gm = new GameManager();
        public MainWindow()
         
        {
            InitializeComponent();
            cdata = new CommonData(this);
            gm.gamemngr();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Anigif_dices.Visibility = Visibility.Visible;
            var r1 = gm.d1.Next(1, 6);
            var r2 = gm.d2.Next(1, 6);
            var dices = r1 + r2;
            cdata.cPlayer.Move(dices);
            gm.currentplayerid++;
            if (gm.currentplayerid > 3) gm.currentplayerid = 0;
        }

        private void Anigif_dices_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Anigif_dices.Visibility = Visibility.Hidden;
        }
    }
}
