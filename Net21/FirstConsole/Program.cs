var theNumber = 42; 
var name = "user";
var attempts = 0;
int guess;
var maxCount = 20; // число попыток
int? previousNumber = null;
int? currentAttempt = null;
var minInterval = 0;
var maxInterval = 100;

    do // ввод имени пользователя + проверка на пустое поле 
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
        currentAttempt = maxCount - attempts;
        Console.WriteLine($"Guess the number {name}. You have only {currentAttempt} attempts. The interval between {minInterval} and  {maxInterval} ");
        var guessString = Console.ReadLine();
    
    if (!int.TryParse(guessString, out guess))
        {
            Console.WriteLine("It's not a number");
            continue;
        }
       
    
    if (guess==previousNumber) //сравнение с предыдущим числом
        {
        Console.WriteLine("You've already wrote it");
        continue;
        }
        previousNumber = guess;

    if (guess > maxInterval) //мин/макс интервал и изменения диапазона
        {
        Console.WriteLine("Your number is more than max Limit. Your move is not counted.");
        attempts--;
        }
        else if (guess < minInterval)
        {
        Console.WriteLine("Your number is less than min Limit. Your move is not counted.");
        attempts--;
        }
        else if (guess> theNumber)
        {
        maxInterval = guess;
        }
        else if (guess < theNumber)
        { 
        minInterval = guess; 
        }


    if (guess > theNumber) // логика мор/лесс
        {
            Console.WriteLine("less");
           
        }
            else if (guess < theNumber)
        {
            Console.WriteLine("more");  
        }


    attempts++; 
        
    if (attempts >= maxCount) // проверка на лимит ходов
    {
        Console.WriteLine($"Loser. You've reached {maxCount} attempts ");
        return;
        }

} while (guess != theNumber);

Console.WriteLine($"You are win {name}");
Console.WriteLine($"You made {attempts} attempts");

