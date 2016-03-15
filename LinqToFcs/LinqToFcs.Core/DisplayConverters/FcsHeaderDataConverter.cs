using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsHeaderDataConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (value is HeaderData)
            {
                var header = (HeaderData)value;
                return string.Format("{0}, {1}, {2}, {3}", header.BeginText, header.EndText, header.BeginData, header.EndData);
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
