using System;
using System.Runtime.Serialization;

namespace WechatMessaging.DataType
{
    [DataContract]
    public class WeChatUserInfoToken:WeChatToken
    {
        [DataMember(Name ="refresh_token")]
        public string RefreshToken { get; set; }
        [DataMember(Name ="openid")]
        public string OpenId { get; set; }
        [DataMember(Name ="scope")]
        public string Scope { get; set; }
        [DataMember(Name ="unionid")]
        public string UnionId { get; set; }
    }
}
