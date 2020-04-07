using System;

namespace Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ProcedureName : Attribute
    {
        public string Name { get; set; }

        public ProcedureName(string name)
        {
            Name = name;
        }
    }
}
