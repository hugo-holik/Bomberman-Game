using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bomberman.GameLogic;

namespace Bomberman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event EventHandler<KeyEventArgs> KeyPressed;
        public Canvas Canvas { get; }

        public MainWindow()
        {
            InitializeComponent();
            Canvas = new Canvas();
            Content = Canvas;

            Game.GetInstance();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            KeyPressed?.Invoke(this, e);
        }

        public void Draw(Shape shape, int x, int y)
        {
            //_canvas.Children.Remove(shape);
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
            Canvas.Children.Add(shape);
        }

        public void Redraw(Shape shape, int x, int y)
        {
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
        }
    }
}
