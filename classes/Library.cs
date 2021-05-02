using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Visma_Internship_Task
{
    public class Library
    {
        public List<Books> books { get; set; }
        public List<TakenBooksList> takenBooksLists { get; set; }
        public string filenameForBooks { get; set; }
        public string filenameForTakenBooks { get; set; }
        public Library()
        {
        }

        public void LoadJsonBooks()
        {
            using (StreamReader r = new StreamReader(filenameForBooks))
            {
                string json = r.ReadToEnd();
                this.books = JsonConvert.DeserializeObject<List<Books>>(json);
            }
        }
        public void LoadJsonTakenBooks()
        {
            using (StreamReader r = new StreamReader(filenameForTakenBooks))
            {
                string json = r.ReadToEnd();
                this.takenBooksLists = JsonConvert.DeserializeObject<List<TakenBooksList>>(json);
            }
        }
        public void overWriteJsonBooks()
        {
            File.WriteAllText(filenameForBooks, JsonConvert.SerializeObject(books, Formatting.Indented));
        }
        public void overWriteJsonTakenBooks()
        {
            File.WriteAllText(filenameForTakenBooks, JsonConvert.SerializeObject(takenBooksLists, Formatting.Indented));
        }

        public void addBook(Books book)
        {
            this.books.Add(book);     
        }
        public void deleteBook(int index)
        {
            books[index].isAvailable = false;
        }
        public void listBooks(string filter, string text)
        {
            
            List<Books> filteredBooks = new List<Books>();
            switch (filter.ToLower())
            {
                case "name":
                    filteredBooks = books.Where(x => x.book.name.ToLower() == text.ToLower() && x.isAvailable == true).ToList();
                    break;
                case "author":
                    filteredBooks = books.Where(x => x.book.author.ToLower() == text.ToLower() && x.isAvailable == true).ToList();
                    break;
                case "category":
                    filteredBooks = books.Where(x => x.book.category.ToLower() == text.ToLower() && x.isAvailable == true).ToList();
                    break;
                case "language":
                    filteredBooks = books.Where(x => x.book.language.ToLower() == text.ToLower() && x.isAvailable == true).ToList();
                    break;
                case "publication-date":
                    filteredBooks = books.Where(x => x.book.publicationDate.ToLower() == text.ToLower() && x.isAvailable == true).ToList();
                    break;
                case "isbn":
                    filteredBooks = books.Where(x => x.book.ISBN.ToLower() == text.ToLower() && x.isAvailable == true).ToList();
                    break;
                default:
                    Console.WriteLine("Incorrect filter");
                    break;
            }
            if(filteredBooks.Count != 0)
                foreach(var f in filteredBooks)
                {
                    Console.WriteLine(f.ToString());
                }
            else
            {
                Console.WriteLine("No books found by your filter text");
            }

        }
        public void takeBook(TakenBooksList takenbook)
        {
            this.takenBooksLists.Add(takenbook);
        }
        public override string ToString()
        {
            return string.Join("\n", books);
        }
        public string returnBookISBN(int index)
        {
            return books[index].book.ISBN;
        }
        public bool checkIfBookExist(string name, string author)
        {
            if (books.Any(a => a.book.name.ToLower() == name.ToLower() && a.book.author.ToLower() == author.ToLower()))
                return true;
            
            return false;
        }
        public bool checkBookPerPerson(string name)
        {
            int n = 0;
            foreach(var t in takenBooksLists)
                if(t.who.ToLower() == name.ToLower())
                    n++;

            if (n == 3)
                return true;
            return false;
        }

        public bool checkIfPersonHasBook(string isbn, string who)
        {
            if (takenBooksLists.Any(x => x.who.ToLower() == who.ToLower() && x.ISBN == isbn))
                return true;
            return false;
        }
        public void reduceBookNumber(int index)
        {
            books[index].count--;
        }
        public bool checkBookBalance(int index)
        {
            if (books[index].count != 0 && books[index].isAvailable == true)
                return true;
            return false;
        }
        public bool checkIfLate(string isbn)
        {
            if (takenBooksLists.Any(a => a.endDate > DateTime.Now && a.ISBN == isbn))
                return true;
            return false;
        }
        public void removeFromTakenList(string isbn, string who)
        {
            takenBooksLists.RemoveAll(x => x.who.ToLower() == who.ToLower() && x.ISBN == isbn);
        }

        public void addBookNumber(int index)
        {
            books[index].count++;
        }
        public bool checkIfBookIsAvailable(int index)
        {
            if(books[index].isAvailable == true)
                return true;
            return false;
        }
        public void increaseBookCount(int index, int count)
        {
            books[index].count += count;
        }
        public void makeBookAvailable(int index)
        {
            books[index].isAvailable = true;
        }
        public int returnBookIndex(string name, string author)
        {
            return books.FindIndex(x => x.book.name.ToLower() == name.ToLower() && x.book.author.ToLower() == author.ToLower());
        }
        public bool checkMultipleISBN(string isbn)
        {
            int n = 0;
            if (books.Any(x => x.book.ISBN == isbn))
                n++;
            if (n > 1)
                return true;
            return false;
        }

    }
}