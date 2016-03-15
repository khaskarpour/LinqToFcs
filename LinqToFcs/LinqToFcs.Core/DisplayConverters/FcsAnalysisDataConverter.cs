using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;
using System.Globalization;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsAnalysisDataConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (value is AnalysisData)
            {
                return "-- Analysis --";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
