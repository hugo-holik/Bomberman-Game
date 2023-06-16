using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bomberman.Utils;

namespace Bomberman.Blocks
{
    internal abstract class Block
    {
        public const int Size = 40;

        private CanvasShape _block;
        public int X { get; }
        public int Y { get; }

        public Block(int x, int y, Brush color)
        {
            X = x;
            Y = y;
            MainWindow mainWin = (MainWindow)Application.Current.MainWindow;
            Canvas canvas = mainWin.Canvas;
            _block = new CanvasShape(Block.Size * x, Block.Size * y, Block.Size, Block.Size, ShapeType.Rectangle, canvas);
            _block.ChangeColor(color);
        }

        public virtual void Delete()
        {
            _block.Delete();
        }

    }
}
