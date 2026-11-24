using MVC01.Entities.Enums;
using System.Net;

namespace MVC01.Entities
{
    public class GymUser :BaseEntity
    {
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public Address Address { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; } = default!;
    }
}
