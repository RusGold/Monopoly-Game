using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Threading;
namespace WpfApplication1
{
   
    public class Player
    {
        public int playerid;
        public bool isalive;
        public string color;
        public Color c;
        public Image face;
        public List<Property> property = new List<Property>() ;
        public int budget;
        public int position;

        public Player(string Name,Color Color)
        
        {
            face = new Image();
            face.Visibility = System.Windows.Visibility.Hidden;
            face.Name = Name;
            budget = 1500;
            position = 0;
            property = null;
            color = Color.ToString();
            c = Color;
            isalive = true;

            face.Source = new BitmapImage(new Uri("pack://application:,,,/WpfApplication1;component/Resources/male_dummy_128.png"));

        }

        public void Goto(int newPosition,bool addMoney)
        {
            if (addMoney) { budget += 200; }
            position = newPosition;
        }
        public void Move(int Steps)
        {

            position += Steps;
            if (position >= 40)
            {
                position -= 40;
                budget += 200;
            }
            var ii = (Ellipse) MainWindow.cdata.wnd.Board.FindName("pl" + Convert.ToString(this.playerid));
            ii.SetValue(Grid.RowProperty,  MainWindow.cdata.cells[position].y);
            ii.SetValue(Grid.ColumnProperty, MainWindow.cdata.cells[position].x);
        }
        public void Update()
        {
            ;

        }
        public void mover()
        {
            //Make a turn

        }

    }
}

