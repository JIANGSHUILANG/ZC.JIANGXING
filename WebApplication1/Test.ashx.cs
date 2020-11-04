using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using JiangXinService;
using JiangXinService.Module;

namespace WebApplication1
{
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    public class Test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string _SQLCONN2 = "Database='v06';Data Source='218.93.219.194';User Id='root';Password='123456';charset='utf8';pooling=true;port=9988;";

            context.Response.ContentType = "application/json";
            //由于前端axiso 配置了断点续传的配置，所有用如下方法来获取请求参数
            Sfcdatequipmentspecies ss = new Sfcdatequipmentspecies();
            //{ "code":"d5dde9d2-9ac7-4d61-87db-89a129974683","name":"lj","parentcode":"001",
            //    "parentname":"仪器设备","internalcode":"lj","warrantymonth":12,"scrapmonth":120,
            //    "depreciationwayname":"平均折旧方法","depreciationwayid":1,"salvage":3,"depreciationmonth":120}
            ss.code = "d5dde9d2-9ac7-4d61-87db-89a129974683";
            ss.name = "lj";
            ss.parentcode = "002";
            ss.parentname = "仪器设备";
            ss.internalcode = "lj";
            ss.warrantymonth = 12;
            ss.scrapmonth = "120";
            ss.depreciationwayname = "平均折旧方法";
            ss.depreciationwayid = "1";
            ss.salvage = "3";
            ss.depreciationmonth = "120";
            ss.Creator = "江";
            EquipmentparameterModule em = new EquipmentparameterModule();

           // var cc=em.AutoSetEquipmentSpecieCodeTest(_SQLCONN2, ss);


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