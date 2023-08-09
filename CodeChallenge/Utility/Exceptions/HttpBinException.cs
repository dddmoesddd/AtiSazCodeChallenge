using System;

namespace CodeChallenge.Utility.Exceptions
{
    public class HttpBinException:Exception
    {
        public HttpBinException()
        {
        }

        public HttpBinException(string message) : base(message)
        {
        }

        public HttpBinException(string message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    public class AddDataException : Exception
    {
        public AddDataException()
        {
        }

        public AddDataException(string message) : base(message)
        {
        }

        public AddDataException(string message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
