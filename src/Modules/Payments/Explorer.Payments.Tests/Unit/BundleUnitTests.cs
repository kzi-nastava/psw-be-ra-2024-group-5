using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.API.Enum;
using Xunit;

namespace Explorer.Payments.Tests.Unit
{
    public class BundleUnitTests
    {
        [Theory]
        [InlineData("Valid Bundle", 100, 1, new long[] { 101, 102 }, true)]
        [InlineData("", 100, 1, new long[] { 101, 102 }, false)] // Invalid name
        [InlineData("Valid Bundle", -50, 1, new long[] { 101, 102 }, false)] // Negative price
        [InlineData("Valid Bundle", 100, 1, new long[] { 101 }, false)] // Less than 2 items
        public void Constructor_Test(string name, double priceAmount, long authorId, long[] bundleItems, bool shouldSucceed)
        {
            // Arrange
            Money price = new Money(priceAmount, ShoppingCurrency.AC);

            // Act & Assert
            if (shouldSucceed)
            {
                var bundle = new Bundle(name, price, authorId, bundleItems);
                Assert.Equal(name, bundle.Name);
                Assert.Equal(price, bundle.Price);
                Assert.Equal(authorId, bundle.AuthorId);
                Assert.Equal(BundleStatus.Draft, bundle.Status);
                Assert.Equal(bundleItems.Length, bundle.BundleItems.Count);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    var bundle = new Bundle(name, price, authorId, bundleItems);
                });
            }
        }

        [Theory]
        [InlineData("New Valid Name", 1, true)]
        [InlineData("", 1, false)]          // Invalid name
        [InlineData("Valid Name", 999, false)] // Invalid authorId
        public void EditName_Test(string newName, long authorId, bool shouldSucceed)
        {
            // Arrange
            var bundle = CreateTestBundle();

            // Act & Assert
            if (shouldSucceed)
            {
                bundle.EditName(authorId, newName);
                Assert.Equal(newName, bundle.Name);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    bundle.EditName(authorId, newName);
                });
            }
        }

        [Theory]
        [InlineData(150, 1, true)]
        [InlineData(-10, 1, false)]    // Negative price
        [InlineData(150, 999, false)]  // Invalid authorId
        public void EditPrice_Test(double newPriceAmount, long authorId, bool shouldSucceed)
        {
            // Arrange
            var bundle = CreateTestBundle();
            Money newPrice = new Money(newPriceAmount, ShoppingCurrency.AC);

            // Act & Assert
            if (shouldSucceed)
            {
                bundle.EditPrice(authorId, newPrice);
                Assert.Equal(newPrice, bundle.Price);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    bundle.EditPrice(authorId, newPrice);
                });
            }
        }

        [Theory]
        [InlineData(BundleStatus.Published, 1, true)]
        [InlineData(BundleStatus.Archive, 999, false)] // Invalid authorId
        public void ChangeStatus_Test(BundleStatus newStatus, long authorId, bool shouldSucceed)
        {
            // Arrange
            var bundle = CreateTestBundle();

            // Act & Assert
            if (shouldSucceed)
            {
                bundle.ChangeStatus(authorId, newStatus);
                Assert.Equal(newStatus, bundle.Status);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    bundle.ChangeStatus(authorId, newStatus);
                });
            }
        }

        [Theory]
        [InlineData(103, 1, true)]    // Add new item
        [InlineData(101, 1, true)]    // Remove existing item
        [InlineData(103, 999, false)] // Invalid authorId
        public void AddOrRemoveBundleItem_Test(long itemId, long authorId, bool shouldSucceed)
        {
            // Arrange
            var bundle = CreateTestBundle();

            // Act & Assert
            if (shouldSucceed)
            {
                int initialCount = bundle.BundleItems.Count;
                bool itemExists = bundle.BundleItems.Contains(itemId);

                bundle.AddBundleItemOrRemoveIfAlreadyExists(authorId, itemId);

                if (itemExists)
                {
                    Assert.DoesNotContain(itemId, bundle.BundleItems);
                    Assert.Equal(initialCount - 1, bundle.BundleItems.Count);
                }
                else
                {
                    Assert.Contains(itemId, bundle.BundleItems);
                    Assert.Equal(initialCount + 1, bundle.BundleItems.Count);
                }
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    bundle.AddBundleItemOrRemoveIfAlreadyExists(authorId, itemId);
                });
            }
        }

        private static Bundle CreateTestBundle()
        {
            return new Bundle("Test Bundle", new Money(100, ShoppingCurrency.AC), 1, new List<long> { 101, 102, 104 });
        }
    }
}
