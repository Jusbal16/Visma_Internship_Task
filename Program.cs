using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Visma_Internship_Task
{
    class Program
    {
        static void Main(string[] args)
        {

            string booksFile = "Books.json";
            string takenBooksFile = "TakenBooksList.json";
            string UiFile = "UI.txt";

            string foldername = "data";

            string thisFile = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
            string path1 = Path.GetDirectoryName(thisFile);
            path1 += Path.DirectorySeparatorChar + foldername + Path.DirectorySeparatorChar + booksFile;

            string path2 = Path.GetDirectoryName(thisFile);
            path2 += Path.DirectorySeparatorChar + foldername + Path.DirectorySeparatorChar + takenBooksFile;

            string path3 = Path.GetDirectoryName(thisFile);
            path3 += Path.DirectorySeparatorChar + foldername + Path.DirectorySeparatorChar + UiFile;

           
            var manager = new Manager(path1, path2, path3);
            manager.MainProgram();
        }
    }
}