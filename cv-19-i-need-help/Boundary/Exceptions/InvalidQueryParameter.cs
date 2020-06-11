using System;

namespace CV19INeedHelp.Boundary.Exceptions
{
    public class InvalidQueryParameter : Exception
    {
        public InvalidQueryParameter(string message)
            : base(message)
        {
        }
    }
}