using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htcustomer.service.Enums
{
    public enum TransactionStatus : int
    {
        NotFix=1,
        Fixed=2,
        CannotFix=3,
        Delivered=4
    }
}
