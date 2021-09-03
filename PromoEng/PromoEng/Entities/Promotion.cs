using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoEng.Entities
{
    /// <summary>
    /// Promotion Entity is input to the Promotion Engine
    /// </summary>
    public class Promotion
    {
        public int PromotionId { get; set; }
        public List<PromotionEntry> PromotionEntries { get; set; }
        public float Price { get; set; }
    }
    public class PromotionEntry 
    {
        public int PromotionEntryId { get; set; }
        public int NoOfSKUs { get; set; }   
        public int SkuId { get; set; }
    }
}
