namespace csharp_learning.Modules
{
    public class TheLibrary
    {
        private readonly List<Book> _books = new()
        {
        new Book("Hellsing", 666),
        new Book("Legends", 270)
        };

        public bool IsEmpty => _books.Count == 0;

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
                Console.WriteLine("6. Remove a book");
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

                    case "6":
                        RemoveBook();
                        break;

                    case "0":
                        Console.WriteLine("Exiting program.");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private void DisplayAllBooks()
        {
            if (!HasBookOrDisplayMessage())
            {
                return;
            }

            foreach (Book book in _books)
            {
                Console.WriteLine($"Book: {book.BookName}" +
                    $" - Pages: {book.BookPages}" +
                    $" - Status: {book.BookStatus}" +
                    $" - Borrower: {book.CurrentBorrower?.Name ?? "None"}");
            }
            Console.WriteLine();
        }

        private Book? ChooseBook()
        {
            if (!HasBookOrDisplayMessage())
            {
                return null;
            }

            while (true)
            {
                Console.Write("Book name: ");
                string? chosenBook = Console.ReadLine();

                Book? selectedBook = _books.FirstOrDefault(book =>
                string.Equals(
                    book.BookName,
                    chosenBook,
                    StringComparison.OrdinalIgnoreCase));

                if (selectedBook is not null)
                {
                    return selectedBook;
                }

                Console.WriteLine($"Invalid selection. choose a valid name from list.");
                Console.WriteLine();
            }
        }

        private void AddBook()
        {
            string bookName;

            while (true)
            {
                Console.Write("Book name: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Name cannot be empty.");
                }
                else if (_books.Any(b => b.BookName == input))
                {
                    Console.WriteLine("That book is already in the library");
                }
                else
                {
                    bookName = input;
                    break;
                }
            }

            while (true)
            {
                Console.Write("Total pages: ");
                if (int.TryParse(Console.ReadLine(), out int bookPages)
                    && bookPages > 0)
                {
                    _books.Add(new Book(bookName, bookPages));
                    Console.WriteLine($"Book {bookName} has been added to the library.");
                    Console.WriteLine();
                    return;
                }

                Console.WriteLine("Please entere a positive whole number.");

            }
        }

        private void ChangeBookStatus()
        {
            if (!HasBookOrDisplayMessage())
            {
                return;
            }

            DisplayAllBooks();
            Book? selectedBook = ChooseBook();
            if (selectedBook == null)
            {
                return;
            }

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
                            ReturnBook(selectedBook);
                            Console.WriteLine();
                            return;

                        case 2:
                            if (selectedBook.BookStatus == Status.Borrowed)
                            {
                                Console.WriteLine("Book is currently being borrowed right now.");
                                Console.WriteLine();
                                return;
                            }

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
            if (!_books.Any(b => b.BookStatus == Status.Available))
            {
                Console.WriteLine("Sorry, no books are currently available.");
                Console.WriteLine();
                return;
            }

            DisplayAllBooks();
            Book? selectedBook = null;

            while (true)
            {
                selectedBook = ChooseBook();
                if (selectedBook?.BookStatus == Status.Borrowed)
                {
                    Console.WriteLine("That book is currently borrowed.");
                    Console.WriteLine();
                }
                else if (selectedBook?.BookStatus == Status.Unavailable)
                {
                    Console.WriteLine("That book is currently unavailable.");
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }


            string? borrowerName = null;
            while (borrowerName == null)
            {
                Console.Write("Name of borrower: ");
                string? name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Invalid input. Enter a name.");
                    Console.WriteLine();
                }
                else
                {
                    borrowerName = name;
                }
            }

            Borrower newBorrower = new Borrower(borrowerName);

            selectedBook?.BookStatus = Status.Borrowed;
            selectedBook?.CurrentBorrower = newBorrower;

            Console.WriteLine($"Book is now borrowed to {borrowerName}.");
            Console.WriteLine();
        }

        public void DisplayBorrowers()
        {
            if (!_books.Any(book => book.BookStatus == Status.Borrowed))
            {
                Console.WriteLine("No book is being borrowed right now.");
                Console.WriteLine();
                return;
            }

            List<Book> borrowedBooks = _books
                .Where(book => book.CurrentBorrower is not null)
                .ToList();

            foreach (Book book in borrowedBooks)
            {
                Console.WriteLine($"Book: {book.BookName}" +
                    $" - Pages: {book.BookPages}" +
                    $" - Borrower: {book.CurrentBorrower?.Name}" +
                    $" - LibraryCardNumber: {book.CurrentBorrower?.LibraryCardNumber}");
            }
            Console.WriteLine();
        }

        public void ReturnBook(Book selectedBook)
        {
            selectedBook.BookStatus = Status.Available;
            selectedBook.CurrentBorrower = null;
        }

        public void RemoveBook()
        {
            DisplayAllBooks();
            if (!HasBookOrDisplayMessage())
            {
                return;
            }

            Book? selectedBook = ChooseBook();
            if (selectedBook != null)
            {
                _books.Remove(selectedBook);
                Console.WriteLine($"{selectedBook.BookName} has been removed.");
                Console.WriteLine();
            }
        }

        public bool HasBookOrDisplayMessage()
        {
            if (!IsEmpty)
            {
                return true;
            }

            Console.WriteLine("The library is empty.");
            Console.WriteLine();
            return false;
        }
    }
}

public enum Status
{
    Available,
    Unavailable,
    Borrowed
}

public class Book
{
    public string BookName { get; private set; }
    public int BookPages { get; private set; }
    public Status BookStatus { get; set; } = Status.Available;
    public Borrower? CurrentBorrower { get; set; } = null;

    public Book(string bookName, int bookPages)
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
