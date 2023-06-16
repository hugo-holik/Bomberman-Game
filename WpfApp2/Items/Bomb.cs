using System;
using System.Windows.Media;
using System.Windows.Threading;
using Bomberman.GameLogic;

namespace Bomberman.Items
{
    internal class Bomb : Item
    {
        /// <summary>
        /// Smery do ktorych dosiahne vybuch
        /// od pozicie bomby.
        /// </summary>
        private static readonly int[,] ExplosionRadius = new int[,]
        {
            { 0, 0 },
            { -1, -1 },
            { 0, -1 },
            { 1, -1 },
            { 1, 0 },
            { 1, 1 },
            { 0, 1 },
            { -1, 1 },
            { -1, 0 }
        };

        /// <summary>
        /// Ak je _isBombPlaced true, tak
        /// hrac nemoze polozit dalsiu bombu.
        /// </summary>
        private bool _isBombPlaced;

        private int _tick;

        public Bomb() : base(Brushes.Black)
        {
            _tick = 0;
        }

        public override void Activate(Player player)
        {
            int x = player.PositionX;
            int y = player.PositionY;
            Place(x, y);

            _isBombPlaced = true;

            DispatcherTimer bombTimer = new DispatcherTimer();
            bombTimer.Interval = TimeSpan.FromSeconds(0.5);
            bombTimer.Tick += Tick;
            bombTimer.Start();

            _tick = 0;
        }

        /// <summary>
        /// Casovac bomby - spusti sa
        /// ked je bombe poslana
        /// sprava Activate(Player player).
        /// </summary>
        public void Tick(object sender, EventArgs e)
        {
            if (_isBombPlaced)
            {
                _tick++;
                if (_tick % 2 == 0)
                {
                    ChangeColor(Brushes.Yellow);
                }
                else
                {
                    ChangeColor(Brushes.Black);
                }

                if (_tick > 6)
                {
                    ((DispatcherTimer)sender).Stop();

                    DeleteItem();
                    _isBombPlaced = false;
                    Explode();

                }
            }
        }

        private void Explode()
        {
            for (int i = 0; i < Bomb.ExplosionRadius.GetLength(0); i++)
            {
                int directionX = Bomb.ExplosionRadius[i, 0];
                int directionY = Bomb.ExplosionRadius[i, 1];

                Game.GetInstance().Explode(base.X + directionX, base.Y + directionY);
            }
        }

        public bool IsBombPlaced()
        {
            return _isBombPlaced;
        }
    }
}
