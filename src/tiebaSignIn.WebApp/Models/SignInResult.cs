using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tiebaSignIn.WebApp.Models
{
    public enum SignInCode
    {
        /// <summary>
        /// 签到成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 已经签过
        /// </summary>
        SignIned = 2,
        /// <summary>
        /// 签到失败：其它异常
        /// </summary>
        Failure = 3
    }

    public class SignInResult
    {
        public SignInCode Code { get; set; }

        public string Message { get; set; }
    }
}