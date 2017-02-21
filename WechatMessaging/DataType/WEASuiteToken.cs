using System;
using System.Runtime.Serialization;

namespace WechatMessaging.DataType
{
    [DataContract]
    public class WEASuiteToken:Token
    {
        [DataMember(Name ="suite_access_token")]
        public virtual string SuiteAccessToken { get; set; }
    }
}
