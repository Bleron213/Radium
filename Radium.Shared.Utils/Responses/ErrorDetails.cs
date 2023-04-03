using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Radium.Shared.Utils.Responses
{
    public class ErrorDetails : Error
    {
        public bool Succeeded { get; set; }
        public List<Error> Errors { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
        public string ErrorExceptionMessage { get; set; }
    }
}
