using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace MyApp.Application.Common.Results
    {
        public class Result
        {
            public bool Success { get; protected set; }

            public string? Error { get; protected set; }

            protected Result(bool success, string? error)
            {
                Success = success;
                Error = error;
            }

            public static Result SuccessResult()
                => new(true, null);

            public static Result Failure(string error)
                => new(false, error);
        }

        public class Result<T> : Result
        {
            public T? Data { get; }

            private Result(T data)
                : base(true, null)
            {
                Data = data;
            }

            private Result(string error)
                : base(false, error)
            {
            }

            public static Result<T> Success(T data)
                => new(data);

            public static new Result<T> Failure(string error)
                => new(error);
        }
    
}
