using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//We are going to use EntityFramwork to help maintain DB tables that always match to our Entities classes
//So when I modify the definition of the calss, the DB table for it iwll update as well (code-first migrations)
namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash {get; set; }
        public byte[] PasswordSalt {get; set;}
    }
}