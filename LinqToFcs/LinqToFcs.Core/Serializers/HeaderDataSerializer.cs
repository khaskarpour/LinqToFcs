using LinqToFcs.Core.Entities;
using LinqToFcs.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LinqToFcs.Core.Serializers
{
    /// <summary>
    /// 
    /// </summary>
    [Serializee(typeof(HeaderData))]
    internal class HeaderDataSerializer : SerializerBase<HeaderData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override HeaderData Deserialize(byte[] data, params object[] args)
        {
            var headerData = new HeaderData();

            var readVersion = Encoding.ASCII.GetString(data.Take(10).ToArray());

            if (!string.Equals(readVersion, headerData.Version, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new FormatException("Header line is not formatted by FCS standard.");
            }

            var indexes = data
                .Skip(10)
                .Split(8)
                .Select(x => HeaderDataConverter(x.ToArray()))
                .ToArray();

            headerData.GetType()
                .GetProperties()
                .Where(x => x.HasAnyAttribute<DisplayAttribute>(false))
                .OrderBy(x =>
                {
                    var firstOrDefault = x.GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
                    return firstOrDefault != null ? firstOrDefault.Order : 0;
                })
                .DoForEach((x, index) => x.SetValue(headerData, indexes[index], null));

            return headerData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override byte[] Serialize(HeaderData entity, params object[] args)
        {
            if (entity == null)
            {
                throw new NullReferenceException("Header is null");
            }

            var headerText = new StringBuilder(entity.Version);

            // Add text segment start/end
            headerText.Append(HeaderDataGenerator(entity.BeginText, entity.EndText));

            // Add data segment start/end
            headerText.Append((entity.EndData - entity.BeginData) > 99999999
                ? HeaderDataGenerator(0, 0)
                : HeaderDataGenerator(entity.BeginData, entity.EndData));

            // Add analysis segment start/end
            headerText.Append(HeaderDataGenerator(entity.BeginAnalysis, entity.EndAnalysis));

            return Encoding.ASCII.GetBytes(headerText.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static int HeaderDataConverter(byte[] data)
        {
            var text = Encoding.ASCII.GetString(data.ToArray()).Trim();
            return int.Parse(text == string.Empty ? "0" : text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static string HeaderDataGenerator(long start, long end)
        {
            return string.Format("{0}{1}", start.ToString().PadLeft(8), end.ToString().PadLeft(8));
        }
    }
}
