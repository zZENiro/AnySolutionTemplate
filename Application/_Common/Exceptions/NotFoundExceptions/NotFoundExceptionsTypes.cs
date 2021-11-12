using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application._Common.Exceptions.NotFoundExceptions
{
    public enum NotFoundExceptionsTypes
    {
        Account =   0b0000_0000_0000_0001,
        User    =   0b0000_0000_0000_0010,
        Role    =   0b0000_0000_0000_0100,
    }
}
