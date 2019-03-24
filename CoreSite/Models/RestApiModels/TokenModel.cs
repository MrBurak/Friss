using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSite.Models.RestApiModels
{
    [Serializable]
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserRoleName { get; set; }
        public string FullName { get; set; }
        public int UserRoleId { get; set; }
    }
}
