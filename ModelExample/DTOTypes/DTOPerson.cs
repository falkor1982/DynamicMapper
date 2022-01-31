using DynamicMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelExample
{
    public class DTOPerson
    {
        public string Name { get; set; }

        [DateTimeConversion(DateTimeConversionOptionEnum.Iso8061OnlyDate)]
        public string Birthdate { get; set; }

        public DTOCountry Residence { get; set; }
    }
}
