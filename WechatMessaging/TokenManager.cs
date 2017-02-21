using System;
using System.Security;
using System.Net;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

using WechatMessaging.DataType;
using WechatMessaging.Exceptions;
using WechatMessaging.HelperFunctions;

namespace WechatMessaging
{
    public abstract class TokenManager
    {
        protected Token _token;
        private string _appId;
        private SecureString _appSecret = new SecureString();
        protected string suiteTicket;
        public string AppId { get { return this._appId; } }
        public Token Token
        {
            get
            {
                this.RefreshToken();
                return this._token;
            }
        }

        public void EnforceToRefreshToken()
        {
            this.RefreshToken(true);
        }

        protected abstract void RefreshToken(bool bForce = false);

        public TokenManager(string appId, string appSecret)
        {
            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentException("appId or appSecret can not be null or emppty");
            }

            foreach (char c in appSecret.ToCharArray())
            {
                this._appSecret.AppendChar(c);
            }
            this._appSecret.MakeReadOnly();
            this._appId = appId;
            this._token = null;
        }

        public TokenManager(string appId, string appSecret, string suiteTicket)
        {
            this.suiteTicket = suiteTicket;
        }

        public TokenManager(string appId, string accessToken, DateTimeOffset expireOn)
        {
            this._appId = appId;
            this._token = new WeChatAccessToken();
            this._token.AccessToken = accessToken;
            this._token.ExpiresOn = expireOn;
        }

        public TokenManager(string appId, string appSecret, string accessToken, DateTimeOffset expireOn) : this(appId, appSecret)
        {
            this._appId = appId;
            this._token = new WeChatAccessToken();
            this._token.AccessToken = accessToken;
            this._token.ExpiresOn = expireOn;
        }

