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
using System.Windows.Controls;
using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        
    {

        public static CommonData cdata;
        public GameManager gm = new GameManager();
        public Start starter;
        public Auction Au;
        public Build builder;
        public Moneyback vykyp;
        public Mortgage zalog;
        public MainWindow()
         
        {
            InitializeComponent();
            cdata = new CommonData(this);
            Au = new Auction();
            Au.Closing += (s, e) =>
            {
                e.Cancel = true;
                ((Window)s).Hide();
            };

            vykyp = new Moneyback();
            vykyp.Closing += (s, e) =>
            {
                e.Cancel = true;
                ((Window)s).Hide();
            };
            zalog = new Mortgage();
            zalog.Closing += (s, e) =>
            {
                e.Cancel = true;
                ((Window)s).Hide();
            };
            builder = new Build();
            builder.Closing += (s, e) =>
            {
                e.Cancel = true;
                ((Window)s).Hide();
            };
            starter = new Start();
            starter.Closing += (s, e) =>
            {
                e.Cancel = true;
                ((Window)s).Hide();
            };
            starter.ShowDialog();
            if (cdata.players == null) Close();
            //cdata.cPlayer = cdata.players[gm.currentplayerid];
            var ttt = (Rectangle)Board.FindName("cPlayer");
            var nnn = (Ellipse)Board.FindName("pl" + Convert.ToString(gm.currentplayerid)+"1");
            ttt.Fill = nnn.Fill;
            gm.gamemngr();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Move.IsEnabled = false;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (s, ev) =>
            {
                Thread.Sleep(50);
            };
            backgroundWorker.RunWorkerCompleted += (s, ev) =>
            {
                Anigif_dices.Visibility = Visibility.Hidden;
                perform_movement();

            };
            var r = (Image)Board.FindName("r1");
            r.Visibility = Visibility.Hidden;
            r = (Image)Board.FindName("r2");
            r.Visibility = Visibility.Hidden;
            Anigif_dices.Visibility = Visibility.Visible;
            backgroundWorker.RunWorkerAsync();
 
        }

        private void Anigif_dices_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Anigif_dices.Visibility = Visibility.Hidden;
            perform_movement();
        }
        private void perform_movement()
        {
            var r1 = gm.d1.Next(1, 6);
            var r2 = gm.d2.Next(1, 6);
            var dices = r1 + r2;

            var dface = (Image)Board.FindName("r1");
            dface.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/" + Convert.ToString(r1) + ".gif"));
            dface.Visibility = Visibility.Visible;
            dface = (Image)Board.FindName("r2");
            dface.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/" + Convert.ToString(r2) + ".gif"));
            dface.Visibility = Visibility.Visible;
            cdata.cPlayer.Move(dices);
            Move.IsEnabled = true;
            //analyse where we are
            cdata.cPlayer.PayOrBuy(dices);

            if ((r1 != r2) || (!cdata.cPlayer.isalive))
            {

                //next active player will be currentplayer
                gm.currentplayerid++;
                if (gm.currentplayerid > cdata.players.Length - 1) gm.currentplayerid = 0;
                var active = cdata.players[gm.currentplayerid].isalive;
                while (!active)
                {
                    gm.currentplayerid++;
                    if (gm.currentplayerid > cdata.players.Length - 1) gm.currentplayerid = 0;
                    active = cdata.players[gm.currentplayerid].isalive;
                }
                cdata.cPlayer = cdata.players[gm.currentplayerid];
                Build.IsEnabled = (cdata.cPlayer.mono.Count != 0);
                Mortgage.IsEnabled = Contract.IsEnabled = (cdata.cPlayer.property.Count > 0);
                var ttt = (Rectangle)Board.FindName("cPlayer");
                var nnn = (Ellipse)Board.FindName("pl" + Convert.ToString(gm.currentplayerid) + "1");
                ttt.Fill = nnn.Fill;
                PInfo.Text = cdata.cPlayer.name + Convert.ToChar(13) + Convert.ToString(cdata.cPlayer.budget);
                var prt = (Image)Board.FindName("portrait");
                prt.Source = cdata.cPlayer.face.Source;
            }
            else
            {
                Build.IsEnabled = cdata.cPlayer.mono.Count>0;
            }
            if(!gm.checkplayers())
            {
                MessageBox.Show(cdata.cPlayer.name +" won!");
                starter = new Start();
                cdata.cPlayer.drop();
                starter.ShowDialog();
            }
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            builder.bProperty.SelectedItem = null;
            builder.bProperty.Items.Clear();
            builder.cPlayer.Fill = new SolidColorBrush(MainWindow.cdata.cPlayer.c);
            builder.Portrait.Source = cdata.cPlayer.face.Source;
            foreach (string m in cdata.cPlayer.mono)
            {
                var cellList = MainWindow.cdata.cells.Where(x => x.card.color == m).ToList<Cell>();
                foreach (Cell cCell in cellList)
                {
                    var i = (Image)FindName(cCell.prop.name + "2");
                    var ic = (Rectangle)FindName(cCell.prop.name + "1");
                    var ih = (Image)FindName(cCell.prop.name + "3");
                    builder.bProperty.Items.Add(new Prop { index = cdata.cells.ToList<Cell>().IndexOf(cCell), name = cCell.prop.name, flag = i, street = ic, bld = ih });
                }
            }
            builder.ShowDialog();
        }

        private void Prepare_to_quit(object sender, CancelEventArgs e)
        {
            builder.Closing -= (s, er)=>
             {
                 e.Cancel = true;
                 ((Window)s).Hide();
             };
            builder.Close();
            Au.Closing -= (s, er)=>
             {
                 e.Cancel = true;
                 ((Window)s).Hide();
             };
            Au.Close();
            starter.Closing -= (s,er)=>
             {
                 e.Cancel = true;
                 ((Window)s).Hide();
             }; 
            starter.Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void Mortgage_Click(object sender, RoutedEventArgs e)
        {
            zalog.ShowDialog();
        }
    }
}
