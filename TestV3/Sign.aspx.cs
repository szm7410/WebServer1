using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;
using System.Web;

using TestV3.Service;
using Microsoft.DragonGate.Common.DataType;


namespace TestV3
{
    public partial class Sign : System.Web.UI.Page
    {
        //static string dragonGateDomain = "https://msdg-edog.live.com";//edog
        static string dragonGateDomain = "https://msdg.live.com";//prod
        //static string dragonGateDomain = "https://localhost:44300";//local
        public void Page_Load(object sender, EventArgs e)
        {
            //dgsdktest(神农测试账号)
            //string dgAppId = "5db4400a-9516-45f9-a92e-2b1e6cafddef";
            //string dgAppSecrect = "b3db5d17-fc28-411f-9f37-baf8e8388a93";
            //string wechatCorpId = "wxbb8ed5f7c99d351b";
            //string wechatOpenId = "5be76cab-94a4-41c4-a1b4-0790789c1045";
            string dgAppId = HttpUtility.UrlDecode(Request.QueryString["dgAppId"]);
            string dgAppSecrect = HttpUtility.UrlDecode(Request.QueryString["dgAppSecrect"]);
            string wechatOpenId = HttpUtility.UrlDecode(Request.QueryString["WechatId"]);
            string wechatCorpId = HttpUtility.UrlDecode(Request.QueryString["wechatCorpId"]);



            #region
            //上海临港/prod
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "4e8c0f36-9b5d-495d-818e-d149fc1c19c6" }, { "appSecret", "9097a1d5-046c-437f-8394-cf8d4c637a39" }, { "wechatId", "6609e9d1-b8bd-4fea-965b-f90a6c2133e7" }, { "SubscriberUniqueId", "wx4fb065f0e1f4ec67" } };
            //北京教育学院/test
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "a75cab3e-5e6f-4791-9b31-9ec1a6b88515" }, { "appSecret", "2393cb58-7f3a-41a3-b651-9c1bf3570934" }, { "wechatId", "ceshilaoshi" }, { "SubscriberUniqueId", "wx9a0e64d49a4d6f8c" } };
            //北京教育学院/prod
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "86d1fe62-d672-4039-a204-cd77ad864462" }, { "appSecret", "8b7e6e78-b474-4479-8021-da0ce1b1edff" }, { "wechatId", "ceshilaoshi" }, { "SubscriberUniqueId", "wx9a0e64d49a4d6f8c" } };
            //北京教育学院/prod1
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "df792d90-99a7-4c96-9f7d-585e7d7b36b8" }, { "appSecret", "b43248dd-ad7f-4908-81a1-8b1f26ba4126" }, { "wechatId", "ceshilaoshi" }, { "SubscriberUniqueId", "wx9a0e64d49a4d6f8c" } };
            //北京教育学院/test1
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "a75cab3e-5e6f-4791-9b31-9ec1a6b88515" }, { "appSecret", "2393cb58-7f3a-41a3-b651-9c1bf3570934" }, { "wechatId", "ceshilaoshi" }, { "SubscriberUniqueId", "wx9a0e64d49a4d6f8c" } };
            //月亮上的花/test
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "6b4b7929-221f-4442-9beb-fcd0ce35afb2" }, { "appSecret", "fe1f3779-0b8c-4d17-a606-bb4ad1beb939" }, { "wechatId", "8dbe075e-19b1-4328-90fa-846abfc2f52d" }, { "SubscriberUniqueId", "wxf1780df29e40ec2d" } };
            //测试admin consent
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "a000e402-3e64-46af-9be6-7e530d6be69c" }, { "appSecret", "e5ad89d8-fd21-4ff9-b878-f4b267324895" }, { "wechatId", "8dbe075e-19b1-4328-90fa-846abfc2f534" }, { "SubscriberUniqueId", "wx519a73bea6983dd3" } };
            //北大附中
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "0857b59f-ca36-4683-be1a-20940af7c91e" }, { "appSecret", "10094ee1-045e-4204-a757-012f90618273" }, { "wechatId", "c2ade859-81d4-4e68-9a11-807ffa813425" }, { "SubscriberUniqueId", "wx2fabd57244d8c950" } };
            //测试oauth2
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "fadd472a-8482-405b-ab01-3790597184f6" }, { "appSecret", "f2496f99-9bb9-4263-9236-50e380100f47" }, { "wechatId", "ceshilaoshi" }, { "SubscriberUniqueId", "wx9a0e64d49a4d6f8c" } };
            //岳麓区prod
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "553b0704-de76-4cc6-9da9-6605abdc921c" }, { "appSecret", "b5a6a510-c353-4ccb-83bd-0ed74dd54740" }, { "wechatId", "92419b7b-f73e-4f78-8bb6-a8c4e740ce94" }, { "SubscriberUniqueId", "wx2a2822d197606fc7" } };
            //dgsdktest
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "3cc331ea-c2c0-4af5-aa88-d5e384129249" }, { "appSecret", "343efe32-078f-41db-825b-30d35e748dab" }, { "wechatId", "szmtest123" }, { "SubscriberUniqueId", "wxd236ae9c6f9afb7f" } };
            //bdfz
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "e647d410-acbf-42f3-9b5f-40ce088b84c3" }, { "appSecret", "205d6501-5dae-4c86-8327-65b26d2e9ae6" }, { "wechatId", "98261ed3-1703-495f-b732-aae58245017f" }, { "SubscriberUniqueId", "wx2fabd57244d8c950" } };
            //dgtest12
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "3cc331ea-c2c0-4af5-aa88-d5e384129249" }, { "appSecret", "343efe32-078f-41db-825b-30d35e748dab" }, { "wechatId", "szm61" }, { "SubscriberUniqueId", "wxd236ae9c6f9afb7f" } };
            //微软移动办公
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "75473eb9-43da-46df-addf-3af94b753059" }, { "appSecret", "8fe76b30-3276-4e38-aff7-8aa4541a24a0" }, { "wechatId", "linhao40" }, { "SubscriberUniqueId", "wx491d1bd08c38a7a0" } };
            #endregion

            //微软演示平台5db4400a-9516-45f9-a92e-2b1e6cafddef
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "5db4400a-9516-45f9-a92e-2b1e6cafddef" }, { "appSecret", "b3db5d17-fc28-411f-9f37-baf8e8388a93" }, { "wechatId", "szmtestedu345" }, { "SubscriberUniqueId", "wxbb8ed5f7c99d351b" } };
            //深圳宝安
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "4e8c0f36-9b5d-495d-818e-d149fc1c19c6" }, { "appSecret", "9097a1d5-046c-437f-8394-cf8d4c637a39" }, { "wechatId", "e437ca9d-21b1-429d-8d5d-10fd15269786" }, { "SubscriberUniqueId", "wx4fb065f0e1f4ec67" } };
            //eduforedog（edog）
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "706293d4-4857-432e-afe4-2229d76cf4ff" }, { "appSecret", "0ad2e3fe-a077-4035-b08e-ef8401913f33" }, { "wechatId", "08022e1d-1977-4296-af0b-ed14e18305aa" }, { "SubscriberUniqueId", "wx08c5cb302d6f8cc8" } };
            //dgtest11 辽宁科技大学（prod）
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "2A8D58D5-E08E-47F1-85A7-E4A9B23476FB" }, { "appSecret", "592b4dfd-ddb2-45d9-9fe1-df0e2a54b147" }, { "wechatId", "admin111" }, { "SubscriberUniqueId", "wx59b44950939e2fda" } };
            //泰达
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "28f0e4d1-2769-4dc7-a3c2-339e63aab33a" }, { "appSecret", "6f487697-f5f9-4fee-97ba-57d76ac59647" }, { "wechatId", "910505d3-6231-4a9b-9edd-c3641ebc2bde" }, { "SubscriberUniqueId", "wxbec9bec3d4e7f3fe" } };
            //edog trial
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "00208799-9760-480E-97CD-CB643FBC31E4" }, { "appSecret", "6b7ec2b6-4aaa-47b5-8a00-266cdf80e4dc" }, { "wechatId", "437121e2-8485-4156-a145-45be654183e7" }, { "SubscriberUniqueId", "wxbe9587a6604a5fdc" } };
            //onebox2
            //Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "1cd12644-04e9-45a4-83cd-b1b9042902d7" }, { "appSecret", "2b7e5127-4bdd-40c1-9353-72f08b0dc42d" }, { "wechatId", "testuser00418852422" }, { "SubscriberUniqueId", "wxd710ecaf02e96afd" } };

            //songbo
            Dictionary<string, string> dgTokenheaders = new Dictionary<string, string> { { "appId", "c7e5807f-1974-46de-9e88-7682e1f08ba6" }, { "appSecret", "20dfe8ba-03e9-456b-b8bd-477c412b4c50" }, { "wechatId", "f5f4377e-285e-4561-b0ea-5c7207465bd5" }, { "SubscriberUniqueId", "wxbd33100e7aeab0f5" } };

            string requestUrl = string.Format("{0}/v1.0/dgtoken", dragonGateDomain);
            //string requestUrl = string.Format("{0}/api/core/getdgtoken", dragonGateDomain);
            string dragonGateTokenStr = GetValueFromRequest(requestUrl, dgTokenheaders);
            dynamic result = JsonConvert.DeserializeObject(dragonGateTokenStr);
            if (result != null && result.ErrorCode != null)
            {

            }

            DragonGateToken dgToken = new DragonGateToken(result.token.ToString());

            O365UserToken token = GetO365UserToken(dgToken);
            string redirectUrl = "http://localhost:1270/O365WeaPage.aspx?complate=1";
            Util.RedirectUserToSignIn(dgToken.Token, redirectUrl);
            #region Trial注册
            ////login

            //string complate = HttpUtility.UrlDecode(Request.QueryString["complate"]);
            //if (string.IsNullOrEmpty(complate))
            //{
            //    DragonGateToken dgToken = null;
            //    dgToken = GetDragonGateToken(dgAppId, dgAppSecrect, wechatOpenId, wechatCorpId);
            //    string redirectUrl = "http://localhost:1270/O365WeaPage.aspx?complate=1";
            //    TestV3.Models.Util.RedirectUserToSignIn(dgToken.Token, redirectUrl);
            //}

            #endregion

        }


        private static string GetValueFromRequest(string url, Dictionary<string, string> headers = null, string postData = "")
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            if (headers != null)
            {
                foreach (string item in headers.Keys)
                {
                    request.Headers.Add(item, headers[item]);
                }
            }
            if (!string.IsNullOrWhiteSpace(postData))
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(postData);
                request.Method = "POST";
                // Set the content type of the data being posted.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the content length of the string being posted.
                request.ContentLength = bytes.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Close();
                }
            }
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream stream = response.GetResponseStream())
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }

        public static O365UserToken GetO365UserToken(DragonGateToken dragonGateToken)
        {
            //string requestUrl = string.Format("{0}/api/IDMapper/GetO365Token/3", dragonGateDomain);
            string requestUrl = string.Format("{0}/v1.0/idmapper/o365token?resourcetype=4", dragonGateDomain);
            Dictionary<string, string> headers = new Dictionary<string, string> { { "dgToken", dragonGateToken.Token } };
            try
            {
                string o365Token = GetValueFromRequest(requestUrl, headers);
                dynamic result = JsonConvert.DeserializeObject(o365Token);
                if (result != null && result.ErrorCode != null)
                {
                    throw new Exception(string.Format("Service Unavailable, please contact service provider. dgToken {0}   ", dragonGateToken.Token));
                }
                //O365UserToken o365UserToken = new O365UserToken(result.access_token.ToString(), DateTimeOffset.Parse(result.expires_on.ToString()), result.refresh_token.ToString());
                string token = result.access_token.ToString();
                string refreshtoken = result.refresh_token.ToString();
                string resource_url = result.resource_url.ToString();
                O365UserToken o365UserToken = new O365UserToken() { AccessToken=token,RefreshToken=refreshtoken,ResourceUrl=resource_url};
                return o365UserToken;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DragonGateToken GetDragonGateToken(string appId, string appSecret, string wechatId, string corpId)
        {
            string requestUrl = string.Format("{0}/v1.0/dgtoken", dragonGateDomain);
            //string requestUrl = string.Format("{0}/api/core/getdgtoken", dragonGateDomain);
            Dictionary<string, string> headers = new Dictionary<string, string> { { "appId", appId }, { "appSecret", appSecret }, { "wechatId", wechatId }, { "SubscriberUniqueId", corpId } };
            try
            {
                string dragonGateTokenStr = GetValueFromRequest(requestUrl, headers);
                dynamic result = JsonConvert.DeserializeObject(dragonGateTokenStr);
                if (result != null && result.ErrorCode != null)
                {
                    throw new Exception(string.Format("Service Unavailable, please contact service provider: appid {0}, appsecet {1}, wechatopenid {2}", appId, appSecret, wechatId));
                }

                DragonGateToken dgToken = new DragonGateToken(result.token.ToString());
                return dgToken;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public SNHttpWebResponse PostRequest(string serviceUrl, Dictionary<string, string> headers = null, string postData = "", string contentType = "", List<Cookie> cookies = null, string accept = "")
        {
            return CommonHttpRequest(serviceUrl, WebRequestMethods.Http.Post, headers, postData, contentType, cookies, accept);
        }

        public SNHttpWebResponse CommonHttpRequest(string serviceUrl, string type, Dictionary<string, string> headers = null, string postData = "", string contentType = "", List<Cookie> cookies = null, string accept = "")
        {
            HttpWebRequest request = WebRequest.Create(serviceUrl) as HttpWebRequest;
            if (headers != null)
            {
                foreach (string item in headers.Keys)
                {
                    request.Headers.Add(item, headers[item]);
                }
            }

            if (cookies != null)
            {
                if (request.CookieContainer == null)
                {
                    request.CookieContainer = new CookieContainer();
                }

                foreach (var c in cookies)
                {
                    request.CookieContainer.Add(c);
                }
            }

            request.Method = type;
            if (!string.IsNullOrWhiteSpace(accept))
            {
                request.Accept = accept;
            }

            if (!string.IsNullOrWhiteSpace(postData))
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(postData);
                request.ContentLength = bytes.Length;

                if (string.IsNullOrWhiteSpace(contentType))
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                else
                {
                    request.ContentType = contentType;
                }

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Close();
                }
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string responseStream = GetResponseString(response);
            return new SNHttpWebResponse(response.StatusCode, response.StatusDescription, responseStream, response);
        }

        public static string GetResponseString(HttpWebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                return new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            }
        }
    }


    public class SNHttpWebResponse
    {

        public HttpStatusCode StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        public string ResponseStream { get; private set; }

        public HttpWebResponse OriginalResponse { get; private set; }

        public SNHttpWebResponse(HttpStatusCode statusCode, string statusDescription, string respoinseStream, HttpWebResponse originalResponse)
        {
            this.StatusCode = statusCode;

            this.StatusDescription = statusDescription;

            this.ResponseStream = respoinseStream;

            this.OriginalResponse = originalResponse;
        }

    }
}