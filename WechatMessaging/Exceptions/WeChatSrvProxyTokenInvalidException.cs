using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatMessaging.Exceptions
{
    public class WeChatSrvProxyTokenInvalidException:Exception
    {
        public WeChatSrvProxyTokenInvalidException()
        { }

        public WeChatSrvProxyTokenInvalidException(string message)
            : base(message)
        { }

        public WeChatSrvProxyTokenInvalidException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class WechatSrvproxytokenInvalidException : Exception
    {
        public WechatSrvproxytokenInvalidException()
        { }

        public WechatSrvproxytokenInvalidException(string message)
            : base(message)
        { }

        public WechatSrvproxytokenInvalidException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
