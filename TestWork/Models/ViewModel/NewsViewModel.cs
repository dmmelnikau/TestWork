using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models.ViewModel
{
    public class NewsViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<News> News { get; set; }
    }
}
