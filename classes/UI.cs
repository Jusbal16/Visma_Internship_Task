using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Visma_Internship_Task
{
    public class UI
    {
        public string uiText { get; set; }
        public UI()
        {
           
        }
        public void loadUItext(string filename)
        {
            this.uiText = File.ReadAllText(filename);
        }
        public void loadUI()
        {
            Console.WriteLine(uiText);
        }
        public List<string> returnCommand()
        {
            var command = Console.ReadLine();
            List<string> args = Regex.Split(command, " (?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").Select(x => x.Replace("\"", "")).ToList();
            return args;
        }

            


    }
}
