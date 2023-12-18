using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Dtos.Auth
{
    public class RegisterResultDto
    {
        public bool Success { get; set; }

        public string? Error { get; set; }
    }
}
