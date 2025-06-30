using System.Reflection.Metadata.Ecma335;

Console.WriteLine("Hello! Enter players name:");
var PlayerName = Console.ReadLine();

Console.WriteLine("Choose difficulty:");
Console.WriteLine("Press E for Easy");
Console.WriteLine("Press M for Medium");
Console.WriteLine("Press H for Hard");
var Max = 0;
var Min = 0;
while (Max == 0)
{
    var DiffChoose = Console.ReadLine();
    if (DiffChoose == "E")
    { Max = 10; }
    else if (DiffChoose == "M")
    { Max = 100; }
    else if (DiffChoose == "H")
    { Max = 1000; }
    else
    {
        Console.WriteLine("You pressed wrong letter");
        continue;
    }
}

var Attempts = (int)Math.Ceiling(Math.Log(Max - Min + 1, 2));

Random random = new Random();
var theNumber = random.Next(Min, Max);

int guess;
var counter = 0;
do
{
    Console.WriteLine($"Guess the number, {PlayerName} between {Min} and {Max}");
    Console.WriteLine($"You have {Attempts} attemps ");
    var guessString = Console.ReadLine();
    if (!int.TryParse(guessString, out guess))
    {
        Console.WriteLine("It's not a number");
        continue;
    }
    if (guess < Min || guess > Max)
    {
        Console.WriteLine("Out of range!");
        continue;
    }
    if (Attempts == 1)
    {
        Console.WriteLine("You lose");
        break;
    }
    else if (guess > theNumber)
    {
        Console.WriteLine("less");
        if (guess < Max)
        { Max = guess - 1; }
        Attempts--;
    }
    else if (guess < theNumber)
    {
        Console.WriteLine("more");
            if (guess > Min)
        { Min = guess + 1; } 
        Attempts--;
    }
    

    counter++;
} while (guess != theNumber);

if(Attempts>1)
{
Console.WriteLine($"You are win, {PlayerName}! It tooks {counter} attemps");
}