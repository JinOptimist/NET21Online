
// Последовательное выполнение двух методово
//Method1Blue();
//Method2Red();

var obj = new object();
var counter = 0;

// Паралельное выполнение двух методово
Task.Run(Method1Blue);
Task.Run(Method1Blue);

Console.ReadKey();

void Method1Blue()
{
    while (true)
    {
        // THREAD 1 SLEEP
        lock (obj)
        {
            // THREAD 2

            counter++;

            if (counter % 2 == 0)
            {
                Console.WriteLine($"Method1Blue {counter} EVEN");
            }
            else
            {
                Console.WriteLine($"Method1Blue {counter} ODD");
            }
        }
        
    }
}


void Method2Red()
{
    while (true)
    {
        // counter == 2
        counter++;
        // counter == 3
        // SLEEP
        if (counter % 2 == 0)
        {
            Console.WriteLine($"Method2Red {counter} EVEN");
        }
        else
        {
            Console.WriteLine($"Method2Red {counter} ODD");
        }
    }
}


