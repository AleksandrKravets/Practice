using System;

namespace Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class ProcedureParameter : Attribute
    {
    }
}
