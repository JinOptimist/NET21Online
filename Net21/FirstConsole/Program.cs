var theNumber = 42;
var name = "user";
int guess;

do 
    {
    Console.WriteLine("Enter your name:");
    name = Console.ReadLine();
    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("The field name is empty. Please Enter your name");
        continue;
    }
    }  while (string.IsNullOrEmpty(name));

    do
    {
        Console.WriteLine($"Guess the number {name}");
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
    } while (guess != theNumber);

Console.WriteLine($"You are win {name}"); 


