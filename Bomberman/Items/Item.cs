using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bomberman.Blocks;
using Bomberman.GameLogic;
using Bomberman.Utils;

namespace Bomberman.Items
{
    internal abstract class Item
    {
        private Brush _color;
        private CanvasShape _shape;
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Item(Brush color)
        {
            _color = color;
        }

        public virtual void Place(int x, int y)
        {
            X = x;
            Y = y;

            MainWindow mainWin = (MainWindow)Application.Current.MainWindow;
            Canvas canvas = mainWin.Canvas;
            _shape = new CanvasShape(x * Block.Size, y * Block.Size, Block.Size, Block.Size, ShapeType.Triangle, canvas);
            _shape.ChangeColor(_color);
        }

        public abstract void Activate(Player player);

        protected void ChangeColor(Brush color)
        {
            _shape.ChangeColor(color);
        }

        public void DeleteItem()
        {
            _shape.Delete();
        }
    }
}
