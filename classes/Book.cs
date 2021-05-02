using System;
namespace Visma_Internship_Task
{
    public class Book
    {
        public string name { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string publicationDate { get; set; }
        public string ISBN { get; set; }

        public Book(string name, string author, string category, string language, string publicationDate, string ISBN)
        {
            this.name = name;
            this.author = author;
            this.category = category;
            this.language = language;
            this.publicationDate = publicationDate;
            this.ISBN = ISBN;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", name, author, category, language, publicationDate, ISBN);
        }
    }
}