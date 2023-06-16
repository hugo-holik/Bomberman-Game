using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Bomberman.Items;

namespace Bomberman.Blocks
{
    internal class BlockFragile : Block
    {
        private Random _random;

        public BlockFragile(int x, int y) : base(x, y, Brushes.Gray)
        {
            _random = new Random();
        }

        public override void Delete()
        {
            base.Delete();

            int randNumber = _random.Next(4);
            if (randNumber == 1)
            {
                Mine mine = new Mine();
                mine.Place(base.X, base.Y);
            }
        }
    }
}
