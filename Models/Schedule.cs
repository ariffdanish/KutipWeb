namespace KutipWeb.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int BinId { get; set; }
        public int AssignedCollectorId { get; set; }
        public Collector AssignedCollector { get; set; } = new Collector();
        public Day Day { get; set; }
        public TimeOnly TimePickup { get; set; }
    }

    public enum Day
    {
        Monday, Tuesday, Wednesday, Friday, Saturday, Sunday
    }
}
