using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class FileItem : ContentItem //derived
    {
        public FileItem() 
        {
            Name = string.Empty;
            Description= string.Empty;
            Type = 0;
        }
        
        public override string DisplayAll()
        {
            return $"{Name} - {Description}\n" +
                $"{FilePath}";
        }
    }
}
