using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatMessaging.Exceptions
{
    public class HttpHelperException:Exception
    {
        public HttpHelperException()
        { }

        public HttpHelperException(string message):base(message)
        {
        }
        public HttpHelperException(string message, Exception inner) : base(message, inner)
        { }
    }

    public class HttpHelperTokenInvalidException : Exception
    {
        public HttpHelperTokenInvalidException()
        { }

        public HttpHelperTokenInvalidException(string message)
            : base(message)
        { }

        public HttpHelperTokenInvalidException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
