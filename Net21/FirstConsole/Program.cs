var theNumber = 42;

int guess;

List<int> guessedNumbers = new List<int>();

do
{
    Console.WriteLine("Guess the number");
    var guessString = Console.ReadLine();
    if (!int.TryParse(guessString, out guess))
    {
        Console.WriteLine("It's not a number");
        continue;
    }

    if (guessedNumbers.Contains(guess))
    {
        Console.WriteLine("You already entered this number");
        continue;
    }
    
    guessedNumbers.Add(guess);

    if (guess > theNumber)
    {
        Console.WriteLine("less");
    }
    else if (guess < theNumber)
    {
        Console.WriteLine("more");
    }
} while (guess != theNumber);

Console.WriteLine("You are win");
