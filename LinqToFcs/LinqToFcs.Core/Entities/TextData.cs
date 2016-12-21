using LinqToFcs.Core.DisplayConverters;
using LinqToFcs.Core.Serializers.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;

namespace LinqToFcs.Core.Entities
{
    public class TextData : DynamicObject, ICloneable
    {
        private Dictionary<string, object> _dictionary;

        /// <summary>
        /// Analysis segment starts from (BEGINANALYSIS)
        /// </summary>
        [Category("Data Segment Properties")]
        [Description("Data segment starts from (BEGINANALYSIS)")]
        [DataMember]
        public long BeginAnalysis
        {
            get;
            internal set;
        }

        /// <summary>
        /// Analysis segment ends to (ENDANALYSIS)
        /// </summary>
        [Category("Data Segment Properties")]
        [Description("Data segment ends to (ENDANALYSIS)")]
        [DataMember]
        public long EndAnalysis
        {
            get;
            internal set;
        }

        /// <summary>
        /// Data segment starts from (BEGINDATA)
        /// </summary>
        [Category("Data Segment Properties")]
        [Description("Data segment starts from (BEGINDATA)")]
        [DataMember]
        public long BeginData
        {
            get;
            internal set;
        }

        /// <summary>
        /// Data segment ends to (ENDDATA)
        /// </summary>
        [Category("Data Segment Properties")]
        [Description("Data segment ends to (ENDDATA)")]
        [DataMember]
        public long EndData
        {
            get;
            internal set;
        }

        /// <summary>
        /// Data segment starts from (BEGINDATA)
        /// </summary>
        [Category("Suplementary Text Segment Properties")]
        [Description("Data segment starts from (BEGINSTEXT)")]
        [DataMember]
        public long BeginSText
        {
            get;
            internal set;
        }

        /// <summary>
        /// Data segment ends to (ENDDATA)
        /// </summary>
        [Category("Suplementary Text Segment Properties")]
        [Description("Data segment ends to (ENDSTEXT)")]
        [DataMember]
        public long EndSText
        {
            get;
            internal set;
        }

        /// <summary>
        /// Investigator's name (EXP)
        /// </summary>
        [Category("General Properties")]
        [Description("Investigator's name (EXP)")]
        [DataMember]
        public string EXP
        {
            get;
            set;
        }

        /// <summary>
        /// Begining time of acquisition (BTIM)
        /// </summary>
        [Category("General Properties")]
        [Description("Begining time of acquisition (BTIM)")]
        [DataMember]
        public TimeSpan BTIM
        {
            get;
            set;
        }

        /// <summary>
        /// Ending time of acquisition (ETIM)
        /// </summary>
        [Category("General Properties")]
        [Description("Ending time of acquisition (ETIM)")]
        [DataMember]
        public TimeSpan ETIM
        {
            get;
            set;
        }

        /// <summary>
        /// Data file name (FIL)
        /// </summary>
        [Category("General Properties")]
        [Description("Data file name (FIL)")]
        [DataMember]
        public string FIL
        {
            get;
            set;
        }

        /// <summary>
        /// Comment of investigator in file (COM)
        /// </summary>
        [Category("General Properties")]
        [Description("Comment of investigator in file (COM)")]
        [DataMember]
        public string COM
        {
            get;
            set;
        }

        /// <summary>
        /// Data segment's mode (MODE)
        /// </summary>
        [Category("General Properties")]
        [Description("Data segment's mode (MODE)")]
        [DataMember]
        [CustomConverter(typeof(DataSegmentModesConverter))]
        public DataSegmentModes MODE
        {
            get;
            set;
        }

        /// <summary>
        /// Data type of events in Data segment (DATATYPE)
        /// </summary>
        [Category("General Properties")]
        [Description("Data type of events in Data segment (DATATYPE)")]
        [CustomConverter(typeof(DataTypesTypeConverter))]
        [DataMember]
        public Type DataType
        {
            get;
            internal set;
        }

