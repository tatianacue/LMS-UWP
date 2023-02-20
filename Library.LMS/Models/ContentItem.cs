﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class ContentItem //base class
    {
        public ContentItem() { }

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
        public virtual string DisplayAll()
        {
            return $"{Name} - {Description}";
        }

    }
}
