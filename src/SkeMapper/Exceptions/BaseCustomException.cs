using System;
using System.Globalization;

namespace SkeMapper.Exceptions
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException()
           : base() { }

        public BaseCustomException(string message)
            : base(message) { }

        public BaseCustomException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
