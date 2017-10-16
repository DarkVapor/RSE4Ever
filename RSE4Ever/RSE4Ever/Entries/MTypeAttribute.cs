using System;

namespace RSETechnofuturTIC.Entries
{
    internal class MTypeAttribute : Attribute
    {
        private string _value;

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public MTypeAttribute(string _v)
        {
            Value = _v;
        }
    }
}