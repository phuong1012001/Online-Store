using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Dtos.Auth
{
    public class ResultDto
    {
        public bool Success { get; set; }

        public string? ErrorCode { get; set; }
    }
}
