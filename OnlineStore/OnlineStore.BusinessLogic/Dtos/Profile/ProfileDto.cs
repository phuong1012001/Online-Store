using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Dtos.Profile
{
    public class ProfileDto
    {
        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Civilianld { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Role { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
