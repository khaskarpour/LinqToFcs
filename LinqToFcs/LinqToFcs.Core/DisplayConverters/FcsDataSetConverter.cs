using System;
using System.ComponentModel;
using System.Globalization;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsDataSetConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is FcsDataSet)
            {
                return string.Format("Parameters = {0}, Events = {1}", ((FcsDataSet)value).TextData.PAR, ((FcsDataSet)value).TextData.TOT);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
