using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Data;

namespace WpfApplication1
{
    public enum cellType {jail, random, transport, service, country,special};
    public class Cell
    {
        public int num;
        public int x;
        public int y;
        public cellType type;
        public Card card;
        public string textDescr;
        public Property prop;
        public CardStack eff;
        public Cell()
        {
            card = new Card();
            num = x = y = -1;
            textDescr = "";
            prop = new Property();
        }
    }
}