        /// <summary>
        /// Number of parameters in an event (PAR)
        /// </summary>
        [Category("General Properties")]
        [Description("Number of parameters in an event (PAR)")]
        [DataMember]
        public int PAR
        {
            get;
            set;
        }

        /// <summary>
        /// total number of events (TOT)
        /// </summary>
        [Category("General Properties")]
        [Description("total number of events (TOT)")]
        [DataMember]
        public int TOT
        {
            get;
            internal set;
        }

        /// <summary>
        /// date of data set acquisition (DATE)
        /// </summary>
        [Category("General Properties")]
        [Description("date of data set acquisition (DATE)")]
        [DataMember]
        [CustomConverter(typeof(DateTimeTypeConverter))]
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// date of data set acquisition (DATE)
        /// </summary>
        [Category("General Properties")]
        [Description("date of data set acquisition (DATE)")]
        [DataMember]
        public int GATE
        {
            get;
            set;
        }

        /// <summary>
        /// operation on subset data (GATING)
        /// </summary>
        [Category("General Properties")]
        [Description("operation on subset data (GATING)")]
        [DataMember]
        public string GATING
        {
            get;
            set;
        }

        /// <summary>
        /// number of lost events because of electronic failures (ABRT)
        /// </summary>
        [Category("General Properties")]
        [Description("number of lost events because of electronic failures (ABRT)")]
        [DataMember]
        public int ABRT
        {
            get;
            set;
        }

        /// <summary>
        /// number of lost events because of aquisition software was busy (LOST)
        /// </summary>
        [Category("General Properties")]
        [Description("number of lost events because of aquisition software was busy (LOST)")]
        [DataMember]
        public int LOST
        {
            get;
            set;
        }

        /// <summary>
        /// byte order of each parameter value (BYTEORD)
        /// </summary>
        [Category("General Properties")]
        [Description("byte order of each parameter value (BYTEORD)")]
        [CustomConverter(typeof(ByteOrderTypeConverter))]
        [DataMember]
        public byte[] BYTEORD
        {
            get;
            set;
        }

        /// <summary>
        /// Cell type which have been measured (CELL)
        /// </summary>
        [Category("General Properties")]
        [Description("Cell type which have been measured (CELL)")]
        public string CELL
        {
            get;
            set;
        }

        /// <summary>
        /// data set fluorescence compensation matrix (COMP)
        /// </summary>
        [Category("General Properties")]
        [Description("data set fluorescence compensation matrix (COMP)")]
        [DataMember]
        public string COMP
        {
            get;
            set;
        }

        /// <summary>
        /// cell subset mode (CSMODE)
        /// </summary>
        [Category("General Properties")]
        [Description("cell subset mode (CSMODE)")]
        public int CSMODE
        {
            get;
            set;
        }

        /// <summary>
        /// number of bits of each subset (CSVBITS)
        /// </summary>
        [Category("General Properties")]
        [Description("number of bits of each subset (CSVBITS)")]
        public int CSVBITS
        {
            get;
            set;
        }

        /// <summary>
        /// flow cytometer used for the data (CYT)
        /// </summary>
        [Category("General Properties")]
        [Description("flow cytometer used for the data (CYT)")]
        [DataMember]
        public string CYT
        {
            get;
            set;
        }

        /// <summary>
        /// Flow cytometer serial number.
        /// </summary>
        [Category("General Properties")]
        [Description("Flow cytometer serial number.")]
        [DataMember]
        public string CYTSN
        {
            get;
            set;
        }

        /// <summary>
        /// institution (INST)
        /// </summary>
        [Category("General Properties")]
        [Description("institution (INST)")]
        [DataMember]
        public string INST
        {
            get;
            set;
        }

        /// <summary>
        /// next dataset starts from (NEXTDATA)
        /// </summary>
        [Category("General Properties")]
        [Description("next dataset starts from (NEXTDATA)")]
        [DataMember]
        public long NextData
        {
            get;
            internal set;
        }

        /// <summary>
        /// cytometer operator (OP)
        /// </summary>
        [Category("General Properties")]
        [Description("cytometer operator (OP)")]
        [DataMember]
        public string OP
        {
            get;
            set;
        }

