using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace LinqToFcs.Core.Serializers.TypeConverters
{
    public class DataSegmentModesConverter : TypeConverter
    {
        /// <summary>
        /// returns possibility of convertion from type to destination type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        /// converts string to array of byte
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var valueString = value as string;

            if (valueString == null)
            {
                return null;
            }

            return Enum.ToObject(typeof(DataSegmentModes), valueString[0]);
        }

        /// <summary>
        /// returns possibility of convertion from type to destination type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <summary>
        /// converts array of byte to string 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return new string(new char[] { (char)((DataSegmentModes)value) });
        }
    }
}
