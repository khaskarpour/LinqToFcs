using LinqToFcs.Core.DisplayConverters;
using LinqToFcs.Core.Entities;
using LinqToFcs.Core.Enumerators;
using LinqToFcs.Core.Extensions;
using LinqToFcs.Core.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;

namespace LinqToFcs.Core
{
    [TypeConverter(typeof(FcsDataSetConverter))]
    public class FcsDataSet
    {
        #region[      Private Properties      ]

        private object _enumerableEventsToWrite;

        #endregion

        #region[       Public Properties      ]

        [Category("Stream Properties")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Browsable(false)]
        public Stream Stream
        {
            get;
            private set;
        }

        [Category("Stream Properties")]
        [Browsable(false)]
        public long StartsFrom
        {
            get;
            private set;
        }

        [Category("Stream Properties")]
        [Browsable(false)]
        public long EndsTo
        {
            get;
            private set;
        }

        [DisplayName("Header Data")]
        [TypeConverter(typeof(FcsHeaderDataConverter))]
        [Category("FCS Protocol Properties")]
        public HeaderData HeaderData
        {
            get;
            internal protected set;
        }

        [DisplayName("Text Data")]
        [TypeConverter(typeof(FcsTextDataConverter))]
        [Category("FCS Protocol Properties")]
        public TextData TextData
        {
            get;
            internal set;
        }

        [DisplayName("Analysis Data")]
        [TypeConverter(typeof(FcsAnalysisDataConverter))]
        [Category("FCS Protocol Properties")]
        public AnalysisData AnalysisData
        {
            get;
            internal set;
        }

        [Browsable(false)]
        [Category("FCS Protocol Properties")]
        public IEvents Events
        {
            get
            {
                return (IEvents)Activator.CreateInstance(
                    typeof(Events<>).MakeGenericType(TextData.DataType), Stream, TextData);
            }
        }
            
        #endregion

        #region[             cntr             ]

        /// <summary>
        /// constructs the object
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="startsFrom"></param>
        public FcsDataSet(Stream stream, long startsFrom)
        {
            Stream = stream;
            StartsFrom = startsFrom;
        }

        #endregion

        #region[       Writing Methods        ]

        public void SaveChanges<TEvent>(IEnumerable<FcsEvent<TEvent>> events)
            where TEvent :  struct
        {
            DataValidation();

            WriteDataSegment(events);
            WriteTextSegment();
            WriteHeaderSegment();
        }

        private void DataValidation()
        {
            if (TextData == null)
            {
                throw new NullReferenceException("Text segment data can not be null");
            }

            if (_enumerableEventsToWrite == null)
            {
                throw new NullReferenceException("Data segment events can not be null");
            }
        }

        private void WriteHeaderSegment()
        {
            byte[] headerData = SerializerBase<HeaderData>.Builder()
                .Serialize(HeaderData);

            Stream.MoveTo(StartsFrom);
            Stream.Write(headerData, 0, ConstantValues.HeaderLength);
        }

        private void WriteTextSegment()
        {
            byte[] textData = SerializerBase<TextData>.Builder()
                .Serialize(TextData);

            Stream.MoveTo(HeaderData.BeginText);
            Stream.Write(textData, 0, textData.Length);

            HeaderData.EndText = HeaderData.BeginText + textData.Length;
        }

        private void WriteDataSegment<TEvent>(IEnumerable<FcsEvent<TEvent>> events)
            where TEvent : struct
        {
            var eventSerializer = SerializerBase<FcsEvent<TEvent>>.Builder(TextData);

            HeaderData.BeginText = StartsFrom + ConstantValues.HeaderLength;

            TextData.BeginData = 0;
            TextData.EndData = 0;
            TextData.TOT = 0;

            byte[] textData = SerializerBase<TextData>.Builder()
                .Serialize(TextData);

            TextData.BeginData = HeaderData.BeginText + textData.Length + 30;
            Stream.MoveTo(TextData.BeginData);

            foreach (FcsEvent<TEvent> ev in events)
            {
                byte[] eventData = eventSerializer.Serialize(ev);
                Stream.Write(eventData, 0, eventData.Length);

                TextData.TOT++;
            }

            TextData.EndData = (int)Stream.Position;
        }

        public void SetEvents<TResult>(IEnumerable<FcsEvent<TResult>> events)
            where TResult : struct
        {
            if (typeof(TResult) != TextData.DataType)
            {
                throw new NotSupportedException(string.Format("{0} events are not in this dataset", typeof(TResult)));
            }

            Type genericType = typeof(Events<>).MakeGenericType(typeof(TResult));
            _enumerableEventsToWrite = Activator.CreateInstance(genericType, events.GetEnumerator());
        }

        #endregion

        #region[       Reading Methods        ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public IEnumerable<FcsEvent<TResult>> GetEvents<TResult>()
            where TResult : struct
        {
            if (typeof(TResult) != TextData.DataType)
            {
                throw new NotSupportedException(string.Format("{0} events are not in this dataset", typeof(TResult)));
            }

            return (Events<TResult>)Activator.CreateInstance(
                typeof(Events<>).MakeGenericType(TextData.DataType), Stream, TextData);
        }

        /// <summary>
        /// reads the dataset
        /// </summary>
        internal protected virtual void Read()
        {
            HeaderData = ReadHeaderSegment();

            TextData = ReadTextSegment();

            AnalysisData = ReadAnalysisSegment();

            EndsTo = (HeaderData.EndAnalysis != 0 ? HeaderData.EndAnalysis : TextData.EndData);
        }

        /// <summary>
        /// reads header segment of data set
        /// </summary>
        protected virtual HeaderData ReadHeaderSegment()
        {
            byte[] headerBytes = Stream.ReadData(StartsFrom, ConstantValues.HeaderLength);
            
            return SerializerBase<HeaderData>.Builder()
                .Deserialize(headerBytes);
        }

        /// <summary>
        /// reads text segment of data set
        /// </summary>
        protected virtual TextData ReadTextSegment()
        {
            int textSegmentLength = (int)(HeaderData.EndText - HeaderData.BeginText + 1);

            byte[] dataBytes = Stream.ReadData(HeaderData.BeginText, textSegmentLength);

            return SerializerBase<TextData>.Builder()
                .Deserialize(dataBytes);
        }

        /// <summary>
        /// reads analysis data segment
        /// </summary>
        protected virtual AnalysisData ReadAnalysisSegment()
        {
            int segmentLength = (int)(HeaderData.EndAnalysis - HeaderData.BeginAnalysis + 1);

            if (segmentLength == 1)
            {
                return null;
            }

            byte[] dataBytes = Stream.ReadData(HeaderData.BeginAnalysis, segmentLength);

            return SerializerBase<AnalysisData>.Builder()
                .Deserialize(dataBytes);
        }

        #endregion
    }
}
