using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ModelExample.Managers
{
    public class CustomerManager
    {
        public static List<Customer> PlayedBy(int sportId)
        {
            return GetAll().Where(p => p.FavouriteSportId == sportId).ToList();
        }

        public static List<Customer> GetAll()
        {
            return PersonManager.GetAll().Where(c => c.GetType() == typeof(Customer)).Cast<Customer>().ToList();
        }
    }
}
