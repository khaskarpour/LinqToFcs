using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LinqToFcs.Core.Entities
{
    public class AnalysisData : ICloneable
    {
        /// <summary>
        /// Name of person who performed the cellsubset analysis.
        /// </summary>
        [Category("General Properties")]
        [Description("Name of person who performed the cellsubset analysis")]
        public string CSEXP { get; set; }

        /// <summary>
        /// Cell subset analysis date
        /// </summary>
        [Category("General Properties")]
        [Description("Cell subset analysis date")]
        public DateTime CSDATE { get; set; }

        /// <summary>
        /// Cell subset definition file name.
        /// </summary>
        [Category("General Properties")]
        [Description("Cell subset definition file name")]
        public string CSDEFFILE { get; set; }

        /// <summary>
        /// Name of person who performed the cellsubset analysis.
        /// </summary>
        [Category("General Properties")]
        [Description("Analysis segment mode")]
        public AnalysisSegmentModes CSMODE { get; set; }

        /// <summary>
        /// specifies number of subset groups.
        /// </summary>
        [Category("General Properties")]
        [Description("Analysis segment mode")]
        public int CSTOT { get; set; }

        /// <summary>
        /// specifies number of subset groups.
        /// </summary>
        [Category("General Properties")]
        [Description("Cell subset assignments")]
        public int CSVBITS { get; set; }

        /// <summary>
        /// Cell subset definition file name.
        /// </summary>
        [Category("General Properties")]
        [Description("S subsets")]
        public List<CellSubsetData> CellSubsets { get; set; }

        /// <summary>
        /// clones the object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var closedObject = new AnalysisData
            {
                CSDATE = CSDATE,
                CSDEFFILE = CSDEFFILE,
                CSEXP = CSEXP,
                CSMODE = CSMODE,
                CSTOT = CSTOT,
                CSVBITS = CSVBITS
            };

            closedObject.CellSubsets = CellSubsets
                .Select(x => (CellSubsetData)x.Clone())
                .ToList();

            return closedObject;
        }

        /// <summary>
        /// constructs the object
        /// </summary>
        public AnalysisData()
        {
            CellSubsets = new List<CellSubsetData>();
        }
    }
}
