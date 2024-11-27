using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class Bundle : Entity
    {
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public long AuthorId { get; private set; }
        public BundleStatus Status { get; private set; } = BundleStatus.Draft;

        private readonly List<long> _bundleItems = new List<long>();
        public IReadOnlyCollection<long> BundleItems => _bundleItems.AsReadOnly();
        public Bundle() { }

        public Bundle(string name, Money price, long authorId, IEnumerable<long> bundleItems)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty or whitespace.", nameof(name));
            Name = name;

            Price = price ?? throw new ArgumentNullException(nameof(price));
            if (price.Amount < 0)
                throw new ArgumentException("Amount can't be negativ.", nameof(price));
            Price = price;

            AuthorId = authorId;

            if (bundleItems == null || bundleItems.Count() < 2)
                throw new ArgumentException("You need to add at least 2 items to bundle.", nameof(bundleItems));

            _bundleItems.AddRange(bundleItems);
        }


        public void EditName(long authorId, string newName)
        {
            if (this.AuthorId != authorId)
                throw new InvalidOperationException("You can only edit bundle that already exists and that you created.");
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentNullException(nameof(newName), "Name cannot be empty.");

            Name = newName;
        }

        public void EditPrice(long authorId, Money newPrice)
        {
            if (this.AuthorId != authorId)
                throw new InvalidOperationException("You can only edit a bundle that already exists and that you created.");

            if (newPrice == null || newPrice.Amount <= 0)
                throw new ArgumentException("Price must be positive and valid.", nameof(newPrice));

            Price = newPrice;
        }

        public void ChangeStatus(long authorId, BundleStatus newStatus)
        {
            if (this.AuthorId != authorId)
                throw new InvalidOperationException("You can only change the status of a bundle you created.");

            Status = newStatus;
        }

        public void AddBundleItemOrRemoveIfAlreadyExists(long authorId, long bundleItemId)
        {
            if (this.AuthorId != authorId)
                throw new InvalidOperationException("You can only add items to bundle that already exists and if you are author of thet bundle.");
            
            if (!BundleItems.Contains(bundleItemId))
                _bundleItems.Add(bundleItemId);
            else
                _bundleItems.Remove(bundleItemId);

            if (_bundleItems.Count() < 2)
                throw new ArgumentException("You need to add at least 2 items to bundle.", nameof(_bundleItems));
        }
    }
}


