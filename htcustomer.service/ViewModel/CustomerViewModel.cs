using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public long? Phone { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool? Disable { get; set; } 


    }
}
