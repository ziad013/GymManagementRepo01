namespace MVC01.Entities
{
    public class Member : GymUser
    {
        public string? Photo { get; set; }

        public ICollection<Membership> MemberSubscriptions { get; set; } = default!;
    }
}
