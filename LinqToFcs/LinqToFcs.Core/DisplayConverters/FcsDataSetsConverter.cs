using System;
using System.ComponentModel;
using System.Globalization;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsDataSetsConverter : ExpandableObjectConverter 
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (value is FcsDataSets)
            {
                var dataSets = (FcsDataSets)value;
                return string.Format("{0} DataSet(s)", dataSets.Count);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
