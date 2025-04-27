using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Shared.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; }
        public string FailureMessage { get; protected set; } = "";
        public Exception? Exception { get; protected set; }

        private Result(bool isSuccess,T data)
        {
            IsSuccess = isSuccess;
            Data = data;
            StatusCode = HttpStatusCode.OK;
        }
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
        protected Result(Exception ex, HttpStatusCode statusCode)
        {
            IsSuccess = false;
            Exception = ex;
            FailureMessage = ex.Message;
            StatusCode = statusCode;
        }
        public static Result<T> Success(T value) => new(true, value);
        public static Result<T> Success() => new();

        public static Result<T> Failure(string error, HttpStatusCode statusCode) => new(error, statusCode);
        public static Result<T> Failure(Exception error, HttpStatusCode statusCode) => new(error, statusCode);
        public bool IsException()
        {
            return this.Exception != null;
        }
    }
}
