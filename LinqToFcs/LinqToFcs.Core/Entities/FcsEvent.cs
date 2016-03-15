using System.Collections;
using System.Collections.Generic;

namespace LinqToFcs.Core.Entities
{
    public interface IFcsEvent : IEnumerable
    {
        /// <summary>
        /// gets length of collection
        /// </summary>
        int Length { get; }

        /// <summary>
        /// collection indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object this[int index] { get; }

        /// <summary>
        /// collection indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object this[string index] { get; }
    }

    public class FcsEvent<T> : IFcsEvent, IEnumerable<T>
        where T : struct
    {
        #region Private Properties

        private readonly T[] _columns;

        private readonly Parameters _parameters;

        #endregion

        /// <summary>
        /// parameter value indexer based on parameter index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return _columns[index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object IFcsEvent.this[int index] 
        { 
            get { return _columns[index]; } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object IFcsEvent.this[string index]
        {
            get { return _columns[_parameters[index].Index]; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[string index]
        {
            get { return _columns[_parameters[index].Index]; }
        }

        /// <summary>
        /// gets length of columns
        /// </summary>
        public int Length
        {
            get { return _columns.Length; }
        }

        #region Public Methods

        /// <summary>
        /// converts object to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join("\t", _columns);
        }

        /// <summary>
        /// gets collection's enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_columns).GetEnumerator();
        }

        #endregion

        #region cntr

        /// <summary>
        /// constructs Event, it's used only for internal purposes
        /// </summary>
        public FcsEvent(T[] columns, Parameters parameters)
        {
            _columns = columns;
            _parameters = parameters;
        }

        #endregion

        #region Private Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
