﻿namespace MyService.Utilities.Abstract
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}
