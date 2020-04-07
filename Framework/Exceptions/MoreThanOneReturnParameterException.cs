using System;

namespace Framework.Exceptions
{
    public class MoreThanOneReturnParameterException : Exception
    {
        public MoreThanOneReturnParameterException(string message) : base(message)
        {
        }

        public MoreThanOneReturnParameterException()
        {
        }
    }
}
