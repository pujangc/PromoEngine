using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoEng.Entities
{
    /// <summary>
    /// SKU Unit Details
    /// </summary>
    public class SKUDetails
    {
        public int SKUId { get; set; }
        public String SKUType { get; set; }
        public float Price { get; set; }
        //public Dictionary<String, int> SKUList { get; set; }

    }
}
