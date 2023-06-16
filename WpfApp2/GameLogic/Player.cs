using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Bomberman.Blocks;
using Bomberman.Items;
using Bomberman.Utils;

namespace Bomberman.GameLogic
{
    internal class Player
    {
        private CanvasShape _piece;
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        private Item _itemInHand;
        private List<Mine> _mineMagazine;

        public Player(Brush color)
        {
            PositionX = 1;
            PositionY = 1;
            MainWindow mainWin = (MainWindow)Application.Current.MainWindow;
            Canvas canvas = mainWin.Canvas;
            _piece = new CanvasShape(Block.Size * PositionX, Block.Size * PositionY, Block.Size, Block.Size, ShapeType.Ellipse, canvas);
            _piece.ChangeColor(color);

            _itemInHand = new Bomb();
            _mineMagazine = new List<Mine>();
        }

        public void MoveUp()
        {
            if (!IsMovementPossibleInDirection(0, -1))
            {
                return;
            }

            _piece.MoveVertically(-Block.Size);
            PositionY--;
            TakeItem(PositionX, PositionY);
        }

        public void MoveDown()
        {
            if (!IsMovementPossibleInDirection(0, 1))
            {
                return;
            }

            _piece.MoveVertically(Block.Size);
            PositionY++;
            TakeItem(PositionX, PositionY);
        }

        public void MoveLeft()
        {
            if (!IsMovementPossibleInDirection(-1, 0))
            {
                return;
            }

            _piece.MoveHorizontally(-Block.Size);
            PositionX--;
            TakeItem(PositionX, PositionY);
        }

        public void MoveRight()
        {
            if (!IsMovementPossibleInDirection(1, 0))
            {
                return;
            }

            _piece.MoveHorizontally(Block.Size);
            PositionX++;
            TakeItem(PositionX, PositionY);
        }

        /// <summary>
        /// Zoberie predmet na danej pozicii a podla jeho typu
        /// rozhodne co s nim dalej. Ak je predmet null,
        /// tak to znamena ze na pozicii ziadny predmet nie je.
        /// </summary>
        private void TakeItem(int x, int y)
        {
            Item item = Game.GetInstance().GetItem(PositionX, PositionY);
            if (item == null)
            {
                return;
            }
            if (item is IReactionOnPickUp) 
            {
                ((IReactionOnPickUp)item).ReactOnPickUp();
            }
            if (item is Mine) 
            {
                Mine mine = (Mine)item;
                if (mine.IsActivated())
                {
                    return;
                }
                _mineMagazine.Add(mine);
            }
            Game.GetInstance().DeleteItem(PositionX, PositionY);
            item.DeleteItem();
        }

        public void CycleThroughItems()
        {
            if (_itemInHand is Bomb) 
            {
                if (((Bomb)_itemInHand).IsBombPlaced())
                {
                    return;
                }
                _itemInHand = new Mine();
            } 
            else
            {
                _itemInHand = new Bomb();
            }
        }

        /// <summary>
        /// Aktivuje predmet, ktory ma hrac v ruke.
        /// </summary>
        public void ActivateItem()
        {
            if (_itemInHand is Bomb) {
                if (((Bomb)_itemInHand).IsBombPlaced())
                {
                    return;
                }
                _itemInHand = new Bomb();
            }
            if (_itemInHand is Mine) {
                if (_mineMagazine.Count() < 1)
                {
                    return;
                }

                _itemInHand = _mineMagazine.ElementAt(0); 
                _mineMagazine.RemoveAt(0); 
            }
            _itemInHand.Activate(this);
        }

        private bool IsMovementPossibleInDirection(int directionX, int directionY)
        {
            return !Game.GetInstance().IsBlockOnPosition(PositionX + directionX, PositionY + directionY);
        }
    }
}
