using LinqToFcs.Core.DisplayConverters;
using System;
using System.ComponentModel;
using System.Linq;

namespace LinqToFcs.Core.Entities
{
    [TypeConverter(typeof(FcsParameterDataConverter))]
    public class ParameterData : ICloneable
    {
        /// <summary>
        /// specify the short nameof parameter PnN
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("Parameter index")]
        public int Index
        {
            get;
            internal set;
        }
        
        /// <summary>
        /// specify the short nameof parameter PnN
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specify the short name of parameter PnN")]
        public string PnN
        {
            get;
            set;
        }

        /// <summary>
        /// specifies number of parameter bits (PnB)
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies number of parameter bits (PnB)")]
        public int PnB
        {
            get;
            internal set;
        }

        /// <summary>
        /// pecifies whether linear or logarithmicamplifiers were used for parameter number (PnE)
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("pecifies whether linear or logarithmicamplifiers were used for parameter number (PnE)")]
        public string PnE
        {
            get;
            set;
        }

        /// <summary>
        /// specifies whether linear or logarithmic amplifiers were used for gating parameter number (GnE)
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies whether linear or logarithmic amplifiers were used for gating parameter number (GnE)")]
        public string GnE
        {
            get;
            set;
        }

        /// <summary>
        /// the optical filter that was used for the light reaching the detector for parameter (PnF)
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("the optical filter that was used for the light reaching the detector for parameter (PnF)")]
        public string PnF
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the optical filter thatwas used for the light reaching the detector for gating parameter (GnF)
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the optical filter thatwas used for the light reaching the detector for gating parameter (GnF)")]
        public string GnF
        {
            get;
            set;
        }
        
        /// <summary>
        /// specifies the gain that was used to amplify the signal for parameter (PnG)
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the gain that was used to amplify the signal for parameter (PnG)")]
        public float PnG
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the excitation wavelength (PnL)
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the excitation wavelength (PnL)")]
        public float PnL
        {
            get;
            set;
        }

        /// <summary>
        /// specifies a short name for gating parameter number
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies a short name for gating parameter number")]
        public string GnN
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the excitation power PnO
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the excitation power PnO")]
        public float PnO
        {
            get;
            set;
        }

        /// <summary>
        ///  amount of light collected by the detectorfor parameter number n expressed as a percentage of the lightemitted by a fluorescent object.
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("amount of light collected by the detectorfor parameter number n expressed as a percentage of the lightemitted by a fluorescent object.")]
        public float PnP
        {
            get;
            set;
        }

        /// <summary>
        /// The amount of light collected by the detectorfor gating parameter number n1 expressed as a percentage of thelight emitted by a fluorescent object.
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("The amount of light collected by the detectorfor gating parameter number n1 expressed as a percentage of thelight emitted by a fluorescent object.")]
        public float GnP
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the maximum range
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the maximum range")]
        public float PnR
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the range
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the range")]
        public float GnR
        {
            get;
            set;
        }

        /// <summary>
        /// specifies a long name to be used as an axis label in a plot of parameter 
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies a long name to be used as an axis label in a plot of parameter")]
        public string PnS
        {
            get;
            set;
        }

        /// <summary>
        /// specifies a longer name for gating parameter 
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies a longer name for gating parameter")]
        public string GnS
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the detector type forparameter
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the detector type forparameter")]
        public string PnT
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the detector type forgating parameter
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the detector type forgating parameter")]
        public string GnT
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the detector bias voltage
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the detector bias voltage")]
        public float PnV
        {
            get;
            set;
        }

        /// <summary>
        /// specifies the detector bias voltagefor gating parameter
        /// </summary>
        [Category("Text Segment Properties")]
        [Description("specifies the detector bias voltagefor gating parameter")]
        public float GnV
        {
            get;
            set;
        }

        /// <summary>
        /// specify the short nameof parameter PnN
        /// </summary>
        [Category("Helper Properties")]
        [Description("specify the short name of parameter PnN")]
        [Browsable(false)]
        public string Name
        {
            get { return PnN; }
        }

        /// <summary>
        /// represents long name which is only value for Fluidigm purposes
        /// </summary>
        [Category("Helper Properties")]
        [Description("specify the long name of parameter - custom formatting")]
        [Browsable(false)]
        public string LongName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(PnS))
                {
                    return string.Format("{0}({1})", PnS, PnN);
                }

                return PnN;
            }
        }

        /// <summary>
        /// clones the object
        /// </summary>
        /// <returns>cloned parameter</returns>
        public object Clone()
        {
            return new ParameterData
            {
                GnE = GnE,
                GnF = GnF,
                GnN = GnN,
                GnP = GnP,
                GnR = GnR,
                GnS = GnS,
                GnT = GnT,
                GnV = GnV,
                PnB = PnB,
                PnE = PnE,
                PnF = PnF,
                PnG = PnG,
                PnL = PnL,
                PnN = PnN,
                PnO = PnO,
                PnP = PnP,
                PnR = PnR,
                PnS = PnS,
                PnT = PnT,
                PnV = PnV,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="par2"></param>
        /// <returns></returns>
        protected bool Equals(ParameterData par2)
        {
            return GetType()
                .GetProperties()
                .All(x =>
                {
                    var par2PropValue = par2.GetType()
                        .GetProperty(x.Name)
                        .GetValue(par2);

                    return x.GetValue(this).Equals(par2PropValue);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((ParameterData)obj);
        }
    }
}
