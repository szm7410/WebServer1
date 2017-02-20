using System;
using System.Runtime.Serialization;

namespace WechatMessaging.DataType
{
    [DataContract]
    public abstract class Token
    {
        private int expiresIn;
        private DateTimeOffset expiresOn;
        [DataMember(Name ="access_token")]
        public virtual string AccessToken { get; set; }

        [DataMember(Name ="expires_in")]
        private int ExpiresIn {
            get { return expiresIn; }
            set {
                expiresIn = value;
                expiresOn = expiresIn <= 0 ? DateTimeOffset.MinValue : DateTimeOffset.UtcNow.AddSeconds(expiresIn-60);
            }
        }
        [DataMember(Name ="expires_on")]
        public DateTimeOffset ExpiresOn
        {
            get { return expiresOn; }
            set { expiresOn = value; }
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.AccessToken) && this.ExpiresOn > DateTime.UtcNow.AddMinutes(10);
            }
        }
    }
}
