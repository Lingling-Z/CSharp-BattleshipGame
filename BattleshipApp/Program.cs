using BattleshipClassLibrary;
using BattleshipClassLibrary.Models;

WelcomeMessage();
PlayerInfoModel activePlayer = CreatePlayer("player1");
PlayerInfoModel opponent = CreatePlayer("player2");
PlayerInfoModel winner = null;

do
{
    DisplayShotGrid(activePlayer);
    RecordShot(activePlayer, opponent);
    bool doesGameContinue = GameLogic.PlayerStillAlive(opponent);
    if(doesGameContinue == true)
    {
        //swap position
        (activePlayer, opponent) = (opponent, activePlayer);
    }
    else
    {
        winner = activePlayer;
    }
} while (winner == null);
EndGameMessage(winner);
Console.ReadLine();





void RecordShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
{
    bool isValidShot = false;
    string row = "";
    int column = 0;

    while (isValidShot == false) 
    {
        string shot = AskForShot(activePlayer);
        (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
        isValidShot = ValidateLocation(activePlayer, row, column);
    } 

    bool isAHit =  GameLogic.IdentifyShotResult(opponent, row, column);
    GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
    DisplayShotResult(row, column, isAHit);
    
}

void DisplayShotResult(string row, int column, bool isAHit)
{
    if (isAHit)
    {
        Console.WriteLine("Great! That's a hit!");
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("Not a hit");
        Console.WriteLine();
    }
}

static bool ValidateLocation(PlayerInfoModel player, string row, int column)
{
    bool isValidLocation = GameLogic.ValidateGridLocation(player, row, column);
    if (isValidLocation == false)
    {
        Console.WriteLine("Invalid shot location. Please try again.");
        Console.WriteLine("(A - E and 1 - 5, e.g. B3");
    }
    return isValidLocation;
}

static void WelcomeMessage()
{
    Console.WriteLine("Welcome to the Battleship game!");
    Console.WriteLine();
}
static string GetUsersName()
{
    Console.Write("What is your name: ");
    string output = Console.ReadLine();
    return output;
}
static PlayerInfoModel CreatePlayer(string playerTitle)
{
    PlayerInfoModel output = new PlayerInfoModel();
    Console.WriteLine($"Player info for {playerTitle}");
    output.UserName = GetUsersName();
    GameLogic.InitializeShotGrid(output);
    PlaceShips(output);
    Console.Clear();

    return output;
}

static void PlaceShips(PlayerInfoModel player)
{
    int numberOfShipsPerUser = 5;
    while (player.ShipLocations.Count < numberOfShipsPerUser)
    {
        Console.Write($"Where do you want to place ship {player.ShipLocations.Count + 1}: ");
        string shipLocation = Console.ReadLine();
        bool isValidShipLocation = GameLogic.PlaceShip(player, shipLocation); 
        if (isValidShipLocation == false)
        {
            Console.WriteLine("Invalid ship location. Please try again.");
        }
    }

}

static void DisplayShotGrid(PlayerInfoModel player)
{
    string currentRow = player.ShotGrid[0].SpotLetter;

    foreach(var spot in player.ShotGrid)
    {
        if(spot.SpotLetter != currentRow)
        {
            Console.WriteLine();
            currentRow = spot.SpotLetter;
        }
        if(spot.Status == Enums.GridSpotStatus.Empty)
        {
            Console.Write($" {spot.SpotLetter}{spot.SpotNumber} ");
        }else if(spot.Status == Enums.GridSpotStatus.Hit)
        {
            Console.Write(" X  ");
        }else if(spot.Status == Enums.GridSpotStatus.Miss)
        {
            Console.Write(" O  ");
        }
        else
        {
            Console.Write(" ? ");
        }

    }
    Console.WriteLine();
}
static string AskForShot(PlayerInfoModel player)
{
    Console.Write($"Where do you want to shoot, {player.UserName}: ");
    string output = Console.ReadLine();
    return output;
}

void EndGameMessage(PlayerInfoModel winner)
{
    Console.WriteLine($"Congradulations to {winner.UserName}!");
    Console.WriteLine($"You took {GameLogic.GetShotCount(winner)} shots to win the game.");
}