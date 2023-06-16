using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace Bomberman.Blocks
{
    internal class BlockExplosion : Block
    {
        private const int ExplosionDuration = 0;
        private int _tick;

        public BlockExplosion(int x, int y) : base(x, y, Brushes.Yellow)
        {
            DispatcherTimer explosionTimer = new DispatcherTimer();
            explosionTimer.Interval = TimeSpan.FromSeconds(0.5);
            explosionTimer.Tick += Tick;
            explosionTimer.Start();

            _tick = 0;
        }

        public void Tick(object sender, EventArgs e)
        {
            ((DispatcherTimer)sender).Stop();
            Delete();
        }
    }
}
