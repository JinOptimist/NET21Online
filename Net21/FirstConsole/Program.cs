//#5
Console.WriteLine("What's your name?");
var name_user = Console.ReadLine();

//var theNumber = 42;

//#9
int min_number = 0;

Random rand = new Random(); // Создает рандомное число
var theNumber = rand.Next(0, 100);
int max_number = theNumber + rand.Next(30, 100); // Next задаёт диапозон рандомного числа

int guess = 0;


var step_count = 0; //#6
var min_step_count = UppdateMinStepCount(max_number, min_number); //#10
//var max_step_count = 10; //#7
var max_step_count = min_step_count + 10; //#10

bool isFail = false;

//#8
HashSet<int> user_numbers = new HashSet<int>(); //Бесконечный список, в котором могут храниться только НЕодинаковые элементы (т.е. из последоватьности "1 2 1 2" в массив войдут только 1 и 2 по одному разу "1 2")

Console.Write($"Hello {name_user}! Your goal is to guess the number.");
while (true)
{
    Console.WriteLine($"The least this number of attempts can be: {min_step_count}"); //#9
    do
    {
        //#7
        if (step_count >= max_step_count)
        {
            Error($"You have exceeded the number of attempts. The number you are trying to guess is: {theNumber}"); //Если мы привысили количество попыток, то вызывается функция Error c переданной строкой
            isFail = true;
            break; // Выходит из цикла
        }

        Console.WriteLine($"Guess the number. Its range is from {min_number} to {max_number}."); //#9

        var guessString = Console.ReadLine();
        if (!int.TryParse(guessString, out guess))
        {
            Console.WriteLine($"{name_user}, it's not a number");
            continue;
        }

        //#8
        user_numbers.Add(guess); // Добавляем элемент (Если такой элемент уже существует, то ничего не добавится) 

        if (step_count == user_numbers.Count)
        {
            Error($"{name_user}, you have already tried to answer this way. Try again.");
            continue;
        }

        if (guess > theNumber)
        {
            Console.WriteLine("less");
            if (max_number >= guess) max_number = guess; //#9
        }
        else if (guess < theNumber)
        {
            Console.WriteLine("more");
            if (min_number <= guess) min_number = guess; //#9
                                                         // Проверка сделана, чтобы игрок вводя меньше чем итоговая позиция, не менял её. 
        }

        step_count++; //#6

    } while (guess != theNumber);

    //#5
    Console.Clear();
    if(!isFail) Console.WriteLine($"{name_user}, you are win. {guess} is really my number. You did complete to {step_count} steps \nDo you want to continue? {{Yes or No}}"); //#6
    else Console.WriteLine($"{name_user}, you are Fail. \nDo you want to continue? {{Yes or No}}");

    var response_user = Console.ReadLine();

    if (response_user.ToLower().Contains("yes")) DefaultVariables(); 
    else return;
}

//My Create Idea
void DefaultVariables()
{
    step_count = 0;
    min_number = 0;
    theNumber = rand.Next(0, 100);
    max_number = theNumber + rand.Next(30, 100);
    guess = 0;
    min_step_count = UppdateMinStepCount(max_number, min_number);
    max_step_count = min_step_count + 10;
    user_numbers = new HashSet<int>();
    isFail = false;
    Console.Clear();
}

//#7
void Error(string? mess) // void - Значит функция ничего не возращает; string mess - Создана строка с именем mess
{
    Console.Clear(); // Очищаем консоль
    Console.WriteLine(mess);
}

//#10
int UppdateMinStepCount(int right, int left)
{
    int step_count = 0;

    while (left <= right)
    {
        int middle = left + (right - left) / 2;

        if (middle > theNumber) right = middle - 1;
        else left = middle + 1;

        step_count++;
    }

    return step_count;
}
