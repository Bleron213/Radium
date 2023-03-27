using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Radium.Shared.Utils.Errors
{
    public abstract class CustomError : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public CustomError(HttpStatusCode httpStatusCode, string errorMessage)
        {
            ErrorMessage = errorMessage;
            StatusCode = httpStatusCode;
        }

        public CustomError(string errorMessage) : this(HttpStatusCode.BadRequest, errorMessage)
        {
        }

        public abstract List<KeyValuePair<string, string>> SerializeErrors();

    }

}
