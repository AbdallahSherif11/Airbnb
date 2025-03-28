using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.AccountDTOs
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public string NationalId { get; set; }
        public string IsAgreed { get; set; }
        
    }
}
