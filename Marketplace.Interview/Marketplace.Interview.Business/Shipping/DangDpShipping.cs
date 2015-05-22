using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Interview.Business.Basket;

namespace Marketplace.Interview.Business.Shipping
{
    public class DangDpShipping : ShippingBase
    {
        public IEnumerable<RegionShippingCost> PerRegionCosts { get; set; }

        public override string GetDescription(LineItem lineItem, Basket.Basket basket)
        {
            return string.Format("DangDp Shipping to {0}", lineItem.DeliveryRegion);
        }

        public override decimal GetAmount(LineItem lineItem, Basket.Basket basket)
        {
            LineItem firstItem = basket.LineItems.FirstOrDefault(item => lineItem.SupplierId == item.SupplierId 
                && lineItem.DeliveryRegion == item.DeliveryRegion && item.Shipping.GetType() == this.GetType());
            
            var amount = (from c in PerRegionCosts
                          where c.DestinationRegion == lineItem.DeliveryRegion
                          select c.Amount).Single();

            if (!lineItem.Equals(firstItem))
            {
                amount -= 0.5m;
            }
            return amount;
        }
    }
}
