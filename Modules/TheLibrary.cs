namespace csharp_learning.Modules
{
    public class TheLibrary
    {

        private readonly List<Book> _books = new()
    {
        new Book("Hellsing", 666),
        new Book("Legends", 270)
    };

        public static void Run()
        {
            TheLibrary libraryInstance = new TheLibrary();
            libraryInstance.ShowMenu();
        }

        private void ShowMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("=== Welcome to The Library ===");
                Console.WriteLine("1. Show all books");
                Console.WriteLine("2. Add a book");
                Console.WriteLine("3. Change a book's status");
                Console.WriteLine("4. Lend book");
                Console.WriteLine("5. List of borrowers");
                Console.WriteLine("0. Exit program");
                Console.Write("Your choice: ");

                string? userChoice = Console.ReadLine();
                Console.WriteLine();

                switch (userChoice)
                {
                    case "1":
                        DisplayAllBooks();
                        break;

                    case "2":
                        AddBook();
                        break;

                    case "3":
                        ChangeBookStatus();
                        break;

                    case "4":
                        LendBook();
                        break;

                    case "5":
                        DisplayBorrowers();
                        break;

                    case "0":
                        Console.WriteLine("Exiting program");
                        isRunning = false;
                        break;
                }
            }
        }

        private void DisplayAllBooks()
        {
            foreach (Book book in _books)
            {
                Console.WriteLine($"Book: {book.BookName}" +
                    $" - Pages: {book.BookPages}" +
                    $" - Status: {book.BookStatus}" +
                    $" - Borrower: {book.CurrentBorrower?.Name ?? "Ingen"}");
            }
            Console.WriteLine();
        }

        private Book ChooseBook()
        {
            while (true)
            {
                Console.Write("Book name: ");
                string? chosenBook = Console.ReadLine();

                foreach (Book book in _books)
                {
                    if (book.BookName == chosenBook)
                    {
                        return book;
                    }
                }

                Console.WriteLine($"Invalid selection. choose a valid name from list.");
                Console.WriteLine();
            }
        }

        private void AddBook()
        {

            Console.Write("Book name: ");
            string? bookName = Console.ReadLine();

            while (true)
            {
                Console.Write("Total pages: ");
                if (int.TryParse(Console.ReadLine(), out int bookPages))
                {
                    _books.Add(new Book(bookName, bookPages));
                    Console.WriteLine($"Book {bookName} has been added to the library.");
                    Console.WriteLine();
                    return;
                }
                else
                {
                    Console.WriteLine("Please entere a whole number.");
                }
            }
        }

        private void ChangeBookStatus()
        {
            DisplayAllBooks();
            Book selectedBook = ChooseBook();

            while (true)
            {
                Console.WriteLine("Choose a new status");
                Console.WriteLine("1. Available");
                Console.WriteLine("2. Unavailable");
                Console.Write("Number: ");

                if (int.TryParse(Console.ReadLine(), out int newStatus))
                {
                    switch (newStatus)
                    {
                        case 1:
                            selectedBook.BookStatus = Status.Available;
                            Console.WriteLine();
                            return;

                        case 2:
                            selectedBook.BookStatus = Status.Unavailable;
                            Console.WriteLine();
                            return;
                    }
                }
                Console.WriteLine("Invalid selection. choose 1 or 2.");
                Console.WriteLine();
            }
        }
        private void LendBook()
        {
            string? borrowerName = null;
            while (borrowerName == null)
            {
                Console.Write("Name of borrower: ");
                string? temp = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(temp))
                {
                    Console.WriteLine("Invalid input. Enter a name.");
                    Console.WriteLine();
                }
                else
                {
                    borrowerName = temp;
                }
            }

            Borrower newBorrower = new Borrower(borrowerName);
            DisplayAllBooks();
            Book selectedBook = ChooseBook();
            selectedBook.BookStatus = Status.Unavailable;
            selectedBook.CurrentBorrower = newBorrower;

            Console.WriteLine($"Boken er nå lånt ut til {borrowerName}.");
            Console.WriteLine();
        }

        public void DisplayBorrowers()
        {
            foreach (Book book in _books)
            {
                Console.WriteLine($"Book: {book.BookName}" +
                    $" - Pages: {book.BookPages}" +
                    $" - Status: {book.BookStatus}" +
                    $" - Borrower: {book.CurrentBorrower?.Name ?? "Ingen"}" +
                    $" - LibraryCardNumber: {book.CurrentBorrower?.LibraryCardNumber}");
            }
            Console.WriteLine();
        }
    }
}

public enum Status
{
    Available,
    Unavailable
}

public class Book
{
    public string? BookName { get; private set; }
    public int BookPages { get; private set; }
    public Status BookStatus { get; set; } = Status.Available;
    public Borrower? CurrentBorrower { get; set; } = null;

    public Book(string? bookName, int bookPages)
    {
        BookName = bookName;
        BookPages = bookPages;
    }

}

public class Borrower
{
    public string Name { get; set; }

    public Guid LibraryCardNumber { get; } = Guid.NewGuid();

    public Borrower(string name)
    {
        Name = name;
    }
}
