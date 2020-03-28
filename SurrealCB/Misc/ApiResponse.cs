using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace SurrealCB.Server.Misc
{
    [Serializable]
    [DataContract]
    public class ApiResponse
    {
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public int StatusCode { get; set; } = 0;

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Exception ResponseException { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        [JsonConstructor]
        public ApiResponse(int statusCode, string message = "", object result = null, Exception apiError = null, string apiVersion = "0.7.0")
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
            ResponseException = apiError;
            Version = apiVersion;
            IsError = false;
        }

        public ApiResponse(int statusCode, Exception apiError)
        {
            StatusCode = statusCode;
            Message = apiError.Message;
            ResponseException = apiError;
            IsError = true;
        }
    }
}
