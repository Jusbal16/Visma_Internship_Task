using System;
namespace Visma_Internship_Task
{
    public class Books
    {
        public Book book { get; set; }
        public int count { get; set; }
        public bool isAvailable { get; set; }

        public Books(Book book, int count)
        {
            this.book = book;
            this.count = count;
            this.isAvailable = true;
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", book.ToString(), count.ToString());
        }
    }
}