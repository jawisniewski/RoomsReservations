using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Shared.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public string FailureMessage { get; protected set; }
        public Exception Exception { get; protected set; }

        private Result(bool isSuccess,T data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }
        protected Result()
        {
            IsSuccess = true;
        }
        protected Result(string message)
        {
            IsSuccess = false;
            FailureMessage = message;
        }
        protected Result(Exception ex)
        {
            IsSuccess = false;
            Exception = ex;
            FailureMessage = ex.Message;
        }
        public static Result<T> Success(T value) => new(true, value);
        public static Result<T> Success() => new();

        public static Result<T> Failure(string error) => new(error); 
        public bool IsException()
        {
            return this.Exception != null;
        }
    }
}
