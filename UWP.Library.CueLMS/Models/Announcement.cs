﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Announcement
    {
        public Announcement() 
        {
            Title= string.Empty;
            Text= string.Empty;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; } //body of announcement
        public override string ToString()
        {
            return $"[{Id}] - {Title}";
        }
        public string Display => $"{Title}";
    }
}
