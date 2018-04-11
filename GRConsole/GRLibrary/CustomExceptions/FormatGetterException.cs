using System;

namespace GRLibrary
{
    public class FormatGetterException : Exception
    {
        public FormatGetterException()
            : base() { }

        public FormatGetterException(string message)
            : base(message) { }             
    }
}
