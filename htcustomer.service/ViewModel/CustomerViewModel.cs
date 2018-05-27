﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Name should not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Phone should not be empty")]
        [RegularExpression(@"(09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "Phone number was not in true format")]
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool? Disable { get; set; } 


    }
}
