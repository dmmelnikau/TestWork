using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Models.ViewModel
{
    public class AdvertisementViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}