        /// <summary>
        /// Project Name (PROJ)
        /// </summary>
        [Category("General Properties")]
        [Description("Project Name (PROJ)")]
        [DataMember]
        public string PROJ
        {
            get;
            set;
        }

        /// <summary>
        /// specifies specimen number (SMNO)
        /// </summary>
        [Category("General Properties")]
        [Description("pecifies specimen number (SMNO)")]
        [DataMember]
        public string SMNO
        {
            get;
            set;
        }

        /// <summary>
        /// specifies source of specimen (SRC)
        /// </summary>
        [Category("General Properties")]
        [Description("specifies source of specimen (SRC)")]
        [DataMember]
        public string SRC
        {
            get;
            set;
        }

        /// <summary>
        /// specifies aquisition computer operating system (SYS)
        /// </summary>
        [Category("General Properties")]
        [Description("specifies aquisition computer operating system (SYS)")]
        [DataMember]
        public string SYS
        {
            get;
            set;
        }

        /// <summary>
        /// specifies acquisition time steps (TIMESTEP)
        /// </summary>
        [Category("General Properties")]
        [Description("specifies acquisition time steps (TIMESTEP)")]
        [DataMember]
        public float TIMESTEP
        {
            get;
            set;
        }

        /// <summary>
        /// specifies Trigger parameter (TR)
        /// </summary>
        [Category("General Properties")]
        [Description("specifies Trigger parameter (TR)")]
        [DataMember]
        public string TR
        {
            get;
            set;
        }

        /// <summary>
        /// specifies list of parameters
        /// </summary>
        [Category("General Properties")]
        [Description("List of Parameters data")]
        [TypeConverter(typeof(FcsParametersConverter))]
        public Parameters Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// clones the object
        /// </summary>
        /// <returns>cloned text segment</returns>
        public object Clone()
        {
            var textData = new TextData
            {
                BeginData = BeginData,
                EndData = EndData,
                BeginSText = BeginSText,
                ABRT = ABRT,
                BTIM = BTIM,
                BYTEORD = BYTEORD,
                CELL = CELL,
                COM = COM,
                COMP = COMP,
                CSMODE = CSMODE,
                CSVBITS = CSVBITS,
                CYT = CYT,
                CYTSN = CYTSN,
                DataType = DataType,
                Date = Date,
                EndSText = EndSText,
                ETIM = ETIM,
                EXP = EXP,
                FIL = FIL,
                GATE = GATE,
                GATING = GATING,
                INST = INST,
                LOST = LOST,
                MODE = MODE,
                NextData = NextData,
                OP = OP,
                PAR = PAR,
                PROJ = PROJ,
                SMNO = SMNO,
                SRC = SRC,
                SYS = SYS,
                TIMESTEP = TIMESTEP,
                TOT = TOT,
                TR = TR,
            };

            textData.Parameters = (Parameters)Parameters.Clone();

            return textData;
        }

        /// <summary>
        /// constructs the object
        /// </summary>
        public TextData()
        {
            Parameters = new Parameters();
            _dictionary = new Dictionary<string, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text2"></param>
        /// <returns></returns>
        protected bool Equals(TextData text2)
        {
            var checkTextPropResult = GetType()
                .GetProperties()
                .All(x =>
                {
                    var par2PropValue = text2.GetType()
                        .GetProperty(x.Name)
                        .GetValue(text2);

                    return x.GetValue(this).Equals(par2PropValue);
                });

            return checkTextPropResult && this.Parameters.Equals(text2.Parameters);
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

            return obj.GetType() == GetType() && Equals((TextData)obj);
        }

        /// <summary>
        /// If you try to get a value of a property 
        /// not defined in the class, this method is called.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name.ToLower();

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return _dictionary.TryGetValue(name, out result);
        }

        /// <summary>
        /// If you try to set a value of a property that is
        /// not defined in the class, this method is called.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            _dictionary[binder.Name.ToLower()] = value;

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }

        /// <summary>
        /// sets the 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        internal void SetMember(string name, object value)
        {
            _dictionary[name.ToLower()] = value;
        }
    }
}
