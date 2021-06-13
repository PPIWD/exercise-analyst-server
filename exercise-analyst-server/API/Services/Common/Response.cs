using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace API.Services.Common
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(HttpStatusCode httpStatusCode, IEnumerable<string> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        public Response(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Payload { get; set; }
    }

    public class Response
    {

        public Response(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public Response(HttpStatusCode httpStatusCode, IEnumerable<string> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}