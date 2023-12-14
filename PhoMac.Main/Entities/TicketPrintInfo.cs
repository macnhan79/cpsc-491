using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Main.Entities
{
    class TicketPrintInfo
    {
        public int TicketID_Root { get; set; }
        public int TableID { get; set; }
        public int DTicketNum { get; set; }
        public string TableName { get; set; }
        public string EmpName { get; set; }
        public string CustomerName { get; set; }
        public DateTime Time { get; set; }
        public bool TicketTakeOut { get; set; }
        public List<Product> ListProduct { get; set; }
        public int CountItemDonePrint { get; set; }
        public int CountTotalItemPrint { get; set; }
        public string Payment { get; set; }
    }
}
