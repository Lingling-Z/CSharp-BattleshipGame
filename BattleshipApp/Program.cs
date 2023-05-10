using BattleshipClassLibrary;
using BattleshipClassLibrary.Models;

WelcomeMessage();
PlayerInfoModel player1 = CreatePlayer();
//ask for ship placement
//validate spot
//store
//clear



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
static PlayerInfoModel CreatePlayer()
{
    PlayerInfoModel output = new PlayerInfoModel();
    output.UserName = GetUsersName();
    GameLogic.InitializeGrid(output);

    return output;
}