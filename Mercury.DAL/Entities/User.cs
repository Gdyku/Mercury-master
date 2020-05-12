using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Marker> Markers { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsNewsletterEnabled { get; set; }
        public bool IsRecommendedReadingEnabled { get; set; }
    }
}
