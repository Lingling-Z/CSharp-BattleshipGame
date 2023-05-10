using BattleshipClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipClassLibrary
{
    public static class GameLogic
    {
        public static void InitializeGrid(PlayerInfoModel player)
        {
            List<string> letters = new List<string> { "A", "B", "C", "D", "E" };
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            foreach(string letter in letters)
            {
                foreach(int number in numbers)
                {
                    AddGridSpot(player, letter, number);
                }
            }
        }
        private static void AddGridSpot(PlayerInfoModel player, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number
            };
            player.ShotGrid.Add(spot);
        }
    }
}
