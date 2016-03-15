using LinqToFcs.Core.Entities;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace LinqToFcs.Core.Serializers
{
    [StructLayout(LayoutKind.Explicit)]
    struct UnionArray
    {
        [FieldOffset(0)]
        public byte[] Bytes;

        [FieldOffset(0)]
        public float[] Floats;

        [FieldOffset(0)]
        public double[] Doubles;

        [FieldOffset(0)]
        public int[] Integers;
    }

    /// <summary>
    /// float to event serializer
    /// </summary>
    [Serializee(typeof(FcsEvent<float>))]
    internal class FloatEventSerializer : SerializerBase<FcsEvent<float>>
    {
        private readonly Parameters _parameters;

        public override FcsEvent<float> Deserialize(byte[] data, params object[] args)
        {
            var arry = new UnionArray { Bytes = data };

            return new FcsEvent<float>(
                    new ArraySegment<float>(arry.Floats)
                        .Take(data.Length / 4)
                        .ToArray(),
                    _parameters);
        }

        public override byte[] Serialize(FcsEvent<float> entity, params object[] args)
        {
            float[] columns = entity.ToArray();

            Array.Resize(ref columns, columns.Length * 4);

            UnionArray arry = new UnionArray { Floats = columns };

            return arry.Bytes.ToArray();
        }

        public FloatEventSerializer(Parameters parameters)
        {
            _parameters = parameters;
        }
    }

    /// <summary>
    /// double to event serializer
    /// </summary>
    [Serializee(typeof(FcsEvent<double>))]
    internal class DoubleEventSerializer : SerializerBase<FcsEvent<double>>
    {
        private readonly Parameters _parameters;

        public override FcsEvent<double> Deserialize(byte[] data, params object[] args)
        {
            var arry = new UnionArray { Bytes = data };

            return new FcsEvent<double>(
                    new ArraySegment<double>(arry.Doubles)
                        .Take(data.Length / 8)
                        .ToArray(),
                     _parameters);
        }

        public override byte[] Serialize(FcsEvent<double> entity, params object[] args)
        {
            double[] columns = entity.ToArray();

            UnionArray arry = new UnionArray { Doubles = columns };
            return arry.Bytes
                .Take(columns.Length * 8)
                .ToArray();
        }

        public DoubleEventSerializer(Parameters parameters)
        {
            _parameters = parameters;
        }
    }

    /// <summary>
    /// Int32 to event serializer
    /// </summary>
    [Serializee(typeof(FcsEvent<int>))]
    internal class Int32EventSerializer : SerializerBase<FcsEvent<int>>
    {
        private readonly Parameters _parameters;

        public override FcsEvent<int> Deserialize(byte[] data, params object[] args)
        {
            var arry = new UnionArray { Bytes = data };

            return new FcsEvent<int>(
                    new ArraySegment<int>(arry.Integers)
                        .Take(data.Length / 4)
                        .ToArray(),
                     _parameters);
        }

        public override byte[] Serialize(FcsEvent<int> entity, params object[] args)
        {
            int[] columns = entity.ToArray();

            var arry = new UnionArray { Integers = columns };

            return arry.Bytes
                .Take(columns.Length * 4)
                .ToArray();
        }

        public Int32EventSerializer(Parameters parameters)
        {
            _parameters = parameters;
        }
   }
}
