using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProject.ASP.Models
{
    public class MovieIndex
    {
        public List<MovieModel> Movies { get; set; }
        public int Total { get; set; }
        public string KeyWord { get; set; }
        public int SectedCategory { get; set; }
    }
}
