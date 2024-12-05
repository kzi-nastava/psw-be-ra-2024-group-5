using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;
public class PaymentRecord : Entity {
    public long TouristId { get; private set; }
    public long ItemId { get; private set; }
    public Money Price { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public bool IsBundle { get; private set; }

    public PaymentRecord() { }
    public PaymentRecord(long touristId, long itemId, Money price, bool isBundle) {
        TouristId = touristId;
        ItemId = itemId;
        Price = price;
        PurchaseDate = DateTime.UtcNow;
        IsBundle = isBundle;
    }
}