        protected string GetResponseStringFromUrl(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException(String.Format("Can not get response,http response status code = {0}",
                    response.StatusCode.ToString()));
            }

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        protected string GetAppSecret()
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(this._appSecret);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }

    public class WEATokenManager : TokenManager
    {
        private const string requestAccessTokenUrl = @"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";
        protected string errMg = "Can not get wechat access token , see inner for details";

        public WEATokenManager(string appId, string appSecret) : base(appId, appSecret)
        {
        }

        protected override void RefreshToken(bool bForce = false)
        {
            if (!bForce && this._token != null && this._token.IsValid)
            {
                return;
            }

            string response = this.GetResponseStringFromUrl(string.Format(requestAccessTokenUrl, this.AppId, this.GetAppSecret()));
            this._token = JsonConvert.DeserializeObject<WeChatAccessToken>(response);
        }
    }

    public class WEAGroupChatTokenManager : WEATokenManager
    {
        public WEAGroupChatTokenManager(string appId, string appSecret) : base(appId, appSecret)
        { }
    }

    public class WSATokenManager : TokenManager
    {
        private const string requestAccesstokenUrl = @"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        private bool tokenGetFromOut = false;

        public WSATokenManager(string appId, string appSecret) : base(appId, appSecret)
        { }

        public WSATokenManager(string accessToken, DateTimeOffset expireOn, string key, string appSecret) :
            base(key, appSecret, accessToken, expireOn)
        {
            tokenGetFromOut = true;
        }

        protected override void RefreshToken(bool bForce = false)
        {
            if (!bForce && this._token != null && this._token.IsValid)
            {
                return;
            }
            if (tokenGetFromOut)
            {
                return;
            }

            string response = this.GetResponseStringFromUrl(string.Format(requestAccesstokenUrl, this.AppId, this.GetAppSecret()));
            this._token = JsonConvert.DeserializeObject<WeChatAccessToken>(response);
            if (_token == null || string.IsNullOrEmpty(_token.AccessToken))
            {
                throw new Exception($"WSATokenManager::RefreshToken appId:{AppId},response:{response}");
            }
        }
    }

    //

    public class WSAUserInfoAccessTokenManager : TokenManager
    {
        private const string requestAccessTokenUrl = @"https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        private const string refreshAccessTokenUrl = @"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type=refresh_token&refresh_token={1}";
        private const string verifyOpenIdUrl = @"https://api.weixin.qq.com/sns/auth?access_token={0}&openid={1}";

        private string _openId;
        private string _code;
        public string Scope;
        public string UnionId;

        public string OpenId
        {
            get
            {
                if (!string.IsNullOrEmpty(this._openId))
                {
                    string response = this.GetResponseStringFromUrl(string.Format(verifyOpenIdUrl, this._token.AccessToken, this._openId));
                    dynamic obj = JsonConvert.DeserializeObject(response);
                    int errorCode = (int)obj["errcode"];
                    if (errorCode != 0)
                    {
                        throw new WeChatSrvProxyException(string.Format("error code: {0},error msg: {1}", (int)obj["errcode"], obj["errmsg"] as string));
                    }
                }
                return this._openId;
            }
        }

        public WSAUserInfoAccessTokenManager(string appId, string appSecret, string code)
            : base(appId, appSecret)
        {
            this._code = code;
            this.RefreshToken();
        }

        public WSAUserInfoAccessTokenManager(string accessToken, DateTimeOffset expireOn, string key, string code) :
            base(key, accessToken, expireOn)
        {
            this._code = code;
        }

        protected override void RefreshToken(bool bForce = false)
        {
            if (this._token != null && this._token.IsValid)
            {
                return;
            }
            if (this._token != null)
            {
                string response = this.GetResponseStringFromUrl(string.Format(refreshAccessTokenUrl, this.AppId, this._token.AccessToken));
                this.HandleTokenResponse(response);
            }
            else
            {
                string response = this.GetResponseStringFromUrl(string.Format(requestAccessTokenUrl, this.AppId, this.GetAppSecret(), this._code));
            }
        }

        private void HandleTokenResponse(string response)
        {
            this._token = JsonConvert.DeserializeObject<WeChatUserInfoToken>(response);
            this._openId = ((WeChatUserInfoToken)this._token).OpenId;
            this.Scope = ((WeChatUserInfoToken)this._token).Scope;
            this.UnionId = ((WeChatUserInfoToken)this._token).UnionId;
        }
    }

    public class WEA3rthAuthTokenManager : TokenManager
    {
        private const string requestAccessTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/service/get_suite_token";
        public WEA3rthAuthTokenManager(string suiteId, string suiteSecret, string suiteTicket) :
            base(suiteId, suiteSecret, suiteTicket)
        { }
        protected override void RefreshToken(bool bForce = false)
        {
            if (!bForce && _token != null && _token.IsValid)
            {
                return;
            }

            WEA3rthAuthSuiteTokenRequestMessage msg = new WEA3rthAuthSuiteTokenRequestMessage()
            {
                SuiteId = AppId,
                SuiteSecret = GetAppSecret(),
                SuiteTicket = suiteTicket
            };

            WEASuiteToken stoken = HttpHelper.PostJsonObjToUrl<WEASuiteToken>(requestAccessTokenUrl, msg.ToJsonString());
            stoken.AccessToken = stoken.SuiteAccessToken;

            this._token = stoken;
        }
    }

    public class WEATokenFromPermCodeManager : TokenManager
    {
        public WEATokenFromPermCodeManager(string appId, string appSecret, Token token) :
            base(appId, appSecret, "")
        {
            _token = token;
        }

        protected override void RefreshToken(bool bForce = false)
        {
            if (!bForce && _token != null && _token.IsValid)
            {
                return;
            }
        }
    }

    public class WEATokenInstance : TokenManager
    {
        public WEATokenInstance(string accessToken, DateTimeOffset expireOn, string key) :
            base(key, accessToken, expireOn)
        { }

        protected override void RefreshToken(bool bForce = false)
        {
            if (!bForce && _token != null && _token.IsValid)
            {
                return;
            }
        }
    }
}
