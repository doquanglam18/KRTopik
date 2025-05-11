using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services
{
    public class ResultV<T> : Result
    {
        public T Data { get; set; }

        public static ResultV<T> Success(T data, string message = "Thành công.")
        {
            return new ResultV<T> { IsSuccess = true, Data = data, Message = message };
        }

        public new static ResultV<T> Failure(string message = "Thất bại.")
        {
            return new ResultV<T> { IsSuccess = false, Data = default, Message = message };
        }
    }

}
