using LinqToFcs.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace LinqToFcs.Core.Enumerators
{
    public interface IEvents : IEnumerable<IFcsEvent>
    {
    }

    public class Events<T> : IEvents, IEnumerable<FcsEvent<T>>
        where T : struct
    {
        /// <summary>
        /// event enumerator container.
        /// </summary>
        private readonly IEnumerator<FcsEvent<T>> _enumerator;

        /// <summary>
        /// gets the collection's enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<FcsEvent<T>> GetEnumerator()
        {
            return _enumerator;
        }

        /// <summary>
        /// gets the collection's enumerator.
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// constructs the object
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="textData"></param>
        public Events(Stream stream, TextData textData)
        {
            Type genericType = typeof(EventEnumerator<>).MakeGenericType(textData.DataType);
            _enumerator = (EventEnumerator<T>)Activator.CreateInstance(genericType, stream, textData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerator"></param>
        public Events(IEnumerator<FcsEvent<T>> enumerator)
        {
            _enumerator = enumerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator<IFcsEvent> IEnumerable<IFcsEvent>.GetEnumerator()
        {
            return (IEnumerator<IFcsEvent>)_enumerator;
        }
    }
}
