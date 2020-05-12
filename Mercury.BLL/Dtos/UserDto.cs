using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.BLL.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsNewsletterEnabled { get; set; }
        public bool IsRecommendedReadingEnabled { get; set; }
    }
}
