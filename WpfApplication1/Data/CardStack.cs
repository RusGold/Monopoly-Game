using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApplication1.Data
{
    public class RandomCard
    {
        public string name;
        public string img;
        public string effect;
    }

    public class CardStack
    {
        private Random _r = new Random();
        public int qty;
        private List<RandomCard> _stack;

        public void Shuffle()
        {
            var tempStack = _stack;
            for (int i = 0; i < qty; i++)
            {
                var rand = _r.Next(0, qty-i);
                tempStack.Add(_stack[rand]);
                tempStack.RemoveAt(rand);
            }
            _stack = tempStack;
        }

        public RandomCard getNextCard()
        {
            var card = _stack.FirstOrDefault();
            _stack.RemoveAt(0);
            _stack.Add(card);
            return card;
        }

        public CardStack(string filename)
        {
            qty = 0;
            _stack = new List<RandomCard>();
            var line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                //read a line from file
                var parts= line.Split('\t');
                var rCard = new RandomCard { name = parts[1], effect = parts[3], img = parts[2] };
                _stack.Add(rCard);
                qty++;
            }
            Shuffle();
        }
    }
}
