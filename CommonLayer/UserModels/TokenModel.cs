using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.UserModels
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserRoleName { get; set; }
        public string FullName { get; set; }
        public int UserRoleId { get; set; }

    }
}
