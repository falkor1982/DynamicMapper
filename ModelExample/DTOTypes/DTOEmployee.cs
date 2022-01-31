using System;
using System.Collections.Generic;
using System.Text;
using DynamicMapper;

namespace ModelExample
{
    [MirroredEntityAttribute("Employee")]
    public class DTOEmployee : DTOPerson
    {
        public string Salary { get; set; } //Salary   Fee

    }
}
