using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace tiebaSignIn.WebApp.Models
{
    public class TiebaSignIn
    {
        public static string MobileUserAgent = "Mozilla/5.0 (Linux; U; Android 8.1.0; zh-cn; BLA-AL00 Build/HUAWEIBLA-AL00) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/57.0.2987.132 MQQBrowser/8.9 Mobile Safari/537.36";

        #region 签到指定的贴吧（需关注此贴吧）
        public SignInResult SignIn(string tiebaName, Dictionary<string, string> cookies)
        {
            return new SignInResult();
        }
        #endregion

        #region 签到关注的贴吧
        public List<SignInResult> FollowedTiebaSignIn(Dictionary<string, string> cookies)
        {
            return new List<SignInResult>();
        }
        #endregion

        #region Http POST
        public string HttpPost(string url, string postDataStr, Dictionary<string, string> cookies = null, string userAgent = null, string referer = null, string contentType = null, List<string> headers = null)
        {
            string rtnResult = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                if (cookies != null & cookies.Count > 0)
                {
                    foreach (string name in cookies.Keys)
                    {
                        request.CookieContainer.Add(new Cookie(name, cookies[name]));
                    }
                }
                if (userAgent != null)
                {
                    request.UserAgent = userAgent;
                }
                else
                {
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
                }
                if (userAgent != null)
                {
                    request.UserAgent = userAgent;
                }
                if (referer != null)
                {
                    request.Referer = referer;
                }
                if (contentType != null)
                {
                    request.ContentType = contentType;
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                }
                if (headers != null && headers.Count > 0)
                {
                    foreach (string header in headers)
                    {
                        if (header.Contains("Host"))
                        {
                            // 提供公共API 的 Header 使用公共API设置
                            request.Host = header.Replace("Host:", "").Trim();
                        }
                        else if (header.Contains("Connection"))
                        {
                            if (header.Contains("Keep-Alive"))
                            {
                                request.KeepAlive = true;
                            }
                            else
                            {
                                request.KeepAlive = false;
                            }
                        }
                        else if (header.Contains("Accept"))
                        {
                            request.Accept = header.Replace("Accept:", "").Trim();
                        }
                        else
                        {
                            request.Headers.Add(header);
                        }
                    }
                }
                byte[] postBuffer = Encoding.UTF8.GetBytes(postDataStr);
                request.ContentLength = postBuffer.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postBuffer, 0, postBuffer.Length);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }
                using (StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    rtnResult = sReader.ReadToEnd();
                }
                responseStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rtnResult;
        }
        public string HttpPost(string url, Dictionary<string, string> postDataDic, Dictionary<string, string> cookies = null, string userAgent = null, string referer = null, string contentType = null, List<string> headers = null)
        {
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;
            // key1=value1&key2=value2&key3=value3
            foreach (string key in postDataDic.Keys)
            {
                if (isFirst)
                {
                    sb.Append(key + "=" + postDataDic[key]);
                }
                else
                {
                    sb.Append("&" + key + "=" + postDataDic[key]);
                }
            }
            string postDataStr = sb.ToString();
            return HttpPost(url: url, postDataStr: postDataStr, cookies: cookies, userAgent: userAgent, referer: referer, contentType: contentType, headers: headers);
        }
        #endregion

        #region Http GET
        public string HttpGet(string url, Dictionary<string, string> cookies = null, string userAgent = null, string referer = null, string contentType = null, List<string> headers = null)
        {
            string rtnResult = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                if (cookies != null & cookies.Count > 0)
                {
                    foreach (string name in cookies.Keys)
                    {
                        request.CookieContainer.Add(new Cookie(name, cookies[name]));
                    }
                }
                if (userAgent != null)
                {
                    request.UserAgent = userAgent;
                }
                else
                {
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
                }
                if (userAgent != null)
                {
                    request.UserAgent = userAgent;
                }
                if (referer != null)
                {
                    request.Referer = referer;
                }
                if (contentType != null)
                {
                    request.ContentType = contentType;
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                }
                if (headers != null && headers.Count > 0)
                {
                    foreach (string header in headers)
                    {
                        if (header.Contains("Host"))
                        {
                            // 提供公共API 的 Header 使用公共API设置
                            request.Host = header.Replace("Host:", "").Trim();
                        }
                        else if (header.Contains("Connection"))
                        {
                            if (header.Contains("Keep-Alive"))
                            {
                                request.KeepAlive = true;
                            }
                            else
                            {
                                request.KeepAlive = false;
                            }
                        }
                        else if (header.Contains("Accept"))
                        {
                            request.Accept = header.Replace("Accept:", "").Trim();
                        }
                        else
                        {
                            request.Headers.Add(header);
                        }
                    }
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                //如果http头中接受gzip的话，这里就要判断是否为有压缩，有的话，直接解压缩即可  
                if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }
                using (StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    rtnResult = sReader.ReadToEnd();
                }
                responseStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rtnResult;
        }
        #endregion

        #region 得到贴吧 FID
        /// <summary>
        /// 得到贴吧 FID
        /// </summary>
        /// <param name="kw">贴吧名(kw)</param>
        /// <returns>FID</returns>
        public string GetFid(string kw)
        {
            string url = "http://tieba.baidu.com/mo/m?kw=" + HttpUtility.HtmlEncode(kw);
            Dictionary<string, string> cookies = new Dictionary<string, string> { { "BAIDUID", MD5Encrypt32(GetTimeStamp()).ToUpper() } };
            string referer = "http://wapp.baidu.com/";
            string contentType = "application/x-www-form-urlencoded";
            string responseData = HttpGet(url: url, cookies: cookies, referer: referer, contentType: contentType);
            Match match = Regex.Match(responseData, "<input type=\"hidden\" name=\"fid\" value=\"(*)\"/>");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 得到TBS
        /// <summary>
        /// 得到TBS
        /// </summary>
        /// <param name="bduss"></param>
        /// <returns></returns>
        public string GetTbs(string bduss)
        {
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add("BDUSS", bduss);
            string userAgent = MobileUserAgent;
            Random r = new Random();
            string[] headers = { "X-Forwarded-For: 115.28.1." + r.Next(1, 255) };
            string responseData = HttpGet(url: "http://tieba.baidu.com/dc/common/tbs", cookies: cookies, userAgent: userAgent, referer: "http://tieba.baidu.com/", headers: headers.ToList());
            string tbs = JsonStr2Obj(responseData).tbs.ToString();
            return tbs;
        }
        #endregion

        #region 返回 （对输入的字典添加客户端验证代码 后）的字典（tiebaclient!!!）
        /// <summary>
        /// 返回 （对输入的字典添加客户端验证代码 后）的字典（tiebaclient!!!）
        /// </summary>
        /// <param name="dic"></param>
        public Dictionary<string, string> AddTiebaSign(Dictionary<string, string> dic)
        {
            Dictionary<string, string> rtnDic = new Dictionary<string, string>();
            rtnDic.Add("_client_id", "03-00-DA-59-05-00-72-96-06-00-01-00-04-00-4C-43-01-00-34-F4-02-00-BC-25-09-00-4E-36");
            rtnDic.Add("_client_type", "4");
            rtnDic.Add("_client_version", "6.0.1");
            rtnDic.Add("_phone_imei", "540b43b59d21b7a4824e1fd31b08e9a6");
            foreach (string key in dic.Keys)
            {
                rtnDic.Add(key, dic[key]);
            }

            StringBuilder sb = new StringBuilder();
            foreach (string key in rtnDic.Keys)
            {
                sb.Append(key + "=" + rtnDic[key]);
            }
            string temp = MD5Encrypt32(sb.ToString() + "tiebaclient!!!").ToUpper();
            if (rtnDic.ContainsKey("sign"))
            {
                rtnDic["sign"] = temp;
            }
            else
            {
                rtnDic.Add("sign", temp);
            }
            return rtnDic;
        }
        #endregion

        #region 50贴吧客户端一键签到
        /// <summary>
        /// 50贴吧客户端一键签到
        /// </summary>
        /// <param name="kw">贴吧名(kw)</param>
        /// <param name="bduss"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string DoSign_Onekey(string kw, string bduss, string fid)
        {
            string responseData = string.Empty;
            string url = "http://c.tieba.baidu.com/c/c/forum/msign";
            string userAgent = "bdtb for Android 6.5.8";
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add("BDUSS", bduss);
            Dictionary<string, string> tempDic = new Dictionary<string, string>();
            tempDic.Add("BDUSS", bduss);
            tempDic.Add("_client_id", "03-00-DA-59-05-00-72-96-06-00-01-00-04-00-4C-43-01-00-34-F4-02-00-BC-25-09-00-4E-36");
            tempDic.Add("_client_type", "4");
            tempDic.Add("_client_version", "1.2.1.17");
            tempDic.Add("_phone_imei", "540b43b59d21b7a4824e1fd31b08e9a6");
            tempDic.Add("fid", fid);
            tempDic.Add("kw", kw);
            tempDic.Add("net_type", "3");
            tempDic.Add("tbs", GetTbs(bduss));
            tempDic = AddTiebaSign(tempDic);

            responseData = HttpPost(url: url, postDataDic: tempDic, userAgent: userAgent, cookies: cookies);
            return responseData;
        }
        #endregion

        #region 手机网页签到
        /// <summary>
        /// 手机网页签到
        /// </summary>
        /// <param name="kw"></param>
        /// <param name="fid"></param>
        /// <param name="bduss"></param>
        /// <returns></returns>
        public string DoSign_Mobile(string kw, string fid, string bduss)
        {
            string responseData = string.Empty;
            string url = "http://tieba.baidu.com/mo/q/sign?tbs=" + GetTbs(bduss) + "&kw=" + HttpUtility.UrlEncode(kw) + "&is_like=1&fid=" + fid;
            string userAgent = MobileUserAgent;
            string referer = "http://tieba.baidu.com/f?kw=" + kw;
            string[] headers = {
                "X-Forwarded-For: 115.28.1." + (new Random()).Next(1, 255),
                "Host: tieba.baidu.com",
                "Origin: http://tieba.baidu.com",
                "Connection: Keep-Alive"
            };
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add("BDUSS", bduss);
            cookies.Add("BAIDUID", MD5Encrypt32(GetTimeStamp()).ToUpper());

            responseData = HttpGet(url: url, cookies: cookies, userAgent: userAgent, referer: referer, headers: headers.ToList());

            return responseData;
        }
        #endregion

        #region 网页签到
        /// <summary>
        /// 网页签到
        /// </summary>
        /// <param name="kw"></param>
        /// <param name="fid"></param>
        /// <param name="bduss"></param>
        /// <returns></returns>
        public bool DoSign_Default(string kw, string fid, string bduss)
        {
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add("BDUSS", bduss);
            cookies.Add("BAIDUID", MD5Encrypt32(GetTimeStamp()).ToUpper());
            string url = "http://tieba.baidu.com/mo/m?kw=" + HttpUtility.UrlEncode(kw) + "&fid=" + fid;
            string userAgent = MobileUserAgent;
            string referer = "http://wapp.baidu.com/";
            string contentType = "application/x-www-form-urlencoded";

            string tempResData = HttpGet(url: url, cookies: cookies, userAgent: userAgent, referer: referer, contentType: contentType);
            string pattern = "<td style=\"text - align:right;\"><a href=\"(.*)\">签到</a></td></tr>";
            string signInHrefValue = Regex.Match(tempResData, pattern).Groups[1].Value;
            if (!string.IsNullOrEmpty(signInHrefValue))
            {
                url = "http://tieba.baidu.com" + signInHrefValue;
                string[] headers = {
                    "Accept: text/html, application/xhtml+xml, */*",
                    "Accept-Language: zh-Hans-CN,zh-Hans;q=0.8,en-US;q=0.5,en;q=0.3"
                };
                tempResData = HttpGet(url: url, cookies: cookies, userAgent: userAgent, headers: headers.ToList());
                // 临时判断解决方案
                url = "http://tieba.baidu.com/mo/m?kw=" + HttpUtility.HtmlEncode(kw) + "&fid=" + fid;
                referer = "http://wapp.baidu.com/";
                contentType = "application/x-www-form-urlencoded";
                tempResData = HttpGet(url: url, cookies: cookies, userAgent: userAgent, referer: referer, contentType: contentType);
                pattern = "<td style=\"text - align:right;\"><span >已签到</span></td>";
                // 如果 找不到这段html 则表示 没有签到 则 返回false
                bool isSignIned = Regex.Match(tempResData, pattern).Success;
                return isSignIned;
            }
            else
            {
                // 找不到签到按钮说明 已经 签到
                return true;
            }
        }
        #endregion

        #region 返回 当前 Unix 时间戳（10位）
        /// <summary>
        /// 返回 当前 Unix 时间戳（10位）
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            long unixDate = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return unixDate.ToString();
        }
        #endregion

        #region md5加密（32位）
        /// <summary>
        /// md5加密（32位）
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string sourceStr)
        {
            string rtnStr = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(sourceStr));
            for (int i = 0; i < buffer.Length; i++)
            {
                rtnStr += buffer[i].ToString("X");
            }
            return rtnStr;
        }
        #endregion

        #region Json字符串转换为对象
        /// <summary>
        /// Json字符串转换为对象
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static dynamic JsonStr2Obj(string jsonStr)
        {
            return JsonConvert.DeserializeObject<dynamic>(jsonStr);
        }
        #endregion
    }
}