using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorUI.PropertyGrid
{
    public class BooleanTypeConverter : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            return (bool)value ?
               "Да" : "Нет";
        }

        public override object ConvertFrom(ITypeDescriptorContext context,CultureInfo culture,object value)
        {
            return (string)value == "Да";
        }
    }

}
