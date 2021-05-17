using WeGift.CodingLoop.Api.EntityModels;

namespace WeGift.CodingLoop.Api.DomainModels
{
    public class GiftCardType: BaseEntity
    {
        public string Type { get; set; }

        public double DiscountPercentage { get; set; }

    }
}
