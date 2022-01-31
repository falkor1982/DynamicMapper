using ModelExample.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelExample
{
    public class Country : DTOBase
    {
        public string Name { get; set; }

        private List<Person> residents { get; set; }

        public List<Person> Residents
        {
            get
            {
                if (residents == null)
                    residents = PersonManager.GetByCountry(Id);
                return residents;
            }
        }
    }
}
