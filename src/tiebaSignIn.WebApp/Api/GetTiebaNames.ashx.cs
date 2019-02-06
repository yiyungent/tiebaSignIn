using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using tiebaSignIn.WebApp.Models;
using Newtonsoft.Json;

namespace tiebaSignIn.WebApp.Api
{
    /// <summary>
    /// Summary description for GetTiebaNamesByBduss
    /// </summary>
    public class GetTiebaNames : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (!string.IsNullOrEmpty(context.Request.QueryString["bduss"]))
            {
                // 失败
                TiebaSignIn tiebaSignIn = new TiebaSignIn();
                List<string> tiebaNames = tiebaSignIn.GetFollowedTiebaNamesByBduss(context.Request.QueryString["bduss"]);
                string jsonStr = JsonConvert.SerializeObject(tiebaNames);
                context.Response.Write(jsonStr);
            }
            else if (!string.IsNullOrEmpty(context.Request.QueryString["userName"]))
            {
                // 成功
                TiebaSignIn tiebaSignIn = new TiebaSignIn();
                List<string> tiebaNames = tiebaSignIn.GetFollowedTiebaNames(context.Request.QueryString["userName"]);
                string jsonStr = JsonConvert.SerializeObject(tiebaNames);
                context.Response.Write(jsonStr);
            }
            else
            {
                context.Response.Write("请提供bduss或userName");
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