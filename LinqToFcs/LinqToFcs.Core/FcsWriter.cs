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
        public void Save(FcsDataSets dataSets)
        {

        }
    }
}
