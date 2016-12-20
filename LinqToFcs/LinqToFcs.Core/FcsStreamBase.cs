using LinqToFcs.Core.DisplayConverters;
using System;
using System.ComponentModel;
using System.IO;

namespace LinqToFcs.Core
{
    public abstract class FcsStreamBase : IDisposable
    {
        private bool _disposed;

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
        /// 
        /// </summary>
        public FcsDataSets DataSets
        {
            get;
            private set;
        }

        /// <summary>
        /// construct the object from 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        public FcsStreamBase(
            string filename, 
            FileMode mode = FileMode.Open, 
            FileAccess access = FileAccess.ReadWrite,
            FileShare share = FileShare.ReadWrite,
            SupportedVersions version = SupportedVersions.FCS3)
        {
            DataSets = new FcsDataSets();
            Stream = new FileStream(filename, mode, access, share);
        }

        /// <summary>
        /// Finalizes the object.
        /// </summary>
        ~FcsStreamBase()
        {
            Dispose(false);
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        /// <param name="disposing">Specifies whether dispose or not</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Stream.Dispose();
            }

            _disposed = true;
        }
    }
}
