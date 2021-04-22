using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace API.Services.Common
{
    public class Response<T>
    {
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Payload { get; set; }
    }

    public class Response
    {
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}