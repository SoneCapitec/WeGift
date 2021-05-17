using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeGift.CodingLoop.Api.DomainModels;
using WeGift.CodingLoop.Api.EntityModels;

namespace WeGift.CodingLoop.Api.Controllers
{
    public class GiftCardController : Controller
    {
        private List<GiftCardType> _defaultGiftCards;

        public GiftCardController()
        {
            //Setup
            _defaultGiftCards = new List<GiftCardType>
            {
                new GiftCardType
                {
                    Id = 1,
                    DiscountPercentage = 10.00,
                    Type = "Amazon"
                },
                new GiftCardType
                {
                    Id = 2,
                    DiscountPercentage = 5.00,
                    Type = "E-bay"
                },
                new GiftCardType
                {
                    Id = 3,
                    DiscountPercentage = 4.00,
                    Type = "Tesco"
                }
            };
        }

        public Basket UpdateBasket(Basket basket)
        {
            double total = 0.00;

            basket.GiftCards.ForEach(c =>
            {
                total += CalculateDiscoutedTotal(c);
            });
            basket.Total = total;
            return basket;
        }

        private double CalculateDiscoutedTotal(GiftCard giftCard)
        {
            var giftCardType = _defaultGiftCards.Find(c => c.Id == giftCard.GiftCardTypeId);
            
            return giftCardType.DiscountPercentage / 100 * giftCard.FaceValue;
        }
    }
}
