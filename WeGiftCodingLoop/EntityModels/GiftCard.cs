using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeGift.CodingLoop.Api.DomainModels;

namespace WeGift.CodingLoop.Api.EntityModels
{
    public class GiftCard: BaseEntity
    {
        public int GiftCardTypeId { get; set; }
        public GiftCardType GiftCartType { get; set; }
        public double FaceValue { get; set; }

    }
}
