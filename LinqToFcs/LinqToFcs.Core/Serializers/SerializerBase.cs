using LinqToFcs.Core.Entities;
using LinqToFcs.Core.Serializers.TypeConverters;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace LinqToFcs.Core.Serializers
{
    internal abstract class SerializerBase<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract TEntity Deserialize(byte[] data, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract byte[] Serialize(TEntity entity, params object[] args);

        /// <summary>
        /// returns type converter of property
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        internal protected static TypeConverter GetTypeConverter(PropertyInfo propertyInfo)
        {
            var typeConverter = propertyInfo
                .GetCustomAttributes<CustomConverterAttribute>(true)
                .FirstOrDefault();

            return typeConverter != null
                ? (TypeConverter)Activator.CreateInstance(typeConverter.Converter)
                : TypeDescriptor.GetConverter(propertyInfo.PropertyType);
        }

        /// <summary>
        /// sets a property by a specific value
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        internal protected virtual void SetProperty(object entity, string propertyName, object value)
        {
            var propertyInfo = entity.GetType()
                .GetProperties()
                .FirstOrDefault(x => string.Equals(x.Name, propertyName, StringComparison.CurrentCultureIgnoreCase));

            if (propertyInfo == null)
            {
                throw new NullReferenceException();
            }

            propertyInfo.SetValue(entity, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        /// <summary>
        /// Builds counterpart serializer of TEntity
        /// </summary>
        /// <returns></returns>
        public static SerializerBase<TEntity> Builder(params object[] args)
        {
            var serializerType = typeof(TEntity)
                .Assembly
                .GetTypes()
                .First(x =>
                {
                    var attr = x.GetCustomAttribute<SerializeeAttribute>(false);
                    return attr != null && attr.SerializeeType == typeof(TEntity);
                });

            return (SerializerBase<TEntity>)Activator.CreateInstance(serializerType, args);
        }
    }
}
