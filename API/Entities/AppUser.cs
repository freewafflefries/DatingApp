using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;

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
        public DateTime DateOfBirth {get; set;}
        public string KnownAs {get; set;}
        public DateTime DateCreated {get; set;} = DateTime.Now;
        public DateTime LastActive {get; set;} = DateTime.Now;
        public string Gender {get; set;}
        public string Introduction {get; set;}
        public string LookingFor {get; set;}
        public string Interests {get; set;}
        public string City {get; set;}
        public string Country {get; set;}
        public ICollection<Photo> Photos {get; set;}

        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();
        //}
    }
}