using BullsAndCows;



Console.WriteLine("Enter 1 if you want play with bot. " +
                "\nEtner 2 if you have a friend");
var gameMode = Console.ReadLine();
switch (gameMode)
{
    case ("1"):
       var game = new BullAndCowBotVsHuman();
        game.Play();
        break;
    case ("2"):
        var game2 = new BullAndCowHumanVsHuman();
        game2.Play();
        break;
    default:
        Console.WriteLine("wrong number");
        return;
}