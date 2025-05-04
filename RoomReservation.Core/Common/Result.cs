using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RoomReservation.Shared.Common
{
    /// <summary>
    /// API response with data, message, and status.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        /// <example>false</example>
        public bool IsSuccess { get; protected set; }

        /// <summary>
        /// HTTP status code of the response.
        /// </summary>
        /// <example>422</example>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; protected set; }

        /// <summary>
        /// Message of failure.
        /// </summary>
        /// <example>Message</example>
        public string FailureMessage { get; protected set; } = "";

        protected Result()
        {
            IsSuccess = true;
            StatusCode = HttpStatusCode.OK;
        }

        protected Result(string message, HttpStatusCode statusCode)
        {
            IsSuccess = false;
            FailureMessage = message;
            StatusCode = statusCode;
        }

        public static Result Success() => new Result();
        public static Result Failure(string error, HttpStatusCode statusCode) => new Result(error, statusCode);
    }

    /// <inheritdoc cref="Result"/>/>
    public class Result<T> : Result
    {
        /// <summary>
        /// Payload of the response.
        /// </summary>
        public T? Data { get; }

        private Result(bool isSuccess, T data) : base()
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        private Result() : base() { }

        private Result(string message, HttpStatusCode statusCode) : base(message, statusCode) { }

        private Result(Exception ex, HttpStatusCode statusCode) : base(ex.Message, statusCode) { }

        public static new Result<T> Success(T value) => new Result<T>(true, value);
        public static new Result<T> Success() => new Result<T>();

        public static new Result<T> Failure(string error, HttpStatusCode statusCode) => new Result<T>(error, statusCode);
        public static new Result<T> Failure(Exception error, HttpStatusCode statusCode) => new Result<T>(error, statusCode);
    }
}
