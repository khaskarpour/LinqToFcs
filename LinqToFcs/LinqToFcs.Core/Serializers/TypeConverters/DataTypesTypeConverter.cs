using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace LinqToFcs.Core.Serializers.TypeConverters
{
    public class DataTypesTypeConverter : TypeConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Tuple<string, Type>[] dataTypes = 
            {
                new Tuple<string,Type>("B", typeof(bool)),
                new Tuple<string,Type>("D", typeof(double)),
                new Tuple<string,Type>("F", typeof(float)),
                new Tuple<string,Type>("I", typeof(int)),
                new Tuple<string,Type>("A", typeof(string)),
            };

            return dataTypes
                .First(x => x.Item1 == (string)value)
                .Item2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var dataTypes = new[]
            {
                new Tuple<string,Type>("B", typeof(bool)),
                new Tuple<string,Type>("D", typeof(double)),
                new Tuple<string,Type>("F", typeof(float)),
                new Tuple<string,Type>("I", typeof(int)),
                new Tuple<string,Type>("A", typeof(string)),
            };

            return dataTypes
                .First(x => x.Item2 == (Type)value)
                .Item1;
        }
    }
}
