using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ModelExample.Managers
{
    public class PersonManager
    {
        public static Person Get(int id)
        {
            return GetAll().Where(c => c.Id == id).First();
        }

        public static List<Person> GetByCountry(int countryId)
        {
            return GetAll().Where(p => p.ResidenceId == countryId).ToList();
        }

        public static List<Person> GetAll()
        {
            return new List<Person>() {
                new Customer(){Id = 1, Name = "John", Birthdate = new DateTime(2002,2,14), ResidenceId = 2, FavouriteSportId = 5},
                new Employee(){Id = 2, Name = "Cindy", Birthdate = new DateTime(1994,7,7), ResidenceId = 1, Salary = 10.5m},
                new Customer(){Id = 3, Name = "Patricia", Birthdate = new DateTime(1988,12,1), ResidenceId = 5, FavouriteSportId = 3},
                new Customer(){Id = 4, Name = "George", Birthdate = new DateTime(1999,7,19), ResidenceId = 3, FavouriteSportId = 2},
                new Employee(){Id = 5, Name = "Andrea", Birthdate = new DateTime(1988,1,4), ResidenceId = 4, Salary = 12.0m},
            };
        }
    }
}
