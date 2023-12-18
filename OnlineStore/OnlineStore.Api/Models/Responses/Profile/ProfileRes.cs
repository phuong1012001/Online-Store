using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Api.Models.Responses.ProfileRes
{
    public class ProfileRes
    {
        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Civilianld { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                      ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public int Role { get; set; }
    }
}
