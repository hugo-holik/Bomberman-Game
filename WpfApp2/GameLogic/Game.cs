using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Bomberman.Blocks;
using Bomberman.Items;
using Bomberman.Utils;
using Block = Bomberman.Blocks.Block; 

namespace Bomberman.GameLogic
{
    internal class Game
    {
        // Pre zmenu velkosti hracej areny staci prepisat hodnoty ArenaHeight a ArenaWidth
        // Poznamka: iba neparne cisla (aby hracie pole bolo symetricke)
        public const int ArenaHeight = 11;
        public const int ArenaWidth = 13;

        private static Game Instance;

        private Block[,] _enviroment;
        private Item[,] _items;
        private Player _player1;
        private Player _player2;
        private Referee _referee;

        private Game()
        {
            _enviroment = new Block[ArenaHeight, ArenaWidth];
            for (int y = 0; y < ArenaHeight; y++)
            {
                for (int x = 0; x < ArenaWidth; x++)
                {

                    if (y == 0 || y == ArenaHeight - 1)
                    {
                        BlockFixed blockFixed = new BlockFixed(x, y);
                        _enviroment[y, x] = blockFixed;
                    }
                    else if (y % 2 != 0)
                    {
                        if (x == 0 || x == ArenaWidth - 1)
                        {
                            BlockFixed blockFixed = new BlockFixed(x, y);
                            _enviroment[y, x] = blockFixed;
                        }
                    }
                    else
                    {
                        if (x % 2 == 0)
                        {
                            BlockFixed blockFixed = new BlockFixed(x, y);
                            _enviroment[y, x] = blockFixed;
                        }
                    }
                }
            }

            for (int y = 2; y < ArenaHeight - 1; y++)
            {
                for (int x = 0; x < ArenaWidth; x++)
                {
                    if (_enviroment[y, x] == null)
                    {
                        BlockFragile blockFragile = new BlockFragile(x, y);
                        _enviroment[y, x] = blockFragile;
                    }
                }
            }

            _referee = new Referee(240);
            _player1 = new Player(Brushes.Blue);
            _player2 = new Player(Brushes.Red);

            MainWindow mainWin = (MainWindow)Application.Current.MainWindow;
            var keyProcessor = new KeyProcessor(_player1, _player2);
            mainWin.KeyPressed += keyProcessor.ProcessKey;

            _items = new Item[Game.ArenaHeight, Game.ArenaWidth];
        }


        public static Game GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Game();
            }

            return Instance;
        }

        public bool IsBlockOnPosition(int x, int y)
        {
            return _enviroment[y, x] != null;
        }

        /// <summary>
        /// Na poziadanie vytvori vybuch na danej
        /// pozicii v hracom poli. Ak sa vo vybuchu
        /// nachadza hrac, prehrava.
        /// </summary>
        public void Explode(int x, int y)
        {
            BlockExplosion blockExplosion = new BlockExplosion(x, y);
            if (_player1.PositionY == y && _player1.PositionX == x)
            {
                _referee.EndGame("Vyhral 2.hrac!");
            }
            if (_player2.PositionY == y && _player2.PositionX == x)
            {
                _referee.EndGame("Vyhral 1.hrac!");
            }

            Block block = _enviroment[y, x];
            if (block is BlockFragile) {
                block.Delete();
                _enviroment[y, x] = null;
            }
        }

        public void PlaceBlockFixed(int x, int y)
        {
            _enviroment[y, x] = new BlockFixed(x, y);
            Explode(x, y);
        }

        public void PlaceItem(Item item, int x, int y)
        {
            _items[y, x] = item;
        }

        public void DeleteItem(int x, int y)
        {
            _items[y, x] = null;
        }

        public Item GetItem(int x, int y)
        {
            return _items[y, x];
        }
    }
}
