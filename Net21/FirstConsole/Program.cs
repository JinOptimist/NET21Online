bool gameLoop = true;
bool isWin = false;

var theNumber = 42;
int guess;
int? minGuess = null;
int? maxGuess = null;

int step = 0;
int attempt = 10; //max step count
int[] journal = new int[attempt]; //previous user steps

string userName;


Console.WriteLine("------------------------------------------");
Console.WriteLine("Enter your name: ");
userName = Console.ReadLine()!;
if (userName == "") userName = "user";

Console.WriteLine("------------------------------------------");
do
{
    Console.WriteLine($"{userName}, Guess the number between ({(minGuess == null ? "-INF" : minGuess)}; {(maxGuess == null ? "+INF" : maxGuess)})"); // INF is equals "infinity"
    if (minGuess != null && maxGuess != null)
    {
        Console.WriteLine($"You can do it for {CalculateMinStepsToWin((int)minGuess, (int)maxGuess, theNumber)} steps");
    }
    Console.WriteLine($"You have {attempt} attempts");
    var guessString = Console.ReadLine();
    if (!int.TryParse(guessString, out guess))
    {
        Console.WriteLine($"|   IT'S NOT A NUMBER, {userName}");
        continue;
    }
    //checking on repeating guess
    if (Array.Find(journal, el => el == guess) != 0) Console.WriteLine("|   YOU HAVE TRIED THIS VALUE EARLIER!!!");
    journal[step] = guess;
    step++;
    attempt--;

    if (guess == theNumber)
    {
        isWin = true;
        gameLoop = false;
    }
    else if (guess > theNumber)
    {
        Console.WriteLine($"less, {userName}");
        if (maxGuess == null) maxGuess = guess;
        else if (guess < maxGuess) maxGuess = guess;
    }
    else if (guess < theNumber)
    {
        Console.WriteLine($"more, {userName}");
        if (minGuess == null) minGuess = guess;
        else if (guess > minGuess) minGuess = guess;
    }
    if (attempt == 0 && !isWin)
    {
        gameLoop = false;
    }
    Console.WriteLine("------------------------------------------");
} while (gameLoop);

Console.WriteLine(isWin ? $"|   YOU ARE WIN, {userName}, FOR {step} STEPS" : $"|   YOU ARE LOSE, {userName}");
Console.WriteLine("------------------------------------------");


static int CalculateMinStepsToWin(int min, int max, int value)
{
    int iter = 0;

    while (min <= max)
    {
        iter += 1;
        int middle = (max + min) / 2;

        if (middle == value) break;
        else if (middle > value)
        {
            max = middle;
        }
        else if (middle < value)
        {
            min = middle;
        }
    }

    return iter;
}
