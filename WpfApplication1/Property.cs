using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WpfApplication1
{
    public class Property
    {
        public string name;
        public cellType PropType;
        public Player ownerplayer;
        public int cost_to_buy;
        public int zalog;

        public void free()
        {
            ownerplayer = null;
            var str = name+"4";
            var r = (Rectangle)MainWindow.cdata.wnd.Board.FindName(str);
            r.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            if (this.PropType == cellType.country)
            ((Country)this).houses = 0;
        }
       
    }
    class Country : Property {
        public int cost_to_build;
        public int houses;
        public int[] rent=new int[6];

    }
    class Transport : Property{
        public int multiplier;
        public int hasTheSame(Player p)
        {
            var c = 0;
            foreach (Property pr in p.property)
            {
                if (pr.PropType == cellType.transport) { c++; }
            }
            return c;
        }
    }

    class Service : Property {
        public int multiplier;
        public bool issecond(Player p)
        {
            var c = 0;
            foreach (Property pr in p.property)
            {
                if (pr.PropType == cellType.service)
                { c++; }
            }
            return (c==2);
        }
    }
}
