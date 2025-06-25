
using System.Numerics;
using System.Xml.Linq;

int minRangeNumber;
int maxRangeNumber;
Console.WriteLine("Hey there! What's your name?");
var playerName = Console.ReadLine();
Console.WriteLine($"Nice to meet you, {playerName}! Let's play a guessing game!");
do
{
    Console.WriteLine("Pick a range. What's the smallest number?");
    if (!int.TryParse(Console.ReadLine(), out minRangeNumber))
    {
        Console.WriteLine("Error, the value entered must be an integer, please try again");
    }
    else 
    {
        break;
    }
} while (true);
do
{
    Console.WriteLine("And the biggest number?");
    if (!int.TryParse(Console.ReadLine(), out maxRangeNumber))
    {
        Console.WriteLine("Error, the value entered must be an integer, please try again");
    }
    else
    {
        break;
    }
} while (true);

if (minRangeNumber > maxRangeNumber) 
{
    int a = minRangeNumber;
    minRangeNumber = maxRangeNumber;
    maxRangeNumber = a;
}
Console.WriteLine($"Got it! I’m thinking of a number between {minRangeNumber} and {maxRangeNumber}. Try to guess it!");
var randomNumber = new Random();
int numberToGuess = randomNumber.Next(minRangeNumber, maxRangeNumber + 1);
var attemptsCount  = 0;

for(int i = 0; (int)(Math.Pow(2, i)) <
    maxRangeNumber - minRangeNumber; i++) 
{ 
    attemptsCount++;
}
Console.WriteLine($"Focus, {playerName}! You've got {attemptsCount} tries left.");

for(int i = 0; i <= attemptsCount; i++) 
{
    Console.WriteLine("Enter your number");
    int userNumber;
    do
    {
        if(!int.TryParse(Console.ReadLine(),out userNumber))
        {
            Console.WriteLine("Error, the value entered must be an integer, please try again");
        }
        else 
        {
            break;
        }

    } while (true);

    if(userNumber > numberToGuess) 
    {
        Console.WriteLine("Too high! My number is lower.");
    }else if(userNumber < numberToGuess) 
    {
        Console.WriteLine("Nope! My number is higher. Try again!");
    }
    else 
    {
        Console.WriteLine($"YES! You got it! My number was {numberToGuess}. Good job,{playerName}!");
        return;
    }
}
Console.WriteLine($"Oops, out of tries! My number was {numberToGuess}. Better luck next time!");






