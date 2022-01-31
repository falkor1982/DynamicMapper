using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ModelExample.Managers
{
    public class CountryManager
    {
        public static Country Get(int id)
        {
            return GetAll().Where(c => c.Id == id).First();
        }

        public static List<Country> GetAll()
        {
            return new List<Country>() {
                new Country(){Id = 1, Name = "Argentina" },
                new Country(){Id = 2, Name = "Brazil"  },
                new Country(){Id = 3, Name = "China"  },
                new Country(){Id = 4, Name = "Dennmark"},
                new Country(){Id = 5, Name = "Egypt"}
            };
        }
    }
}
