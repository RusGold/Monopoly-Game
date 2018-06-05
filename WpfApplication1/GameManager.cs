using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfApplication1.Data;


namespace WpfApplication1
{
    public class GameManager
    {
        public int currentplayerid=0;
        List<Player> players = new List<Player>();
        public Random d1;
        public Random d2;

        public GameManager()
        {
            d1 = new Random();
            Thread.Sleep(1000);
            d2 = new Random();
        }

        public void gamemngr()
        {
            while (true)
            {
                getparametrs();

                while (checkplayers())
                {
                    foreach (Player p in MainWindow.cdata.players)
                    {
                        if (p.isalive)
                        {
                            p.mover();
                            
                        }
                        else
                        {
                            //nothingtodo();
                        }
                    }
                }
            }
        }
        public void getparametrs()
        {

        }
        public bool checkplayers()
        {
            var nnn = 0;
            foreach (Player p in MainWindow.cdata.players)

            {
                if (p.isalive)
                {
                    nnn++;
                }
            }
            if (nnn > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}



    
    

