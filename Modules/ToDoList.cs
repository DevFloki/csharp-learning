namespace csharp_learning.Modules
{
    internal class ToDoList
    {
        public static void Run()
        {
            List<string> toDoList = new();
            toDoList.Add("Buy apples");
            toDoList.Add("Go for a walk");
            toDoList.Add("Shower");

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("=== Choose next action ===");
                Console.WriteLine("1. Display To-Do List");
                Console.WriteLine("2. Add to list");
                Console.WriteLine("3. Remove from list");
                Console.WriteLine("0. Go back to programs overview");

                Console.Write("Your choice: ");
                string? userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case "1":
                        DisplayList(toDoList);
                        break;

                    case "2":
                        AddToList(toDoList);
                        break;

                    case "3":
                        RemoveFromList(toDoList);
                        break;

                    case "0":
                        Console.WriteLine($"Going back to overview \n");
                        isRunning = false;
                        break;
                }
            }
        }

        private static void DisplayList(List<string> toDoList)
        {
            Console.WriteLine("To-Do List");
            int index = 1;
            foreach (string task in toDoList)
            {
                Console.WriteLine($"{index}. {task}");
                index += 1;
            }
            Console.WriteLine();
        }

        private static void AddToList(List<string> toDoList)
        {
            DisplayList(toDoList);

            while (true)
            {
                Console.Write("What do you want to add: ");
                string? taskToAdd = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(taskToAdd))
                {
                    toDoList.Add(taskToAdd);
                    return;
                }
                Console.WriteLine("Invalid input");
                Console.WriteLine();
            }
        }


        private static void RemoveFromList(List<string> toDoList)
        {
            DisplayList(toDoList);

            if (toDoList.Count == 0)
            {
                Console.WriteLine($"The List is already empty! \n");
                return;
            }

            Console.Write("Choose what item to remove: ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int taskToRemove))
                {
                    if (taskToRemove > 0 && taskToRemove <= toDoList.Count)
                    {
                        toDoList.RemoveAt(taskToRemove - 1);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Number is not in list range");
                    }
                }
                else
                {
                    Console.WriteLine("Enter a number from list");
                }
            }
        }
    }
}
