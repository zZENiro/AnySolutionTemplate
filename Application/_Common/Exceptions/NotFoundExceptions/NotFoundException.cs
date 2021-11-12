using Application._Common.Exceptions.NotFoundExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application._Common.Exceptions.NotFoundExceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(NotFoundExceptionsTypes type, string message)
        {
            Message = message;
            Type = type;
        }

        public new string Message { get; }
        public NotFoundExceptionsTypes Type { get; }
    }
}
