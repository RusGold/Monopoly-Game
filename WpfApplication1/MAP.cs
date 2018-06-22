using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WpfApplication1
{
    public class MAP
    {
        public Cell[] cells =new Cell[40];
        public Property[] props = new Property[40];

        public void update_map(MainWindow wnd)
        {
            for (int i = 0; i <= 39; i++)
            {
                if (cells[i].card.img != "")
                {
                    var str = "p"+ Convert.ToString(cells[i].y) + Convert.ToString(cells[i].x)  + "2";
                    Image d = (Image)wnd.Board.FindName(str);
                    d.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/" + cells[i].card.img));
                    str = "p" + Convert.ToString(cells[i].y) + Convert.ToString(cells[i].x) + "1";
                    Rectangle r=(Rectangle)wnd.Board.FindName(str);
                    r.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte((cells[i].card.color).Substring(1, 2), 16), Convert.ToByte((cells[i].card.color).Substring(3, 2), 16), Convert.ToByte((cells[i].card.color).Substring(5, 2), 16)));  
                };
            };
        }

        public MAP()
        {
            for (int i = 0; i < 40; i++) { cells[i] = new Cell(); }

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
            for (int i = 20;i<=30;i++)
            {
                cells[i].y = 10;
                cells[i].x = 30 - i;
            };
            for (int i = 31; i < 40; i++)
            {
                cells[i].x = 0;
                cells[i].y = 40 - i;
            };
            var line = "";
            var path = "countries.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {       
            string[] part = line.Split('\t'); // разбили строку на part
                cells[Convert.ToInt16(part[0])].card.color = part[1];
                cells[Convert.ToInt16(part[0])].card.img = part[2];
                if (part[11]=="0") { props[Convert.ToInt16(part[0])] = new Property {name=null,cost_to_buy=Convert.ToInt16(part[3]), zalog=Convert.ToInt16(part[4]) }; }
            }

        }
    }

}

                  
