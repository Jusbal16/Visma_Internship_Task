using System;
using System.Collections.Generic;

namespace Visma_Internship_Task
{
    public class Manager
    {
        UI ui = new UI();
        Library library = new Library();

        public Manager(string filename, string filename2, string filename3)
        {
            //load library books from file
            library.filenameForBooks = filename;
            library.LoadJsonBooks();
            //load library list from file
            library.filenameForTakenBooks = filename2;
            library.LoadJsonTakenBooks();
            //load user interface to console
            ui.loadUItext(filename3);
            ui.loadUI();
        }

        public void MainProgram()
        {
            List<string> arg = ui.returnCommand();
            while (arg[0]!= "end")
            {
                switch (arg[0].ToLower())
                {
                    case "add":
                        addBook(arg);
                        break;
                    case "take":
                        takeBook(arg);
                        break;
                    case "return":
                        returnBook(arg);
                        break;
                    case "list":
                        listBooks(arg);
                        break;
                    case "delete":
                        deleteBook(arg);
                        break;
                    case "show-ui":
                        ui.loadUI();
                        break;
                    default:
                        Console.WriteLine("Incorret command");
                        break;
                }
                arg = ui.returnCommand();
            }
        }
        private void addBook(List<string> args)
        {
            if(args.Count == 8)
            {
                int bookCopies;
                if (int.TryParse(args[7], out bookCopies))
                {


                    if (bookCopies < 1)
                    {
                        Console.WriteLine("Book can't have less than 1 number of copies");
                    }
                    else if (library.checkIfBookExist(args[1], args[2]))
                    {
                        if (library.checkMultipleISBN(args[0]))
                        {
                            Console.WriteLine("Book with this ISBN already exist in library");
                        }
                        else
                        {
                            int index = library.returnBookIndex(args[1], args[2]);
                            library.increaseBookCount(index, bookCopies);
                            library.makeBookAvailable(index);
                            library.overWriteJsonBooks();
                        }

                    }
                    else
                    {
                        Books books = new Books(new Book(args[1], args[2], args[3], args[4], args[5], args[6]), bookCopies);
                        library.addBook(books);
                        library.overWriteJsonBooks();
                    }
                }
                else
                {
                    Console.WriteLine("CopyNumber must be integer");
                }
                
            } else
            {
                Console.WriteLine("Not enought arguments");
            }

        }
        private void takeBook(List<string> args)
        {
            int index = 0;

            if (args.Count == 5)
            {
                int days;
                if (int.TryParse(args[4], out days))
                {
                    if (!library.checkIfBookExist(args[1], args[2]))
                    {
                        Console.WriteLine("Book doesn't exist");
                    }
                    else
                    {
                        index = library.returnBookIndex(args[1], args[2]);


                        if (library.checkBookPerPerson(args[3]))
                        {
                            Console.WriteLine("You already have 3 books");
                        }
                        else if (days > 60) //check 
                        {
                            Console.WriteLine("We can't give you book for that long");
                        }
                        else if (!library.checkBookBalance(index))
                        {
                            Console.WriteLine("We don't have this book");
                        }
                        else if (library.checkIfPersonHasBook(library.returnBookISBN(index), args[3]))
                        {
                            Console.WriteLine("You already have this book");
                        }
                        else
                        {
                            TakenBooksList takenBook = new TakenBooksList(library.returnBookISBN(index), args[3], days);
                            library.takeBook(takenBook);
                            library.reduceBookNumber(index);
                            library.overWriteJsonBooks();
                            library.overWriteJsonTakenBooks();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Days must be integer");
                }

            }
            else
            {
                Console.WriteLine("Not enought arguments");
            }

        }
        private void returnBook(List<string> args)
        {

            if (args.Count == 4)
            {
                if (!library.checkIfBookExist(args[1], args[2]))
                {
                    Console.WriteLine("Book doesn't exist");
                }
                else
                {
                    int index = library.returnBookIndex(args[1], args[2]);

                    if (!library.checkIfPersonHasBook(library.returnBookISBN(index), args[3]))
                    {
                        Console.WriteLine("Book doesn't exist");
                    }
                    else
                    {

                        library.removeFromTakenList(library.returnBookISBN(index), args[3]);
                        library.addBookNumber(index);
                        library.overWriteJsonBooks();
                        library.overWriteJsonTakenBooks();

                    }
                    if (library.checkIfLate(library.returnBookISBN(index)))
                    {
                        Console.WriteLine("Wanted for returning books late, Dead or Alive :)");
                    }
                }
            }
            else
            {
                Console.WriteLine("Not enought arguments");
            }

        }
        private void listBooks(List<string> args)
        {
            if (args.Count == 3)
            {
                library.listBooks(args[1], args[2]);
            }
            else
            {
                Console.WriteLine("Not enought arguments");
            }
        }
        private void deleteBook(List<string> args)
        {
            if (args.Count == 3)
            {
                if (!library.checkIfBookExist(args[1], args[2]))
                {
                    Console.WriteLine("Book doesn't exist");
                }
                else
                {
                    int index = library.returnBookIndex(args[1], args[2]);
                    library.deleteBook(index);
                    library.overWriteJsonBooks();
                }
            }
            else
            {
                Console.WriteLine("Not enought arguments");
            }

        }

    }
}
