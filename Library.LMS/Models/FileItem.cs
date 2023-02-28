using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class FileItem : ContentItem //derived
    {
        public FileItem() 
        {
            Name = string.Empty;
            Description= string.Empty;
            Id = $"F{LastId++}";
        }
        private static int LastId = 1;
        public string FilePath { get; set; }

        public override string DisplayAll()
        {
            return $"{Name} - {Description}\n" +
                $"{FilePath}";
        }
    }
}
