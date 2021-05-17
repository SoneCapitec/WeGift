using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WeGift.CodingLoop.Api.Controllers;
using WeGift.CodingLoop.Api.DomainModels;
using WeGift.CodingLoop.Api.EntityModels;

namespace WeGift.CodingLoop.Api.Tests
{
    [TestClass]
    public class GiftCardControllerTests
    {
        private List<GiftCardType> _defaultGiftCards;

        [TestInitialize]
        public void Initialize()
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

        [TestMethod]
        public void TestUpdateBasket()
        {
            //Arrange
            var baskets = new List<Basket>
            {
                new Basket
                {
                    Id = 1,
                    Customer = new Customer
                    {
                        EmailAddress = "test.test@test.com",
                    },
                    GiftCards = new List<GiftCard>
                    {
                        new GiftCard
                        {
                            FaceValue = 15.00,
                            GiftCardTypeId = 1
                        },
                        new GiftCard
                        {
                            FaceValue = 10.00,
                            GiftCardTypeId = 2
                        },
                        new GiftCard
                        {
                            FaceValue = 50.00,
                            GiftCardTypeId = 1
                        }
                    },
                },
                    new Basket
                {
                        Id =2,
                    Customer = new Customer
                    {
                        EmailAddress = "test2.test@test.com",
                    },
                    GiftCards = new List<GiftCard>
                    {
                        new GiftCard
                        {
                            FaceValue = 15.00,
                            GiftCardTypeId = 3
                        },
                        new GiftCard
                        {
                            FaceValue = 10.00,
                            GiftCardTypeId = 3
                        },
                        new GiftCard
                        {
                            FaceValue = 50.00,
                            GiftCardTypeId = 3
                        }
                    }
                }
            };


            //Act
            var giftCardController = new GiftCardController();

            var updatedBaskets = baskets.ConvertAll(basket => giftCardController.UpdateBasket(basket));

            var basketsBeforeIncreases = updatedBaskets.ConvertAll(basket => basket.Total).ToArray();

            //Assert
            foreach (var basket in updatedBaskets)
            {
                double total = 0.00;

                basket.GiftCards.ForEach(c =>
                {
                    total += CalculateDiscoutedTotal(c);
                });
                Assert.AreEqual(basket.Total, total);
            }




            baskets.ForEach(b => {
                b.GiftCards.ForEach(c => {
                    c.FaceValue += 10.00;
                    }
                );
            });

            var updatedBasketsAfterFaceValueIncreases = baskets.ConvertAll(basket => giftCardController.UpdateBasket(basket));



            //Assert
            foreach (var basket in updatedBasketsAfterFaceValueIncreases)
            {
                double total = 0.00;

                basket.GiftCards.ForEach(c =>
                {
                    total += CalculateDiscoutedTotal(c);
                });
                Assert.AreEqual(basket.Total, total);
            }

            for (int i = 0; i < updatedBasketsAfterFaceValueIncreases.Count; i++)
            {
                Assert.IsTrue(updatedBasketsAfterFaceValueIncreases[i].Total >= basketsBeforeIncreases[i]);
            }
        }

        private double CalculateDiscoutedTotal(GiftCard giftCard)
        {
            var giftCardType = _defaultGiftCards.Find(c => c.Id == giftCard.GiftCardTypeId);


            return giftCardType.DiscountPercentage / 100 * giftCard.FaceValue;
        }
    }
}


/*
 The system should be demo-able via unit tests (preferably) or the console.
The system does not require persistence.
The system should have more than one customer.
There should be some default gift cards which you can create using static data.
 -- Open denominations - some discount
 -- Type - eg Amazon/ E-Bay
Multiple customers should be able to add multiple types of gift cards to their basket.
The prices of gift cards should be able to be updated.
Gift cards have a face value and a price which is discounted by the brand. The customer pays the discounted price. The discount can be changed at any time.
Calculate customers basket total price. Customers’ basket value should reflect the updated prices.
 */