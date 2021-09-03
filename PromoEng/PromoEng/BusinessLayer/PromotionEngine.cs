using PromoEng.DataLayer;
using PromoEng.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoEng.BusinessLayer
{
    public class PromotionEngine: IPromotionEngine
    {
        List<SKUDetails> _lstSkuDetails = null;
        List<Promotion> _lstPromotion = null;
        Dictionary<String, int> _dictCart = null;
        IPreparePromotionData _preparePromotionData;
        public PromotionEngine(IPreparePromotionData preparePromotionData)
        {
            _preparePromotionData = preparePromotionData;
            _lstSkuDetails = _preparePromotionData.GetActiveSKUs();
            _lstPromotion = _preparePromotionData.GetActivePromotions();
            _dictCart = _preparePromotionData.GetCheckoutCart();
        }

        public double CalculateSkuTotal()
        {
            return CalculateTotal(_lstSkuDetails, _lstPromotion, _dictCart);
        }
        private double CalculateTotal(List<SKUDetails> lstSkuDetails, List<Promotion> lstPromotion, Dictionary<String, int> dictCart)
        {
            int noOfPromoSkus = 0, skuR = 0, skuQ = 0, allmatched = 0; double totalSum = 0, skuSum = 0, priceDeductible = 0, skuSumFinal = 0, skuSumPrev=0;
            foreach (var item in dictCart)
            {
                lstPromotion.ForEach(x =>
                {
                    if(x.PromotionEntries.Any(y=>y.SkuId == _preparePromotionData.GetSKUId(item.Key)))
                    {
                        noOfPromoSkus = int.Parse(x.PromotionEntries.Select(y => y.NoOfSKUs).FirstOrDefault().ToString());
                        if (!(x.PromotionEntries.Count > 1)){
                            if (item.Value >= noOfPromoSkus)
                            {
                                skuQ = noOfPromoSkus != 0 ? item.Value / noOfPromoSkus : 0;
                                skuR = noOfPromoSkus != 0 ? item.Value % noOfPromoSkus : 0;
                                skuSum = skuQ * int.Parse(x.Price.ToString()) + skuR * int.Parse(lstSkuDetails.Where(z => z.SKUId == _preparePromotionData.GetSKUId(item.Key)).Select(y => y.Price).FirstOrDefault().ToString());
                            }
                            else
                            {
                                skuSum = item.Value * int.Parse(lstSkuDetails.Where(z => z.SKUId == _preparePromotionData.GetSKUId(item.Key)).Select(y => y.Price).FirstOrDefault().ToString());
                            }
                        }
                        else
                        {
                            x.PromotionEntries.ForEach(p =>
                            {
                                
                                if(p.SkuId == _preparePromotionData.GetSKUId(item.Key))
                                {
                                    allmatched = ++allmatched;
                                    skuSum = item.Value * int.Parse(lstSkuDetails.Where(z => z.SKUId == _preparePromotionData.GetSKUId(item.Key)).Select(y => y.Price).FirstOrDefault().ToString());
                                    skuSumPrev = skuSumPrev + skuSum;
                                    priceDeductible = skuSum;
                                }
                            });
                        }
                        if (allmatched == x.PromotionEntries.Count)
                        {
                             skuSum = int.Parse(x.Price.ToString()) - (skuSumPrev - priceDeductible);
                        }
                        totalSum = totalSum + skuSum;
                    }
                });
            }
            return totalSum ;
        }
       
    }
}
