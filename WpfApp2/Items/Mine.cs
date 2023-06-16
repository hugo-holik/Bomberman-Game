using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Bomberman.GameLogic;

namespace Bomberman.Items
{
    internal class Mine : Item, IReactionOnPickUp
    {
        private bool _isActivated;

        public Mine() : base(Brushes.Red)
        {
            _isActivated = false;
        }

        public override void Activate(Player player)
        {
            _isActivated = true;
            Place(player.PositionX, player.PositionY);
            ChangeColor(Brushes.Yellow);
        }

        public override void Place(int x, int y)
        {
            base.Place(x, y);
            Game.GetInstance().PlaceItem(this, x, y);
        }

        public void ReactOnPickUp()
        {
            if (_isActivated)
            {
                Game.GetInstance().Explode(base.X, base.Y);
            }
        }

        public bool IsActivated()
        {
            return _isActivated;
        }

    }
}
