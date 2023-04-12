﻿using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System.Collections.Generic;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class AssignmentGroupViewModel
    {
        public AssignmentGroupViewModel(List<AssignmentGroup> list, Assignment a)
        {
            Groups = list;
            AssignmentGroup = new AssignmentGroup();
            assignment = a;
        }
        List<AssignmentGroup> Groups { get; set; }
        public AssignmentGroup AssignmentGroup { get; set; }
        private Assignment assignment { get; set; }
        public string Name 
        { 
            set { AssignmentGroup.Name = value; }
        }
        public int Weight 
        { 
            set { AssignmentGroup.Weight = value; }
        }
        public void Add()
        {
            AssignmentGroup.AddAssignment(assignment); //adds assignment to group
            Groups.Add(AssignmentGroup); //adds group to list of groups

        }
    }
}
