using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Radium.Shared.Utils.Responses
{
    public class ErrorDetails 
    {
        public string? ErrorMessage { get; set; }
        public string? ErrorExceptionMessage { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<KeyValuePair<string, string>> Errors { get; set; } = new List<KeyValuePair<string, string>>();
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
