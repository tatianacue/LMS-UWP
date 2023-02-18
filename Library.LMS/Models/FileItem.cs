using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class FileItem : ContentItem //derived
    {
        public FileItem() { }
        public string FilePath { get; set; }
    }
}
