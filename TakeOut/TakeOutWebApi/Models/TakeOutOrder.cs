using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DormManagementApi.Models
{
    public class TakeOutOrder
    {

        public long StudentID { set; get; }
        public string Name { set; get; }
        public long PhoneNumber { set; get; }
        public long TakeOutOrderID { set; get; }
        public string Date{ set; get; }
        public string State { set; get; }
    }
}
