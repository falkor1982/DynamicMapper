using System;
using System.Collections.Generic;
using System.Text;
using ModelExample;
using DynamicMapper;

namespace ModelExample
{
    [MirroredEntityAttribute("Customer")]
    public class DTOCustomer : DTOPerson
    {
        public DTOSport FavouriteSport { get; set; }

    }
}
