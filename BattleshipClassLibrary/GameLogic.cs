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
        public static void InitializeShotGrid(PlayerInfoModel player)
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
                SpotNumber = number,
                Status = Enums.GridSpotStatus.Empty
            };
            player.ShotGrid.Add(spot);
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = "";
            int column = 0;

            if(shot.Length != 2)
            {
                throw new ArgumentException("Invalid shot location", "shot");
            }
            else
            {
                char[] shotArray = shot.ToCharArray();
                row = shotArray[0].ToString();
                column = int.Parse(shotArray[1].ToString());

                return(row, column);
            }

        }

        public static bool ValidateGridLocation(PlayerInfoModel player, string row, int column)
        {
            foreach(var shot in player.ShotGrid)
            {
                if(row.ToUpper() == shot.SpotLetter && column == shot.SpotNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            bool isAHit = false;
            foreach(var ship in opponent.ShipLocations)
            {
                if(row.ToUpper() == ship.SpotLetter && column == ship.SpotNumber)
                {
                    isAHit = true;
                    ship.Status = Enums.GridSpotStatus.Sunk;
                    break;
                }
            }
            return isAHit;
        }

        public static void MarkShotResult(PlayerInfoModel activePlayer, string row, int column, bool isAHit)
        {
            foreach (var shot in activePlayer.ShotGrid)
            {
                if (row.ToUpper() == shot.SpotLetter && column == shot.SpotNumber)
                {
                    if (isAHit)
                    {
                        shot.Status = Enums.GridSpotStatus.Hit;
                    }
                    else
                    {
                        shot.Status= Enums.GridSpotStatus.Miss;
                    }
                }
            }

        }

        public static bool PlayerStillAlive(PlayerInfoModel player)
        {
            foreach(var ship in player.ShipLocations)
            {
                if(ship.Status != Enums.GridSpotStatus.Sunk)
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetShotCount(PlayerInfoModel player)
        {
            int count = 0;
            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != Enums.GridSpotStatus.Empty)
                {
                    count++;
                }
            }
            return count;
        }

        public static bool PlaceShip(PlayerInfoModel player, string shipLocation)
        {
            bool output = false;

            (string row, int column) = SplitShotIntoRowAndColumn(shipLocation);
            bool isOnGrid = ValidateGridLocation(player, row, column);
            bool isOpenShipLocation = ValidateShipLocation(player, row, column);
            if (isOnGrid && isOpenShipLocation)
            {
                GridSpotModel ship = new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column
                };
                player.ShipLocations.Add(ship);
                output = true;
            }
            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel player, string row, int column)
        {
            bool output = true;
            foreach (var ship in player.ShipLocations)
            {
                if (row.ToUpper() == ship.SpotLetter && column == ship.SpotNumber)
                {
                    output = false;
                    break;
                }
            }
            return output;
        }
    }
}
