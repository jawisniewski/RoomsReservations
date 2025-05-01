using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Shared.Common
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public string FailureMessage { get; protected set; } = "";
        public Exception? Exception { get; protected set; }

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

        public static Result Success() => new Result();
        public static Result Failure(string error, HttpStatusCode statusCode) => new Result(error, statusCode);
        public static Result Failure(Exception error, HttpStatusCode statusCode) => new Result(error, statusCode);

        public bool IsException()
        {
            return this.Exception != null;
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        private Result(bool isSuccess, T data) : base()
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        private Result() : base() { }

        private Result(string message, HttpStatusCode statusCode) : base(message, statusCode) { }

        private Result(Exception ex, HttpStatusCode statusCode) : base(ex, statusCode) { }

        public static new Result<T> Success(T value) => new Result<T>(true, value);
        public static new Result<T> Success() => new Result<T>();

        public static new Result<T> Failure(string error, HttpStatusCode statusCode) => new Result<T>(error, statusCode);
        public static new Result<T> Failure(Exception error, HttpStatusCode statusCode) => new Result<T>(error, statusCode);
    }
}
