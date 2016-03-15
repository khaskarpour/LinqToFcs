using LinqToFcs.Core.Entities;
using LinqToFcs.Core.Extensions;
using LinqToFcs.Core.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LinqToFcs.Core.Enumerators
{
    internal class EventEnumerator<T> : IEnumerator<FcsEvent<T>>
        where T : struct
    {
        #region Private Properties

        private readonly Stream _stream;

        private readonly TextData _textData;

        private readonly int _eventLength;

        private readonly SerializerBase<FcsEvent<T>> _serializer;

        private int[] _mask;

        private Func<byte[]> _readLineAction;

        #endregion

        #region Public Properties

        public FcsEvent<T> Current
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public bool MoveNext()
        {
            if (_stream.Position > _textData.EndData)
            {
                return false;
            }

            var buffer = _readLineAction.Invoke();

            if (buffer == null)
            {
                return false;
            }

            Current = _serializer.Deserialize(buffer);
            return true;
        }

        public void Reset()
        {
            _stream.MoveTo(_textData.BeginData);
        }

        public void Dispose()
        {
        }

        #endregion

        #region cntr

        public EventEnumerator(Stream stream, TextData textData)
        {
            _textData = textData;
            _stream = stream;
            _eventLength = textData.Parameters.Sum(x => x.PnB / 8);

            _serializer = SerializerBase<FcsEvent<T>>.Builder(textData.Parameters);

            InitializeReadingAction();

            Reset();
        }

        private void InitializeReadingAction()
        {
            if (_textData.BYTEORD[0] == 1)
            {
                _readLineAction = new Func<byte[]>(() => _stream.ReadData(_eventLength));
            }
            else
            {
                _readLineAction = new Func<byte[]>(ReadLineIncludingConversion);

                _mask = new int[_eventLength];

                for (int i = 0; i < _eventLength; i += _textData.BYTEORD.Length)
                {
                    for (int j = 0; j < _textData.BYTEORD.Length; ++j)
                    {
                        _mask[i + j] = _textData.BYTEORD[j] + i - 1;
                    }
                }
            }
        }

        private byte[] ReadLineIncludingConversion()
        {
            var readBytes = _stream.ReadData(_eventLength);

            if (readBytes == null)
            {
                return null;
            }

            byte[] line = new byte[_eventLength];

            for (int i = 0; i < _eventLength; i++)
            {
                line[i] = readBytes[_mask[i]];
            }

            return line;
        }

        #endregion

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
