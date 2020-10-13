using JiangXinService;
using JiangXinService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC.DAL.BLL;

namespace JiangXinTest
{
    class Program
    {

       static string sqlConn = "Database='v06';Data Source='218.93.219.194';User Id='root';Password='123456';charset='utf8';pooling=true;port=9988;";

        static string creator = "ZZDTest";

        static string token = "v0612345678";
        static void Main(string[] args)
        {
            //TestRocketMQ testRocketMQ = new TestRocketMQ();
            //testRocketMQ.runtest(null);

            EquipmentparameterModule module = new EquipmentparameterModule();
            ParameterModel model = new ParameterModel();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.FuncName = "GetEquipmentMaintenanceWarning";
            model.AdvanceDays = 6;
            module.GetEquipmentWarning(sqlConn, model);
        }
    }
}
