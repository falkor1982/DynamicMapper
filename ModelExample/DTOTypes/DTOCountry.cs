using System;
using System.Collections.Generic;
using System.Text;

namespace ModelExample
{
    public class DTOCountry
    {
        public string Name { get; set; }

        public List<DTOPerson> Residents { get; set; }
    }
}
