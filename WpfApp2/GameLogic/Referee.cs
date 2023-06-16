using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Bomberman.GameLogic
{
    internal class Referee
    {
        private int _gameDuration;
        private int _tick;
        private int _x;
        private int _y;

        public Referee(int gameDuration)
        {
            _tick = 0;
            _gameDuration = gameDuration;
            _x = 0;
            _y = -1;

            DispatcherTimer gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromSeconds(0.1);
            gameTimer.Tick += Tick;
            gameTimer.Start();
            

        }

        public void Tick(object sender, EventArgs e)
        {
            if (_tick > _gameDuration)
            {
                if (_tick % 2 == 0)
                {
                    if (_x == 0)
                    {
                        _y++;
                    }
                    _x = (_x + 1) % Game.ArenaWidth;
                    Game.GetInstance().PlaceBlockFixed(_x, _y);
                    Game.GetInstance().Explode(_x, _y);
                }
            }
            _tick++;
        }

        public void EndGame(String message)
        {
            MessageBox.Show(message);
            Environment.Exit(0);
        }
    }
}
