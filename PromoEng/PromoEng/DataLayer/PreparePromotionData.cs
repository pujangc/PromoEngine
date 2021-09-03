using PromoEng.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoEng.DataLayer
{
    public class PreparePromotionData:IPreparePromotionData
    {
        List<SKUDetails> _lstSKUs = null;
        List<Promotion> _lstPromotion = null;
        List<String> _lstCart = null;
        public PreparePromotionData()
        {
            _lstSKUs = new List<SKUDetails>();
            _lstPromotion = new List<Promotion>();
            _lstCart = new List<String>();
        }
        public List<SKUDetails> GetActiveSKUs()
        {
            var _lstSKUs = new List<SKUDetails>{
                {new SKUDetails{SKUId=1, SKUType="A", Price= 50} },
                {new SKUDetails{SKUId=2, SKUType="B", Price= 30} },
                {new SKUDetails{SKUId=3, SKUType="C", Price= 20} },
                {new SKUDetails{SKUId=4, SKUType="D", Price= 15} }
            };
            return _lstSKUs;
        }
        public List<Promotion> GetActivePromotions()
        {
            var _lstPromotion = new List<Promotion>();
            _lstPromotion.Add(new Promotion
            {
                PromotionId = 1,
                PromotionEntries = new List<PromotionEntry> {
                    new PromotionEntry { NoOfSKUs = 3, SkuId = GetSKUId("A") } },
                Price = 130
            });
            _lstPromotion.Add(new Promotion
            {
                PromotionId = 1,
                PromotionEntries = new List<PromotionEntry> {
                    new PromotionEntry { NoOfSKUs = 2, SkuId = GetSKUId("B") } },
                Price = 45
            });
            _lstPromotion.Add(new Promotion
            {
                PromotionId = 1,
                PromotionEntries = new List<PromotionEntry> {
                    new PromotionEntry { NoOfSKUs = 1, SkuId = GetSKUId("C") },
                    new PromotionEntry { NoOfSKUs = 1, SkuId = GetSKUId("D") }
                },
                Price = 30
            });
            return _lstPromotion;
        }
        public int GetSKUId(String SKUName)
        {
            var skuId = int.Parse(GetActiveSKUs().Where(x => x.SKUType == SKUName).Select(x=>x.SKUId).FirstOrDefault().ToString());
            return skuId;
        }
        public Dictionary<String, int> GetCheckoutCart()
        {
            String continueFurther = String.Empty;
             _lstCart.AddRange(new List<String> { "A", "A", "A", "A", "A", "B", "B", "B", "B", "B","C" }); // Test sceneio 1
             //_lstCart.AddRange(new List<String> { "A", "B", "C" }); // Test Scenerio 2
            //_lstCart.AddRange(new List<String> { "A", "A", "A", "B", "B", "B", "B", "B", "C","D"}); // Test Scenerio 3
            //**************************I/O********************
            //do
            //{
            //    Console.WriteLine("Please enter an SKU from the available List");
            //    Console.WriteLine("A, B, C, D");
            //    string userSku = Console.ReadLine().ToUpper();
            //    if (GetActiveSKUs().Where(x => x.SKUType.Contains(userSku)).ToList().Count < 1)
            //    {
            //        Console.WriteLine("Invalid Entry! Try again..");
            //        break;
            //    }
            //    else lstUserSkus.Add(userSku);
            //    Console.WriteLine("Do you want to continue? Y/N");
            //    continueFurther = Console.ReadLine().ToUpper();
            //    if (continueFurther.Contains("N"))
            //    {
            //        break;
            //    }
            //    else if(!(continueFurther.Contains("Y") || continueFurther.Contains("N")))
            //    {
            //        Console.WriteLine("Invalid Entry! Try again..");
            //    }

            //} while (continueFurther == "Y");
            //lstUserSkus.ForEach(x =>
            //{
            //    _lstCart.Add(new SKUDetails { SKUId = GetSKUId(x), SKUType = x });
            //});
            return PrepareCartData();
        }

        private Dictionary<String, int> PrepareCartData()
        {
            int skuCount = 0; String sku = String.Empty;
            var _dictCart = new Dictionary<String, int>();
            _lstCart.ForEach(x =>
            { 
                if(string.Equals(sku,x))
                {
                    _dictCart[x] = skuCount;
                }
                else
                {
                    _dictCart.Add(x, 1);
                    skuCount = 1;
                }
                skuCount = skuCount + 1;
                sku = x;
            });
            return _dictCart;
        }
    }
}
