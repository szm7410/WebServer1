using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WechatMessaging
{
    [DataContract]
    public class WEA3rthAuthSuiteTokenRequestMessage:WeChatRequestMessage
    {
        [DataMember(Name ="suite_id")]
        public string SuiteId { get; set;}
        [DataMember(Name ="suite_secret")]
        public string SuiteSecret { get; set; }
        [DataMember(Name ="suite_ticket")]
        public string SuiteTicket { get; set;}
    }
}
