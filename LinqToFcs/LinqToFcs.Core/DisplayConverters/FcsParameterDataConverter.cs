using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;
using System.Globalization;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsParameterDataConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (value is ParameterData)
            {
                var parameter = (ParameterData)value;
                return parameter.PnN;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
