using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatMessaging.Exceptions
{
    public class WeChatSrvProxyException :Exception
    {
        public WeChatSrvProxyException()
        { }
        public WeChatSrvProxyException(string message) : base(message)
        { }
        public WeChatSrvProxyException(string message, Exception inner) :
            base(message, inner)
        { }
    }
}
