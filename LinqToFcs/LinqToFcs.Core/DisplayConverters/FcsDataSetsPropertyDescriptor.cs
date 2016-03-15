using System;
using System.ComponentModel;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsDataSetsPropertyDescriptor : PropertyDescriptor
    {
        private FcsDataSets _dataSets;

        private int _index;

        private string _displayName;

        public override AttributeCollection Attributes
        {
            get { return new AttributeCollection(null); }
        }

        public override Type ComponentType
        {
            get { return _dataSets.GetType(); }
        }

        public override string DisplayName
        {
            get { return _displayName; }
        }

        public override Type PropertyType
        {
            get { return _dataSets[_index].GetType(); }
        }

        public override string Name
        {
            get { return _displayName; }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public FcsDataSetsPropertyDescriptor(FcsDataSets dataSets, int index)
            : base("#" + index.ToString(), null)
        {
            _dataSets = dataSets;
            _index = index;
            _displayName = string.Format("DataSet {0}", _index);
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return _dataSets[_index];
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
        }
    }
}
