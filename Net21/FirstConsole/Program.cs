using System;
public class GamesStepCounter {

    public static void Main() {

        Console.WriteLine("Welcome to the game Step Counter");

        string name;
        do {
            Console.WriteLine("\nEnter youre name please :)");
            name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            { // IsNullOrEmpty в основном делает проверку если пользователь не введет данные (null) своего имени 
                Console.WriteLine("Name cannot empty! Please enter your name to play this game ");
            }
        }
        while (string.IsNullOrEmpty(name));
        Console.WriteLine($"Welcome to the game {name}");
        char choice;
        do
        {
            Console.WriteLine("\nStart game Step");
            // Ввод начальной позиции с проверкой
            int start;

            while (true) {
                Console.WriteLine("Enter start position:");
                if (int.TryParse(Console.ReadLine(), out start)) { 
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a number.");

            }


            // Ввод целевой позиции с проверкой
            int target;
            while (true)
            {
                Console.Write("Enter target position: ");
                if (int.TryParse(Console.ReadLine(), out target))
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a number.");
            }




            // Ввод размера шага с проверкой
            int stepSize;
            while (true)
            {
                Console.WriteLine("Enter max step size by positive:");
                if (int.TryParse(Console.ReadLine(), out stepSize)&& stepSize>0)
                {
                    break;
                }
                Console.WriteLine("Invalid input! Please enter a positive number.");

            }

            if (start == target) {
                Console.WriteLine("You're already at the target position!");
            }
            else {
                int steps = (Math.Abs(target-start)+stepSize-1)/stepSize;
                Console.WriteLine($"\n{name}, you need {steps} steps to win!");
            }
            Console.Write("\nDo you want try again? (yes (y)/no (n)): ");
            choice = Console.ReadKey().KeyChar;
        }
        while (char.ToLower(choice) == 'y');
        Console.WriteLine("Tnaks the games ;)");
    }
   
}


