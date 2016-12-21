using LinqToFcs.Core.Entities;
using LinqToFcs.Core.Extensions;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace LinqToFcs.Core.Serializers
{
    [Serializee(typeof(TextData))]
    internal class TextDataSerializer : SerializerBase<TextData>
    {
        public override TextData Deserialize(byte[] data, params object[] args)
        {
            TextData textData = new TextData();

            const string textSegmentReg = @"^P|G\d+\w{1}$";

            string dataString = Encoding.ASCII.GetString(data);

            ConstantValues.Delimiter = dataString[0];

            var textSegmentTerms = dataString
                .Substring(1)
                .Split(new[] { ConstantValues.Delimiter });

            if (textSegmentTerms.Length % 2 != 0)
            {
                textSegmentTerms = new ArraySegment<string>(textSegmentTerms)
                    .Take(textSegmentTerms.Length - 1)
                    .ToArray();
            }

            for (int i = 0; i < textSegmentTerms.Length; i += 2)
            {
                if (!textSegmentTerms[i].StartsWith("$"))
                {
                    textData.SetMember(textSegmentTerms[i], textSegmentTerms[i + 1]);
                    continue;
                }

                string keyword = textSegmentTerms[i].Substring(1);
                string value = textSegmentTerms[i + 1];

                var propertyInfo = textData
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(y => string.Equals(y.Name, keyword, StringComparison.CurrentCultureIgnoreCase));

                if (propertyInfo != null)
                {
                    var typeConverter = GetTypeConverter(propertyInfo);

                    if (typeConverter == null)
                    {
                        continue;
                    }

                    propertyInfo.SetValue(textData, typeConverter.ConvertFrom(value), null);
                }
                else if (Regex.Match(keyword, textSegmentReg).Success)
                {
                    var parIndex = Regex.Matches(keyword, @"\d+")
                        .OfType<Match>()
                        .Select(y => int.Parse(y.Value) - 1)
                        .FirstOrDefault();

                    var selectedParameter = textData.Parameters.FirstOrDefault(par => par.Index == parIndex);

                    if (selectedParameter == null)
                    {
                        selectedParameter = new ParameterData() { Index = parIndex };
                        textData.Parameters.Add(selectedParameter);
                    }

                    var fixedPropName = string.Format("{0}n{1}", keyword[0], keyword[keyword.Length - 1]);
                    SetProperty(selectedParameter, fixedPropName, value);
                }
            }

            return textData;
        }

        private string SerialzeParameter(ParameterData parameter)
        {
            return string.Concat(
                        parameter
                        .GetType()
                        .GetProperties()
                        .Where(x => x.HasAnyAttribute<DataMemberAttribute>(true) && x.GetValue(parameter, null) != null)
                        .Select(x => string.Format("${0}{1}{2}{3}{4}{5}", x.Name.ToUpper()[0], parameter.Index + 1, x.Name.ToUpper().Last(), ConstantValues.Delimiter, x.GetValue(parameter, null), ConstantValues.Delimiter)));
        }

        public override byte[] Serialize(TextData entity, params object[] args)
        {
            //TODO: this code only convertes object to string without refering to custom formatting
            string parametersString = string.Concat(
                entity
                .Parameters
                .Select(parameter => SerialzeParameter(parameter)));

            var textDataProperties =
                entity
                    .GetType()
                    .GetProperties()
                    .Where(x => x.HasAnyAttribute<DataMemberAttribute>(true) && x.GetValue(entity, null) != null);

            StringBuilder textStringBuilder = new StringBuilder(ConstantValues.Delimiter.ToString());

            foreach (var propertyInfo in textDataProperties)
            {
                var typeConverter = GetTypeConverter(propertyInfo);

                if (typeConverter == null)
                {
                    continue;
                }

                object value = propertyInfo.GetValue(entity, null);

                textStringBuilder.AppendFormat("${0}{1}{2}{3}", propertyInfo.Name.ToUpper(), ConstantValues.Delimiter, typeConverter.ConvertTo(value, typeof(string)), ConstantValues.Delimiter);
            }

            return Encoding.ASCII.GetBytes(string.Format("{0}{1}", textStringBuilder.ToString(), parametersString));
        }
    }
}
