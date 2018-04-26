using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineApp.Models
{
    class PendingCommand
    {
        public int PendingId { get; set; }

        public int status { get; set; }

        public int ProductMachineId { get; set; }

        public ProductMachine ProductMachine { get; set; }
    }
}
