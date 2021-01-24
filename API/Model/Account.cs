using System;
using System.Collections.Generic;

#nullable disable

namespace API.Model
{
    public partial class Account
    {
        public Account()
        {
            Bookings = new HashSet<Booking>();
            Profiles = new HashSet<Profile>();
        }

        public string AccountId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
