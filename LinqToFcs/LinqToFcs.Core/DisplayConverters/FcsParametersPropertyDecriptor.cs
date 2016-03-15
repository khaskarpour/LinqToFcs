using LinqToFcs.Core.Entities;
using System;
using System.ComponentModel;

namespace LinqToFcs.Core.DisplayConverters
{
    class FcsParametersPropertyDecriptor : PropertyDescriptor
    {
        private Parameters _parameters;

        private int _index;

        private string _displayName;

        public override AttributeCollection Attributes
        {
            get { return new AttributeCollection(null); }
        }

        public override Type ComponentType
        {
            get { return _parameters.GetType(); }
        }

        public override string DisplayName
        {
            get { return _displayName; }
        }

        public override Type PropertyType
        {
            get { return _parameters[_index].GetType(); }
        }

        public override string Name
        {
            get { return _displayName; }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public FcsParametersPropertyDecriptor(Parameters parameters, int index)
            : base("#" + index.ToString(), null)
        {
            _parameters = parameters;
            _index = index;
            _displayName = _parameters[_index].Name;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return _parameters[_index];
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
        }    }
}
