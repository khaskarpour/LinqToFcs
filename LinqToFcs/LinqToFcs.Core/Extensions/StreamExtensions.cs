using System.IO;

namespace LinqToFcs.Core.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// moves stream's pointer position 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="position"></param>
        public static bool MoveTo(this Stream stream, long position, SeekOrigin origin = SeekOrigin.Begin)
        {
            return stream.Seek(position, origin) == position;
        }

        /// <summary>
        /// Reads data from stream from position by length
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="position"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadData(this Stream stream, long position, int length)
        {
            byte[] dataBytes = new byte[length];

            if (!stream.MoveTo(position))
            {
                return null;
            }

            if ((stream.Read(dataBytes, 0, length)) != length)
            {
                return null;
            }

            return dataBytes;
        }

        /// <summary>
        /// Reads data from stream from position by length
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadData(this Stream stream, int length)
        {
            byte[] dataBytes = new byte[length];

            if ((stream.Read(dataBytes, 0, length)) != length)
            {
                return null;
            }

            return dataBytes;
        }
    }
}
