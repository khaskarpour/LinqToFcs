using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsTextDataConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (value is TextData)
            {
                var text = (TextData)value;
                return string.Join(", ", new string[]{ text.PROJ, text.FIL, text.Date.ToShortDateString()});
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
