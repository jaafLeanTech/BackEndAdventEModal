using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModel.Entities.DTOs
{
    public class UserPasswordChangeDto : UserLoginRequestDto
    {
        public string NewPassword { get; set; }
    }
}
