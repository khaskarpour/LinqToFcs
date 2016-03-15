using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;
using System.Globalization;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsParametersConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (value is Parameters)
            {
                var parameters = (Parameters)value;
                return string.Format("{0} Parameter(s)", parameters.Count);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
