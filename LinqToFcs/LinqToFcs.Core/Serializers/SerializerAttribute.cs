using System;

namespace LinqToFcs.Core.Serializers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SerializeeAttribute : Attribute
    {
        public Type SerializeeType
        {
            get;
            private set;
        }

        public SerializeeAttribute(Type serializeeType)
        {
            SerializeeType = serializeeType;
        }
    }
}
