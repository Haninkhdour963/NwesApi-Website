using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public string ImageUrl { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}