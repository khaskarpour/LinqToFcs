using LinqToFcs.Core.DisplayConverters;
using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace LinqToFcs.Core
{
    /// <summary>
    /// Main enterance of Linq to Fcs is here
    /// </summary>
    public class FcsContext : IDisposable
    {
        /// <summary>
        /// gets/privately sets the stream 
        /// </summary>
        [Browsable(false)]
        internal Stream Stream
        {
            get;
            private set;
        }

        /// <summary>
        /// collection of file's datasets
        /// </summary>
        [TypeConverter(typeof(FcsDataSetsConverter))]
        public FcsDataSets DataSets
        {
            get;
            private set;
        }

        /// <summary>
        /// constructs the object from a stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="version"></param>
        public FcsContext(Stream stream, SupportedVersions version = SupportedVersions.FCS3)
        {
            if (stream == null)
            {
                throw new NullReferenceException("target stream can not be null!");
            }

            Stream = System.IO.Stream.Synchronized(stream);
            
            Read();
        }

        /// <summary>
        /// construct the object from 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        public FcsContext(string filename, SupportedVersions version = SupportedVersions.FCS3)
        {
            Stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            Read();
        }
        
        /// <summary>
        /// reads the stream upon the FCS protocol
        /// </summary>
        /// <returns></returns>
        private void Read()
        {
            long offset = 0;

            DataSets = new FcsDataSets();

            try
            {
                do
                {
                    var dataSet = new FcsDataSet(Stream, offset);

                    dataSet.Read();

                    offset = dataSet.TextData.NextData;
                    DataSets.Add(dataSet);

                } while (offset != 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                Stream.Position = DataSets.Count > 0 ? DataSets.Last().EndsTo : 0;
            }
        }

        /// <summary>
        /// adds a dataset
        /// </summary>
        /// <param name="textData"></param>
        public void AddDataSet(TextData textData)
        {
            long offset = DataSets.Count != 0 ? DataSets.Last().TextData.NextData : 0;

            // These mjst be set internally also 
            textData.BeginData = 0;
            textData.EndData = 0;
            textData.BeginSText = 0;
            textData.EndSText = 0;

            var dataSet = new FcsDataSet(Stream, offset)
            {
                TextData = textData,
            };

            DataSets.Add(dataSet);
        }

        /// <summary>
        /// removes a dataset from datasets collection
        /// </summary>
        /// <param name="dataSet"></param>
        public void Remove(FcsDataSet dataSet)
        {
            DataSets.Remove(dataSet);
        }

        /// <summary>
        /// saves changes
        /// </summary>
        public void SaveChanges()
        {
            throw new NotImplementedException();
            int offset = 0;

            //DataSets.DoForEach(x =>
            //    {
            //        x.StartsFrom = offset;
            //        x.SaveChanges();
            //        offset = x.TextData.NextData;
            //    });
        }

        /// <summary>
        /// closes context and its underlying stream
        /// </summary>
        public void Close()
        {
            Stream.Close();
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            Stream.Dispose();
        }
    }
}