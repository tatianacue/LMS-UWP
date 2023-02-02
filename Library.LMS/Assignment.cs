using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS
{
    public class Assignment
    {
        public Assignment() { }

        private string name;
        private string description;
        private int totalpoints;
        private string duedate;

        public string id { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int TotalAvailablePoints
        {
            get { return totalpoints; }
            set { totalpoints = value; }
        }
        public string DueDate
        {
            get { return duedate; }
            set { duedate = value; }
        }
    }
}
