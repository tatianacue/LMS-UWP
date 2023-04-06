﻿using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class AssignmentItemViewModel
    {
        public AssignmentItemViewModel(List<ContentItem> content, ObservableCollection<Assignment> a)
        {
            Content = content;
            AssignmentItem = new AssignmentItem();
            Assignments = a;
        }
        public AssignmentItem AssignmentItem { get; set; }
        public List<ContentItem> Content { get; set; }
        public ObservableCollection<Assignment> Assignments { get; set; }
        public Assignment SelectedAssignment { get; set; }
        public string Name
        {
            set  {   AssignmentItem.Name = value;  }
        }
        public string Description
        {
            set
            {  AssignmentItem.Description = value;  }
        }
        public void AddItem()
        {
            AssignmentItem.Assignment = SelectedAssignment;
            Content.Add(AssignmentItem);
        }
    }
}