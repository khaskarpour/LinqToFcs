using LinqToFcs.Core.Entities;
using System;
using System.Linq;
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

                var propertyInfo = textData.GetType()
                    .GetProperties()
                    .FirstOrDefault(y => string.Equals(y.Name, keyword, StringComparison.CurrentCultureIgnoreCase));

                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType.IsEnum)
                    {
                        propertyInfo.SetValue(textData, Enum.ToObject(propertyInfo.PropertyType, value[0]), null);
                    }
                    else
                    {
                        var typeConverter = GetTypeConverter(propertyInfo);

                        if (typeConverter == null)
                        {
                            continue;
                        }

                        propertyInfo.SetValue(textData, typeConverter.ConvertFrom(value), null);
                    }
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

        public override byte[] Serialize(TextData entity, params object[] args)
        {
            //TODO: this code only convertes object to string without refering to custom formatting
            var textString = string.Concat(
                entity.GetType()
                .GetProperties()
                .Select(x => string.Format("${0}{1}{2}", x.Name, ConstantValues.Delimiter, x.GetValue(entity, null))));

            string parametersString = string.Concat(entity.Parameters
                .Select((parameter, index) =>
                    string.Concat(
                        parameter.GetType()
                        .BaseType
                        .GetProperties()
                        .Select(y => string.Format("${0}{1}{2}{3}{4}", y.Name[0], index + 1, y.Name.Last(), ConstantValues.Delimiter, y.GetValue(parameter, null))))));

            var textToWrite = string.Format("{0}{1}{2}{3}", ConstantValues.Delimiter, textString, ConstantValues.Delimiter, parametersString);

            return Encoding.ASCII.GetBytes(textToWrite);
        }
    }
}
