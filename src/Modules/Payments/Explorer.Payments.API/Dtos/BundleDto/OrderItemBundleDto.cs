namespace Explorer.Payments.API.Dtos.BundleDto;

public class OrderItemBundleDto {
    public long? Id { get; set; }
    public long BundleId { get; set; }
    public string BundleName { get; set; }
    public ShoppingMoneyDto Price { get; set; }
    public OrderItemBundleDto() { }
    public OrderItemBundleDto(long? id, long bundleId, string bundleName, ShoppingMoneyDto price) {
        Id = id;
        BundleId = bundleId;
        BundleName = bundleName;
        Price = price;
    }
}