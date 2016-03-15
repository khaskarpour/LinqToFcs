using System;

namespace LinqToFcs.Core.Serializers.TypeConverters
{
    public class CustomConverterAttribute : Attribute
    {
        public Type Converter { get; private set; }

        public CustomConverterAttribute(Type serializer)
        {
            Converter = serializer;
        }
    }
}
