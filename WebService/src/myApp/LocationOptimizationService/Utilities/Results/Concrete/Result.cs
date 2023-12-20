﻿using MyService.Utilities.Abstract;

namespace MyService.Utilities.Concrete
{
    public class Result : IResult
    {
        public Result(bool success,string message):this(success)
        {
            this.Message = message;
        }
        public Result(bool success)
        {
            this.Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
        
    }
}
