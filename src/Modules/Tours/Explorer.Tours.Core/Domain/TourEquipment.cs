namespace Explorer.Tours.Core.Domain;

public class TourEquipment
{
    public long TourId { get; set; }

    public long EquipmentId { get; set; }

    public TourEquipment(long tourId, long equipmentId)
    {
        TourId = tourId;
        EquipmentId = equipmentId;
    }
}
