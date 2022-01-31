using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ModelExample.Managers
{
    public class SportManager
    {
        public static Sport Get(int id)
        {
            return GetAll().Where(c => c.Id == id).First();
        } 

        private static List<Sport> GetAll()
        {
            return new List<Sport>() {
                new Sport(){Id = 1, Name = "Soccer"},
                new Sport(){Id = 2, Name = "Table Tennis"},
                new Sport(){Id = 3, Name = "Kitesurf"},
                new Sport(){Id = 4, Name = "Golf"},
                new Sport(){Id = 5, Name = "Volley"}
            };
        }
    }
}
