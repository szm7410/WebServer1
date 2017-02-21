using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WechatMessaging.Exceptions;

namespace WechatMessaging.HelperFunctions
{
    public class HttpHelper
    {
        public static T PostJsonObjToUrl<T>(Func<string> funcGetUrlMethod,string postData,Encoding encoding=null)
        {
            T result;
            try
            {
                result = PostJsonObjToUrl<T>(funcGetUrlMethod(), postData);
            }
            catch (WeChatSrvProxyTokenInvalidException)
            {
                result = PostJsonObjToUrl<T>(funcGetUrlMethod(), postData);
            }
            return result;
        }

        public static T PostJsonObjToUrl<T>(string url,string postData,Encoding encoding=null)
        {
            Encoding MessageBodyEncoding = Encoding.UTF8;
            if (encoding != null)
            {
                MessageBodyEncoding = encoding;
            }
            byte[] byteArry = MessageBodyEncoding.GetBytes(postData);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json;charst=utf-8";
            req.ContentLength = byteArry.Length;

            try
            {
                using (Stream dataStream = req.GetRequestStream())
                {
                    dataStream.Write(byteArry, 0, byteArry.Length);
                    dataStream.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{typeof(HttpHelper).FullName}::PostJsonObjToUrl exception, url:{url},postData:{postData},exception:{e.ToString()}");
            }

            return HandleResponse<T>((HttpWebResponse)req.GetResponse());
        }

        private static T HandleResponse<T>(HttpWebResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpHelperException(String.Format("Can not get response, http response status code = {0}",
                    response.StatusCode.ToString()));
            }

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject obj = null;
            if (!string.IsNullOrEmpty(responseString))
            {
                try
                {
                    obj = JObject.Parse(responseString);
                    JToken resValue;
                    if (obj.TryGetValue("errcode", out resValue))
                    {
                        int errCode = (int)obj["errcode"];

                        if (errCode == 42001 || errCode == 40001 || errCode == 40014)
                        {
                            throw new HttpHelperTokenInvalidException(responseString);
                        }

                        if (errCode != 0)
                        {
                            throw new HttpHelperException(responseString);
                        }
                    }
                }
                catch (KeyNotFoundException)
                {
                }

                return JsonConvert.DeserializeObject<T>(responseString);
            }
            else
            {
                throw new HttpHelperException("Get empty json string from server");
            }
        }
    }
}
