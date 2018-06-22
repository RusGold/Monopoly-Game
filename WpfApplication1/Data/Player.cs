using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
namespace WpfApplication1
{
   
    public class Player
    {
        public int playerid;
        public bool isalive;
        public string name;
        public string code;
        public Color c;
        public Image face;
        public List<Property> property = new List<Property>() ;
        public Int32 budget;
        public List<string> mono;
        public int position;

        public Player(int ID, string Name,Color Color, Image fce)
        
        {
            face = fce;
            face.Visibility = System.Windows.Visibility.Visible;
            face.Name = Name;
            budget = 1500;
            position = 0;
            mono = new List<string>();
            property = new List<Property>();
            code = "pl"+Convert.ToString(ID);
            playerid = ID;
            name=Name;
            c = Color;
            isalive = true;
        }

        public void Goto(int newPosition,bool addMoney)
        {
            if (addMoney) { budget += 200; }
            position = newPosition;
            var i1 = (Ellipse)MainWindow.cdata.wnd.Board.FindName("pl" + Convert.ToString(this.playerid) + "1");
            var i0 = (Ellipse)MainWindow.cdata.wnd.Board.FindName("pl" + Convert.ToString(this.playerid) + "0");
            i0.Visibility = Visibility.Hidden;
            i1.Visibility = Visibility.Hidden;
            i0.SetValue(Grid.RowProperty, MainWindow.cdata.cells[position].y);
            i0.SetValue(Grid.ColumnProperty, MainWindow.cdata.cells[position].x);
            i1.SetValue(Grid.RowProperty, MainWindow.cdata.cells[position].y);
            i1.SetValue(Grid.ColumnProperty, MainWindow.cdata.cells[position].x);
            i0.Visibility = Visibility.Visible;
            i1.Visibility = Visibility.Visible;
        }
        public void Move(int Steps)
        {

            position += Steps;
            if (position >= 40)
            {
                position -= 40;
                budget += 200;
            }
            var i1 = (Ellipse) MainWindow.cdata.wnd.Board.FindName("pl" + Convert.ToString(this.playerid)+"1");
            var i0 = (Ellipse)MainWindow.cdata.wnd.Board.FindName("pl" + Convert.ToString(this.playerid) + "0");
            i0.Visibility = Visibility.Hidden;
            i1.Visibility = Visibility.Hidden;
            i0.SetValue(Grid.RowProperty,  MainWindow.cdata.cells[position].y);
            i0.SetValue(Grid.ColumnProperty, MainWindow.cdata.cells[position].x);
            i1.SetValue(Grid.RowProperty, MainWindow.cdata.cells[position].y);
            i1.SetValue(Grid.ColumnProperty, MainWindow.cdata.cells[position].x);
            i0.Visibility = Visibility.Visible;
            i1.Visibility = Visibility.Visible;
        }
        public void Update()
        {
            ;

        }

        public bool HasMono(string ccolor)
        {
            var myMono = true;
            var cCells = MainWindow.cdata.cells.ToList<Cell>().Where(x => (x.prop.PropType == cellType.country && x.card.color == ccolor));
            if (cCells.Count() == 0) return false;
            foreach (Cell c in cCells)
                    myMono = myMono && (c.prop.ownerplayer == this);
            return myMono;
        }

