﻿using Library.LMS.Models.Grading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Assignment
    {
        public Assignment()
        {
        }

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalAvailablePoints { get; set; }
        public string DueDate { get; set; }
        public AssignmentGroup Group { get; set; }
        public int Id { get; set; }
        public override string ToString()
        {
            return $"[{Id}]({DueDate}) - {Name}";
        }
        public string Display => $"{Name}";
    }
}
