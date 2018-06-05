using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
  public class Card
    {
        private string _color;
        private string _img;
        public string color
        { get { return _color; }
          set { _color = value; }
        }
        public string img
        { get { return _img; }
          set { _img = value; }
        }
        public Card()
        {
            _img = "";
            _color = "";
        }
        
    }
}
