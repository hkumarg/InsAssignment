using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCWEF.Models
{
    public class ContractModel
    {
        public int ContractID { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string Dateofbirth { get; set; }
        public string SaleDate { get; set; }
        public string CoveragePlan { get; set; }
        public double NetPrice { get; set; }

    }
}
