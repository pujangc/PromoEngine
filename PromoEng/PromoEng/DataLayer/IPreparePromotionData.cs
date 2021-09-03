using PromoEng.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoEng.DataLayer
{
    public interface IPreparePromotionData
    {
         List<SKUDetails> GetActiveSKUs();
         List<Promotion> GetActivePromotions();
        int GetSKUId(String SKUName);
        Dictionary<String, int> GetCheckoutCart();
    }
}
