using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static Result Success(string message = "Thành công.")
        {
            return new Result { IsSuccess = true, Message = message };
        }

        public static Result Failure(string message = "Thất bại.")
        {
            return new Result { IsSuccess = false, Message = message };
        }
    }



}
