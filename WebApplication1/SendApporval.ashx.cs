using JiangXinService;
using JiangXinService.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// SendApporval 的摘要说明
    /// </summary>
    public class SendApporval : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            //由于前端axiso 配置了断点续传的配置，所有用如下方法来获取请求参数
            var pams = System.Text.Encoding.UTF8.GetString(context.Request.BinaryRead(context.Request.TotalBytes));
            ApprovalModule arrpoval = new ApprovalModule();
            arrpoval.InitiateProcess(1,"11");
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