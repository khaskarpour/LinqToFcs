using System.IO;

namespace LinqToFcs.Core
{
    public class FcsWriter : FcsStreamBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        public FcsWriter(string filename, SupportedVersions version = SupportedVersions.FCS3)
            : base(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write, version)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSets"></param>
        public void Write(FcsDataSets dataSets)
        {
            DataSets.AddRange(dataSets);

            for (int i = 0; i < dataSets.Count; ++i)
            {
                DataSets[i].Stream = this.Stream;
                dataSets[i].Write();
            }
        }
    }
}
