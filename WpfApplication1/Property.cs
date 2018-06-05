using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Property
    {
        public string name;
        public Player ownerplayer;
        public int cost_to_buy;
        public int zalog;
       
    }
    class Country : Property {
        public int cost_to_build;
        public int houses;
        public int rent;



    }
    class Service : Property {
        public int multiplier;
        public bool issecond(Player p) { return false; }


    }
}
