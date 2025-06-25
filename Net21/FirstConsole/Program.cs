var theNumber = 42;

var stepCount = 0;
int guess;
do
{
    Console.WriteLine("Guess the number");
    var guessString = Console.ReadLine();
    if (!int.TryParse(guessString, out guess))
    {
        Console.WriteLine("It's not a number");
        continue;
    }

    if (guess > theNumber)
    {
        Console.WriteLine("less");
    }
    else if (guess < theNumber)
    {
        Console.WriteLine("more");
    }
    stepCount++;
} while (guess != theNumber);

Console.WriteLine("You are win");
Console.WriteLine($"You did it by {stepCount} steps");
