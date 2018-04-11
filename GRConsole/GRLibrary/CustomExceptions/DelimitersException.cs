using System;

namespace GRLibrary
{
    public class DelimitersException : Exception
    {
        public DelimitersException()
            : base() { }

        public DelimitersException(string message)
            : base(message) { }
    }
}
