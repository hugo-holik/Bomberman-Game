using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Bomberman.Blocks
{
    internal class BlockFixed : Block
    {
        public BlockFixed(int x, int y) : base(x, y, Brushes.DarkGray)
        {
        }
    }
}
