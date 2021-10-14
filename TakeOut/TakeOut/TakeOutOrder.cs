using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormManagementApp
{
    class TakeOutOrder
    {
        public long StudentID { set; get; }
        public string Name { set; get; }
        public long PhoneNumber { set; get; }
        public long TakeOutOrderID { set; get; }
        public string Date { set; get; }
        public string State { set; get; }
    }
}
