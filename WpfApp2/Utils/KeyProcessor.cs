using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Bomberman.GameLogic;

namespace Bomberman.Utils
{
    internal class KeyProcessor
    {
        Player _player1;
        Player _player2;
        public KeyProcessor(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
        }


        public void ProcessKey(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.W:
                    _player1.MoveUp();
                    break;
                case Key.A:
                    _player1.MoveLeft();
                    break;
                case Key.S:
                    _player1.MoveDown();
                    break;
                case Key.D:
                    _player1.MoveRight();
                    break;
                case Key.F:
                    _player1.ActivateItem();
                    break;
                case Key.G:
                    _player1.CycleThroughItems();
                    break;

                case Key.Up:
                    _player2.MoveUp();
                    break;
                case Key.Left:
                    _player2.MoveLeft();
                    break;
                case Key.Down:
                    _player2.MoveDown();
                    break;
                case Key.Right:
                    _player2.MoveRight();
                    break;
                case Key.N:
                    _player2.ActivateItem();
                    break;
                case Key.M:
                    _player2.CycleThroughItems();
                    break;
                default:
                    break;
            }



        }
    }

}
