using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media;


namespace WpfApplication1.Data
{
    public class CommonData
    {
        public Player[] players;
        public Card[] cards=new Card[60];
        public Property[] properties=new Property[40];
        public Cell[] cells = new Cell[40];
        public MainWindow wnd;
        public Player cPlayer;
        public CardStack FreePark = new CardStack("park.txt");
        public CardStack od = new CardStack("vault.txt");
        public CardStack rd = new CardStack("chance.txt");
        public CardStack lt = new CardStack("lux_tax.txt");
        public CardStack it = new CardStack("inc_tax.txt");
        //public CardStack Luxury

        public CommonData(MainWindow Wnd)
        {
            wnd = Wnd;
            for (int i = 0; i < 40; i++) { cells[i] = new Cell(); }
            var line = "";
            var path = "countries.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                string[] part = line.Split('\t'); // разбили строку на part
                cells[Convert.ToInt16(part[0])].card.color = part[1];
                cells[Convert.ToInt16(part[0])].card.img = part[2];
                if (part[3] != "0") //country or service or transport
                {
                    if (part[4] != "0")
                    {
                        properties[Convert.ToInt16(part[0])] = new Country { PropType = cellType.country, name = null, cost_to_build=Convert.ToInt16(part[11]), cost_to_buy = Convert.ToInt16(part[3]), zalog = Convert.ToInt16(part[3]) >> 1 };
                        for (int i = 0; i < 6; i++)
                        {
                            ((Country)properties[Convert.ToInt16(part[0])]).rent[i] = Convert.ToInt16(part[i + 4]);
                        }
                        cells[Convert.ToInt16(part[0])].type = cellType.country;

                    }
                    if (part[4] == "0" && part[3] == "200") //transport
                    {
                        cells[Convert.ToInt16(part[0])].type = cellType.transport;
                        properties[Convert.ToInt16(part[0])] = new Service { PropType = cellType.transport, name = null, cost_to_buy = Convert.ToInt16(part[3]), zalog = Convert.ToInt16(part[3]) >> 1, multiplier = 25 };

                    }
                    if (part[4] == "0" && part[3] == "150") //service
                    {
                        cells[Convert.ToInt16(part[0])].type = cellType.service;
                        properties[Convert.ToInt16(part[0])] = new Service { PropType = cellType.service, name = null, cost_to_buy = Convert.ToInt16(part[3]), zalog = Convert.ToInt16(part[3]) >> 1, multiplier = 4 };

                    }
                    cells[Convert.ToInt16(part[0])].prop = properties[Convert.ToInt16(part[0])];
                    cells[Convert.ToInt16(part[0])].textDescr = Convert.ToString(part[12]);
                }
                else //vault, chance
                {
                    cells[Convert.ToInt16(part[0])].type = cellType.random;
                    switch (part[10])
                    {
                        case "0":
                            cells[Convert.ToInt16(part[0])].eff = rd;
                            break;
                        case "1":
                            cells[Convert.ToInt16(part[0])].eff = od;
                            break;
                        case "2":
                            cells[Convert.ToInt16(part[0])].eff = lt;
                            break;
                        case "3":
                            cells[Convert.ToInt16(part[0])].eff = it;
                            break;

                    }
                }
                cells[30].type = cellType.jail;
                cells[20].type = cellType.random;
                cells[20].eff = FreePark;
                cells[0].type = cellType.special;
                cells[10].type = cellType.special;
            }
            for (int i = 0; i <= 10; i++)
            {
                cells[i].y = 0;
                cells[i].x = i;
            };
            for (int i = 11; i < 20; i++)
            {
                cells[i].x = 10;
                cells[i].y = i - 10;
            };
            for (int i = 20; i <= 30; i++)
            {
                cells[i].y = 10;
                cells[i].x = 30 - i;
            };
            for (int i = 31; i < 40; i++)
            {
                cells[i].x = 0;
                cells[i].y = 40 - i;
            };

            for (int i = 0; i <40; i++)
            {
                if (cells[i].card.img != "")
                {
                    var str = "p" + Convert.ToString(cells[i].y) + Convert.ToString(cells[i].x);
                    cells[i].prop.name = str;
                    str=str+ "2";
                    Image d = (Image)wnd.Board.FindName(str);
                    d.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/" + cells[i].card.img));
                    if (cells[i].textDescr != "")
                    {
                        var des = (TextBlock)wnd.Board.FindName(str + "t");
                        des.Text = cells[i].textDescr;
                    }
                    str = "p" + Convert.ToString(cells[i].y) + Convert.ToString(cells[i].x) + "1";
                    Rectangle r = (Rectangle)wnd.Board.FindName(str);
                    r.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte((cells[i].card.color).Substring(1, 2), 16), Convert.ToByte((cells[i].card.color).Substring(3, 2), 16), Convert.ToByte((cells[i].card.color).Substring(5, 2), 16)));
                };
            };


        }

    }
}

