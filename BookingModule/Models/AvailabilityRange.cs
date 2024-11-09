namespace BookingModule.Models
{
    public class AvailabilityRange(DateTime dateFrom, DateTime dateTo, int roomAvailability)
    {
        public DateTime DateFrom { get; set; } = dateFrom;
        public DateTime DateTo { get; set; } = dateTo;
        public int RoomAvailability { get; set; } = roomAvailability;
    }
}
