namespace MVC01.Entities
{
    public class Session : BaseEntity
    {
        public int Capacity { get; set; }

        public string Description { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TrainerId { get; set; }

        public Trainer Trainer { get; set; } = default!;
    }

}
