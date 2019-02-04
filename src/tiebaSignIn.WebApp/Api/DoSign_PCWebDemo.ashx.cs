using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using tiebaSignIn.WebApp.Models;
using Newtonsoft.Json;

namespace tiebaSignIn.WebApp.Api
{
    /// <summary>
    /// Summary description for DoSign_PCWebDemo
    /// </summary>
    public class DoSign_PCWebDemo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            TiebaSignIn tiebaSignIn = new TiebaSignIn();
            string kw = context.Request.QueryString["kw"];
            string bduss = context.Request.QueryString["bduss"];
            if (string.IsNullOrEmpty(kw) || string.IsNullOrEmpty(bduss))
            {
                context.Response.Write("请在url中输入贴吧名和BDUSS");
            }
            else
            {
                SignInResult result = tiebaSignIn.DoSign_PCWeb(kw, bduss);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                // 设置 对象转换为 json字符串时，属性名 小驼峰式转换
                settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                string jsonStr = JsonConvert.SerializeObject(result, settings);
                context.Response.Write(jsonStr);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}