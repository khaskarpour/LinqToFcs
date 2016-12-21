using LinqToFcs.Core.DisplayConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LinqToFcs.Core.Entities
{
    public class Parameters : List<ParameterData>, ICloneable, ICustomTypeDescriptor
    {
        /// <summary>
        /// finds parameter by it's long name
        /// </summary>
        /// <param name="longname"></param>
        /// <returns></returns>
        public ParameterData this[string longname]
        {
            get { return this.FirstOrDefault(x => x.LongName.ToLower() == longname.ToLower()); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Parameters()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public Parameters(IEnumerable<ParameterData> parameters)
            :base(parameters)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pars2"></param>
        /// <returns></returns>
        protected bool Equals(Parameters pars2)
        {
            var primaryParameterCheckResult = this != null &&
                pars2 != null &&
                this.Count == pars2.Count;

            if (!primaryParameterCheckResult)
            {
                return false;
            }

            return this
                .Select((x, index) => new { Parameter = x, Index = index })
                .All(x => pars2[x.Index] == x.Parameter);
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

            return obj.GetType() == GetType() && Equals((Parameters) obj);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Parameters(this
                .Select(x => (ParameterData)x.Clone())
                .ToList());
        }

        #region ICustomTypeDescriptor implementation

        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        /// <summary>
        /// Called to get the properties of this type. Returns properties with certain
        /// attributes. this restriction is not implemented here.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        /// <summary>
        /// Called to get the properties of this type.
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties()
        {
            return new PropertyDescriptorCollection(
                this.Select((x, index) => new FcsParametersPropertyDecriptor(this, index)).ToArray());
        }

        #endregion
    }
}
