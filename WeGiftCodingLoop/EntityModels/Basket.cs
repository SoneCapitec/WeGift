using System.Collections.Generic;
using WeGift.CodingLoop.Api.DomainModels;

namespace WeGift.CodingLoop.Api.EntityModels
{
    public class Basket: BaseEntity
    {
        public Customer Customer { get; set; }
        public List<GiftCard> GiftCards { get;set; }

        public double Total
        {
            get; set;
        }

    }
}
