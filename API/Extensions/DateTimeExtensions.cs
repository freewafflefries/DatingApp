using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth >  today.AddYears(-age)) age --; //checking to see if the user has had birthday yet this year. If not, reduce calc'ed age by 1
            return age;
        }
    }
}