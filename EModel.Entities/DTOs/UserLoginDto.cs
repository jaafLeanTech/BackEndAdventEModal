using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EModel.Entities.DTOs
{
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public int UserId { get; set; }
    }
}
