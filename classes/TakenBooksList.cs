using System;
namespace Visma_Internship_Task
{
    public class TakenBooksList
    {
        public string ISBN { get; set; }
        public string who { get; set; }
        public int days { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public TakenBooksList(string ISBN, string who, int days)
        {
            this.ISBN = ISBN;
            this.who = who;
            this.days = days;
            this.startDate = returnTodayDate();
            this.endDate = returnEndDate(days);

        }
        private DateTime returnTodayDate()
        {
            return DateTime.Now;
        }
        private DateTime returnEndDate(int days)
        {
            return DateTime.Now.AddDays(days);
        }
    }
}