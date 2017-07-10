using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TourDTO
    {
        public Int32 TourID { get; set; }
        public String FromCity { get; set; }
        public String ToCity { get; set; }
        public DateTime SubDeadLine { get; set; }
        public DateTime Departure { get; set; }
        public DateTime ReturnDate { get; set; }
        public Int32 Dues { get; set; }
    }
}
