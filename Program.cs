using csharp_learning.Modules;

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine("=== Choose program ===");
    Console.WriteLine("1. Temperature Converter");
    Console.WriteLine("2. To-Do List");
    Console.WriteLine("3. The Library");
    Console.WriteLine("4. TopSercetProject");
    Console.WriteLine("0. Exit program");
    Console.Write("Your choice: ");

    string? userChoice = Console.ReadLine();
    Console.WriteLine();

    switch (userChoice)
    {
        case "1":
            TemperatureChange.Run();
            break;

        case "2":
            ToDoList.Run();
            break;

        case "3":
            TheLibrary.Run();
            break;

        case "4":
            Console.WriteLine("Under development...");
            Console.WriteLine();
            break;

        case "0":
            Console.WriteLine("Exiting program");
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid input.");
            Console.WriteLine();
            break;

    }

}

