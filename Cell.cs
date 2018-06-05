using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Cell
    {
        public int num;
        public int x;
        public int y;
        public Card card;
        public Cell()
        {
            card = new Card();
            num = x = y = -1;
        }
    }
}
