using LinqToFcs.Core.Entities;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LinqToFcs.Core.Extensions;

namespace LinqToFcs.Core.Serializers
{
    [Serializee(typeof(AnalysisData))]
    internal class AnalysisDataSerializer : SerializerBase<AnalysisData>
    {
        public override AnalysisData Deserialize(byte[] data, params object[] args)
        {
            var analysisData = new AnalysisData();

            const string analysisSegmentReg = @"^CS\d+\w{1}$";

            string dataString = Encoding.ASCII.GetString(data);

            var segmentTerms = dataString.Substring(1)
                .Split(new[] { "$" }, StringSplitOptions.RemoveEmptyEntries);

            foreach(var term in segmentTerms)
            { 
                var keywordValue = term.Split(new[] { ConstantValues.Delimiter }, StringSplitOptions.RemoveEmptyEntries);

                if (keywordValue.Length != 3 && keywordValue.Length != 2)
                {
                    throw new FormatException("Text section is not formatted by FCS standard.");
                }

                string value = keywordValue[1];

                if (keywordValue.Length == 3)
                {
                    value = string.Format("{0}//{1}", keywordValue[1], keywordValue[2]);
                }

                var propertyInfo = analysisData.GetType()
                    .GetProperties()
                    .FirstOrDefault(y => string.Equals(y.Name, keywordValue[0], StringComparison.CurrentCultureIgnoreCase));

                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(analysisData,
                        propertyInfo.PropertyType.IsEnum
                            ? Enum.ToObject(propertyInfo.PropertyType, value[0])
                            : Convert.ChangeType(value, propertyInfo.PropertyType), null);
                }
                else if (Regex.Match(keywordValue[0], analysisSegmentReg).Success)
                {
                    var parIndex = Regex.Matches(keywordValue[0], @"\d+")
                        .OfType<Match>()
                        .Select(y => int.Parse(y.Value) - 1)
                        .FirstOrDefault();

                    if (analysisData.CellSubsets.Count <= parIndex)
                    {
                        analysisData.CellSubsets.Add(new CellSubsetData());
                    }

                    var fixedPropName = string.Format("CSn{0}", keywordValue[0].EndsWith("Name") ? "Name" : "NUM");
                    SetProperty(analysisData.CellSubsets[parIndex], fixedPropName, value);
                }
            }

            return analysisData;
        }

        public override byte[] Serialize(AnalysisData entity, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
