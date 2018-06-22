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
    /// Interaction logic for Auction.xaml
    /// </summary>
    public partial class Auction : Window
    {
        public List<Player> Comp = new List<Player>();
        public Int32 currentBet = 10;
        public Player fp;
        public Auction()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Int32 bid = 0;
            try
            {
                bid = Convert.ToInt32(NextBid.Text);
            }
            catch
            {
                MessageBox.Show("Ставка должна быть целым числом!");
            }
            if (bid < 0)
            {
                MessageBox.Show("Ставку можно только повысить!");
            }
            else
            {
                NextBid.Text = "";
                currentBet += bid;
                Log.Items.Add(fp.name + " повышает цену до " + Convert.ToString(currentBet));
                foreach (Player c in Comp)
                {
                    if (c.budget < currentBet)
                    {
                        Log.Items.Add(c.name+ " заканчивает аукцион: мало средств");
                        var d = Comp.Count();
                        var s = "comp" + Convert.ToString(d);
                        var b = (Rectangle)FindName(s);
                        b.Visibility = Visibility.Hidden;
                        c.isalive = false;
                    }          
                }
                Comp.RemoveAll(x => x.isalive == false);
                foreach (Player c in Comp)
                {
                    var s = "comp" + Convert.ToString(Comp.IndexOf(c)+1);
                    var b = (Rectangle)FindName(s);
                    b.Fill = new SolidColorBrush(c.c);
                }
                if (Comp.Count<2)
                {
                    var yyy = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position];
                    var gPlayer = MainWindow.cdata.players[fp.playerid];
                    yyy.prop.ownerplayer = gPlayer;
                    gPlayer.budget -= currentBet;
                    gPlayer.property.Add(yyy.prop);
                    var str = "p" + Convert.ToString(yyy.y) + Convert.ToString(yyy.x) + "4";
                    Rectangle r = (Rectangle)MainWindow.cdata.wnd.Board.FindName(str);
                    r.Fill = new SolidColorBrush(gPlayer.c);
                    if (gPlayer.HasMono(yyy.card.color))
                    {
                        gPlayer.mono.Add(yyy.card.color);
                    };
                    MainWindow.cdata.wnd.MainLog.Items.Insert(0, gPlayer.name + " купил " + yyy.prop.name + " за " + currentBet + ". Оставшийся бюджет " + Convert.ToString(gPlayer.budget) + ".");
                    Close();
                }
                var ind = Comp.IndexOf(fp);
                if (ind+1 == Comp.Count)
                {
                    fp = Comp.First();
                }
                else { fp = Comp[Comp.IndexOf(fp) + 1]; }

                cComp.Fill = new SolidColorBrush(fp.c);
                var prt = (Image)FindName("portrait");
                prt.Source = fp.face.Source;
                PInfo.Content = fp.name + ", " + Convert.ToString(fp.budget);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            var c = Comp.Find(x=>x.playerid==fp.playerid);
            //MainWindow.cdata.wnd.Main
            Log.Items.Add(c.name + " выходит из аукциона по доброй воле");
            var s = "comp" + Convert.ToString(Comp.Count());//IndexOf(c) + 1);
            var b = (Rectangle)FindName(s);
            b.Visibility = Visibility.Hidden;
            c.isalive = false;
            Comp.RemoveAll(x => x.isalive == false);
            foreach (Player comp in Comp)
            {
                s = "comp" + Convert.ToString(Comp.IndexOf(comp)+1);
                b = (Rectangle)FindName(s);
                b.Fill = new SolidColorBrush(comp.c);
            }
            var ind = Comp.IndexOf(fp);
            if (ind + 1 == Comp.Count)
            {
                fp = Comp.First();
            }
            else { fp = Comp[Comp.IndexOf(fp) + 1]; }
            cComp.Fill = new SolidColorBrush(fp.c);
            var prt = (Image)FindName("portrait");
            prt.Source = fp.face.Source;
            PInfo.Content = fp.name + ", " + Convert.ToString(fp.budget);

            if (Comp.Count < 2)
            {
                var yyy = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position];
                var gPlayer = MainWindow.cdata.players[fp.playerid];
                yyy.prop.ownerplayer = gPlayer;
                gPlayer.budget -= currentBet;
                gPlayer.property.Add(yyy.prop);
                var str = "p" + Convert.ToString(yyy.y) + Convert.ToString(yyy.x) + "4";
                Rectangle r = (Rectangle)MainWindow.cdata.wnd.Board.FindName(str);
                r.Fill = new SolidColorBrush(gPlayer.c);
                if (gPlayer.HasMono(yyy.card.color))
                {
                    gPlayer.mono.Add(yyy.card.color);
                };
                MainWindow.cdata.wnd.MainLog.Items.Insert(0, gPlayer.name + " купил " + yyy.prop.name + " за " + currentBet + ". Оставшийся бюджет " + Convert.ToString(gPlayer.budget) + ".");
                Close();
            }
        }

        public void AU_Init()
        {
            Log.Items.Clear();
            Comp.Clear();
            currentBet = 10;
            foreach (Player p in MainWindow.cdata.players)
            {
                if (p == MainWindow.cdata.cPlayer)
                {
                    Log.Items.Add(p.name + " , начальная ставка: 10");
                }
                else
                {
                    if (p.isalive && p.budget >= currentBet) Comp.Add(new Player(p.playerid,p.code,p.c,p.face) {name=p.name,budget=p.budget,isalive=true,mono=p.mono,property=p.property,position=p.position } );
                }

            }
            if (Comp.Count == 0)
            {
                MainWindow.cdata.wnd.MainLog.Items.Insert(0, "Аукцион не состоялся: у игроков недостаточно средств");

                Close();

            }
            else
            {
                fp = Comp.First();
                if (Comp.Count == 1)
                {
                    var yyy = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position];
                    var gPlayer = MainWindow.cdata.players[fp.playerid];
                    yyy.prop.ownerplayer = gPlayer;
                    gPlayer.budget -= currentBet;
                    gPlayer.property.Add(yyy.prop);
                    var str = "p" + Convert.ToString(yyy.y) + Convert.ToString(yyy.x) + "4";
                    Rectangle r = (Rectangle)MainWindow.cdata.wnd.Board.FindName(str);
                    r.Fill = new SolidColorBrush(gPlayer.c);
                    if (gPlayer.HasMono(yyy.card.color))
                    {
                        gPlayer.mono.Add(yyy.card.color);
                    };
                    MainWindow.cdata.wnd.MainLog.Items.Insert(0, "Аукцион не состоялся: только " + Comp.First().name + " имеет достаточно средств");
                    MainWindow.cdata.wnd.MainLog.Items.Insert(0, Comp.First().name + " купил " + yyy.prop.name + " за " + currentBet + ". Оставшийся бюджет " + Convert.ToString(gPlayer.budget) + ".");
                    Close();
                }
                foreach (Player c in Comp)
                {
                    var s = "comp" + Convert.ToString(Comp.IndexOf(c) + 1);
                    var v = (Rectangle)FindName(s);
                    v.Fill = new SolidColorBrush(c.c);
                    v.Visibility = Visibility.Visible;
                }

                cComp.Fill = new SolidColorBrush(fp.c);
                var prt = (Image)FindName("portrait");
                prt.Source = fp.face.Source;
                PInfo.Content = fp.name + ", " + Convert.ToString(fp.budget);
            }
        }
    }
}
