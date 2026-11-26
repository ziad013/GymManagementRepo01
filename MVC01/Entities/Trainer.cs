using MVC01.Entities.Enums;
using static System.Collections.Specialized.BitVector32;

namespace MVC01.Entities
{
    public class Trainer : GymUser
    {

        public Specialities Specialities { get; set; } = default!;

        public ICollection<Session> Sessions { get; set; } = default!;
    }
}
