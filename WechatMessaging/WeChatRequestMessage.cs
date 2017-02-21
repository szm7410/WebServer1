using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace WechatMessaging
{
    public abstract class WeChatRequestMessage
    {
        public virtual string ToJsonString()
        {
            return ((JObject)JToken.FromObject(this)).ToString();
        }
    }
}
