using System.Net;
using System.Text.Json.Serialization;

namespace Elasticsearch.API.Dtos
{
    public class ResponseDto<T>
    {
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        public static ResponseDto<T> Success(T data, HttpStatusCode statusCode) => new ResponseDto<T> { Data = data, StatusCode = statusCode };
        public static ResponseDto<T> Fail(List<string> errors, HttpStatusCode statusCode) => new ResponseDto<T> { Errors = errors, StatusCode = statusCode };


    }
}
