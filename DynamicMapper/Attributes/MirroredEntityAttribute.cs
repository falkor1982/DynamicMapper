using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMapper
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class MirroredEntityAttribute : System.Attribute
    {
        public string BusinessEntityName { get; set; }

        public MirroredEntityAttribute(string businessEntityName)
        {
            this.BusinessEntityName = businessEntityName;
        }
    } 
}
