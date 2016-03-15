using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LinqToFcs.Core.Entities
{
    public class HeaderData
    {
        /// <summary>
        /// FCS Version of dataset
        /// </summary>
        [Category("General")]
        [Description("FCS Version of dataset")]
        public string Version { get { return "FCS3.0    "; } }

        /// <summary>
        /// Text segmentr starts from
        /// </summary>
        [Category("Text Segment")]
        [Display(Order = 0)]
        [Description("Text segmentr starts from")]
        public long BeginText { get; internal set; }

        /// <summary>
        /// Text segment ends to
        /// </summary>
        [Category("Text Segment")]
        [Display(Order = 1)]
        [Description("Text segment ends to")]
        public long EndText { get; internal set; }

        /// <summary>
        /// Data segment starts from
        /// </summary>
        [Category("Data Segment")]
        [Display(Order = 2)]
        [Description("Data segment starts from")]
        public long BeginData { get; internal set; }

        /// <summary>
        /// Data segment ends to
        /// </summary>
        [Category("Data Segment")]
        [Display(Order = 3)]
        [Description("Data segment ends to")]
        public long EndData { get; internal set; }

        /// <summary>
        /// Analysis segment starts from
        /// </summary>
        [Category("Analysis Segment")]
        [Display(Order = 4)]
        [Description("Analysis segment starts from")]
        public long BeginAnalysis { get; internal set; }

        /// <summary>
        /// Analysis segment ends to
        /// </summary>
        [Category("Analysis Segment")]
        [Display(Order = 5)]
        [Description("Analysis segment ends to")]
        public long EndAnalysis { get; internal set; }

        /// <summary>
        /// internal constructor means can only be intantiated by internal objects 
        /// </summary>
        internal HeaderData()
        {
        }
    }
}
