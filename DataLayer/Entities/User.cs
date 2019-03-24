using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int refUserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Token { get; set; }
    }
}
