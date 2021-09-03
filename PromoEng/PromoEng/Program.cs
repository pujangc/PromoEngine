using PromoEng.BusinessLayer;
using PromoEng.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoEng
{
    class Program
    {

        static void Main(string[] args)
        {
            IPreparePromotionData preparePromotionData = new PreparePromotionData();
            IPromotionEngine promoEng = new PromotionEngine(preparePromotionData);
            promoEng.CalculateSkuTotal();
        }
    }
}
