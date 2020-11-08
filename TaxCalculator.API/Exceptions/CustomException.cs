using System;

namespace TaxCalculator.RESTAPI.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException()
        {

        }

        public CustomException(string exception, int statusCode)
            : base(exception)
        {
            this.StatusCode = statusCode;
        }
    }
}
