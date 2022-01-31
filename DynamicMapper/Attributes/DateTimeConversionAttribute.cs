using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMapper
{
    public enum DateTimeConversionOptionEnum
    {
        Iso8061OnlyDate, //ISO 8061 : yyyy-MM-dd
        Iso8061FullDateTime //ISO 8061 : yyyy-MM-ddThh:mm:ss
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class DateTimeConversionAttribute : System.Attribute
    {
        public DateTimeConversionOptionEnum? DateTimeConversionOption { get; set; }
        public string Format { get; set; }

        public DateTimeConversionAttribute(DateTimeConversionOptionEnum dateTimeConversionOption)
        {
            this.DateTimeConversionOption = dateTimeConversionOption;
        }

        public DateTimeConversionAttribute(string format)
        {
            this.Format = format;
        }
    }
}
