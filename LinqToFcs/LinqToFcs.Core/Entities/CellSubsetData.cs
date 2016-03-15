using System;
using System.ComponentModel;

namespace LinqToFcs.Core.Entities
{
    public class CellSubsetData : ICloneable
    {
        /// <summary>
        /// Name of cell subset
        /// </summary>
        [Category("General Properties")]
        [Description("Name of cell subset")]
        public string CSName
        {
            get;
            set;
        }

        /// <summary>
        /// Number of cells in cell subset.
        /// </summary>
        [Category("General Properties")]
        [Description("Number of cells in cell subset")]
        public int CSNUM
        {
            get;
            set;
        }

        public object Clone()
        {
            return new CellSubsetData
            {
                CSName = CSName,
                CSNUM = CSNUM
            };
        }
    }
}