        public void PayOrBuy(int dices)
        {
            var yyy = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position];
            var CellIsEmpty = (yyy.prop.ownerplayer == null);
            switch (yyy.type)
            {
                case cellType.country:
                    if (CellIsEmpty)
                    {
                        ////// buy ////
                        if (budget >= yyy.prop.cost_to_buy)
                        {
                            yyy.prop.ownerplayer = MainWindow.cdata.cPlayer;
                            budget -= yyy.prop.cost_to_buy;
                            property.Add(yyy.prop);
                            var str = "p" + Convert.ToString(yyy.y) + Convert.ToString(yyy.x) + "4";
                            Rectangle r = (Rectangle)MainWindow.cdata.wnd.Board.FindName(str);
                            r.Fill = new SolidColorBrush(c);
                            MainWindow.cdata.wnd.MainLog.Items.Insert(0, name + " купил " + yyy.prop.name+" за "+yyy.prop.cost_to_buy+". Оставшийся бюджет "+Convert.ToString(budget));
                            if (HasMono(yyy.card.color))
                            {
                                mono.Add(yyy.card.color);
                                MainWindow.cdata.wnd.MainLog.Items.Insert(0, name + " теперь монополист( " + yyy.card.color + " )! Арендная плата удваивается.");

                            };
                        }
                        else
                        {
                            MainWindow.cdata.wnd.Au.AU_Init();
                            if (MainWindow.cdata.wnd.Au.Comp.Count()>1) MainWindow.cdata.wnd.Au.ShowDialog();
                        }
                    }
                    else //pay
                    {
                        if ((yyy.prop.ownerplayer != MainWindow.cdata.cPlayer) && (((Country)yyy.prop).rent[((Country)yyy.prop).houses] != 0))
                        {
                            var nrent = ((Country)yyy.prop).rent[((Country)yyy.prop).houses];
                            if (yyy.prop.ownerplayer.HasMono(yyy.card.color) && ((Country)yyy.prop).houses == 0) { nrent = nrent << 1; }
                            if (budget >= nrent)
                            {
                                budget -= nrent;
                                yyy.prop.ownerplayer.budget += nrent;
                                MainWindow.cdata.wnd.MainLog.Items.Insert(0, name + " платит " + yyy.prop.ownerplayer.name + " " + Convert.ToString(nrent)+ ". Оставшийся бюджет " + Convert.ToString(budget));

                            }
                            else
                            {
                                MessageBox.Show(name + " is a bankrupt!");
                                drop();
                            }
                        }

                    }
                    break;
                case cellType.service:
                    if (CellIsEmpty)
                    {
                        ////// buy ////
                        if (MainWindow.cdata.cPlayer.budget >= yyy.prop.cost_to_buy)
                        {
                            yyy.prop.ownerplayer = MainWindow.cdata.cPlayer;
                            MainWindow.cdata.cPlayer.budget -= MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position].prop.cost_to_buy;
                            MainWindow.cdata.cPlayer.property.Add(yyy.prop);
                            var str = "p" + Convert.ToString(yyy.y) + Convert.ToString(yyy.x) + "4";
                            Rectangle r = (Rectangle)MainWindow.cdata.wnd.Board.FindName(str);
                            r.Fill = new SolidColorBrush(MainWindow.cdata.cPlayer.c);
                            MainWindow.cdata.wnd.MainLog.Items.Insert(0, name + " купил " + yyy.prop.name + " за " + yyy.prop.cost_to_buy + ". Оставшийся бюджет " + Convert.ToString(budget));
                            var i = 0;
                            var Service = new List<string>();
                            foreach(Property p in property)
                            {
                                if (p.PropType == cellType.service)
                                {
                                    i++;
                                    Service.Add(p.name);

                                }
                            }
                            var scount = Service.Count;
                            foreach (string name in Service)
                            {

                                ((Service)MainWindow.cdata.cells.ToList<Cell>().Find(x => x.prop.name == name).prop).multiplier = (scount == 2 ? 10 : 4);
                            }
                        }
                        else
                        {
                            MainWindow.cdata.wnd.Au.AU_Init();
                            if (MainWindow.cdata.wnd.Au.Comp.Count() > 1) MainWindow.cdata.wnd.Au.ShowDialog(); 
                        }
                    }
                    else
                    {
                        if (yyy.prop.ownerplayer != MainWindow.cdata.cPlayer) 
                        { 
                            var payment =dices*((Service)yyy.prop).multiplier;
                            if (MainWindow.cdata.cPlayer.budget >= payment)
                            {
                                MainWindow.cdata.cPlayer.budget -= payment;
                                yyy.prop.ownerplayer.budget += payment;
                                MainWindow.cdata.wnd.MainLog.Items.Insert(0, name + " платит " + yyy.prop.ownerplayer.name + " " + Convert.ToString(payment) + ". Оставшийся бюджет " + Convert.ToString(budget));
                            }
                            else
                            {
                                MessageBox.Show(name + " is a bankrupt!");
                                MainWindow.cdata.cPlayer.drop();
                            }
                        }
                    }
                    break;
                case cellType.transport:
                    if (CellIsEmpty)
                    {
                        ////// buy ////
                        if (MainWindow.cdata.cPlayer.budget >= yyy.prop.cost_to_buy)
                        {
                            yyy.prop.ownerplayer = MainWindow.cdata.cPlayer;
                            MainWindow.cdata.cPlayer.budget -= MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position].prop.cost_to_buy;
                            MainWindow.cdata.cPlayer.property.Add(yyy.prop);
                            var str = "p" + Convert.ToString(yyy.y) + Convert.ToString(yyy.x) + "4";
                            Rectangle r = (Rectangle)MainWindow.cdata.wnd.Board.FindName(str);
                            r.Fill = new SolidColorBrush(MainWindow.cdata.cPlayer.c);
                            MainWindow.cdata.wnd.MainLog.Items.Insert(0, name + " купил " + yyy.prop.name + " за " + yyy.prop.cost_to_buy + ". Оставшийся бюджет " + Convert.ToString(budget));
                            var i = 0;
                            var Transport = new List<string>();
                            foreach (Property p in property)
                            {
                                if (p.PropType == cellType.transport)
                                {
                                    i++;
                                    Transport.Add(p.name);

                                }
                            }
                            var scount = Transport.Count;
                            foreach (string name in Transport)
                            {

                                ((Service)MainWindow.cdata.cells.ToList<Cell>().Find(x => x.prop.name == name).prop).multiplier = (25<<(scount-1));
                            }
                        }
                        else
                        {
                            MainWindow.cdata.wnd.Au.AU_Init();
                            if (MainWindow.cdata.wnd.Au.Comp.Count() > 1) MainWindow.cdata.wnd.Au.ShowDialog();
                        }
                    }
                    else
                    {
                        if (yyy.prop.ownerplayer != MainWindow.cdata.cPlayer)
                        { 
                            var payment = ((Service)yyy.prop).multiplier;
                            if (MainWindow.cdata.cPlayer.budget >= payment)
                            {
                                MainWindow.cdata.cPlayer.budget -= payment;
                                yyy.prop.ownerplayer.budget += payment;
                                MainWindow.cdata.wnd.MainLog.Items.Insert(0, name + " платит " + yyy.prop.ownerplayer.name + " " + Convert.ToString(payment) + ". Оставшийся бюджет " + Convert.ToString(budget));
                            }
                            else
                            {
                                MessageBox.Show(name + " is a bankrupt!");
                                drop();
                            }
                        }
                    }
                    break;
                case cellType.jail:
                    MessageBox.Show("Отправляйся в тюрьму!");
                    Goto(10,false);
                    break;
                case cellType.random:
                    var card = yyy.eff.getNextCard();
                    MessageBox.Show(card.name);
                    var s = card.effect;
                    string[] parts = s.Split('.');
                    switch (parts[1])
                    {
                        case "move":
                            Move(Convert.ToInt16(parts[2]));
                            PayOrBuy(0);
                            break;
                        case "goto":
                            var cType = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position].type;
                            switch (parts[2])
                            {
                                case "service":
                                    while (cType != cellType.service)
                                    {
                                        Move(1); cType = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position].type;
                                    }
                                    PayOrBuy(0);
                                    break;
                                case "transport":
                                    cType = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position].type;
                                    while (cType != cellType.transport)
                                    {
                                        Move(1); cType = MainWindow.cdata.cells[MainWindow.cdata.cPlayer.position].type;
                                    }
                                    PayOrBuy(0);
                                    break;
                                default:
                                    Goto(Convert.ToInt16(parts[2]), false);
                                    PayOrBuy(0);
                                    break;
                            }
                            
                            break;
                        case "money":
                            MainWindow.cdata.cPlayer.budget += Convert.ToInt16(parts[2]);
                            break;
                        case "buildings":
                            break;
                        case "players":
                            break;
                        default:
                            break;

                    }
                    break;
            }
            MainWindow.cdata.wnd.PInfo.Text = name + Convert.ToChar(13) + Convert.ToString(budget);
            if (budget < 0)
            {
                MessageBox.Show(name + " is a bankrupt!");
                drop();
            }

        }

        public void drop()
        {
            isalive = false;
            var e = (Ellipse)MainWindow.cdata.wnd.FindName("pl" + Convert.ToString(playerid)+"0");
            e.Visibility = System.Windows.Visibility.Hidden;
            e.SetValue(Grid.RowProperty, 0);
            e.SetValue(Grid.ColumnProperty,0);
            e = (Ellipse)MainWindow.cdata.wnd.FindName("pl" + Convert.ToString(playerid) + "1");
            e.Visibility = System.Windows.Visibility.Hidden;
            e.SetValue(Grid.RowProperty, 0);
            e.SetValue(Grid.ColumnProperty, 0);
            foreach (Property p in property)
            {
                p.free();
                
            }
        }


    }
}
