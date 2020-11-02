using JiangXinService;
using JiangXinService.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            //EquipmentparameterModule em = new EquipmentparameterModule();
            //var model = new EquipmentModel();
            //model.FuncName = "SummaryByDepartment";
            ////model.Usetime = "2020-10-23";

            //em.model = model;
            //em.GetEquiparameter();
            string reg = "W0100100257;100;2020/09/29;2021/09/28;Y-Bond S220(10g/5cc);8229IM-04;";
           var match=  Regex.Matches(reg, @"/(?<=^).*?(?=\;)//(?<=;)[^;]+(?=;$)/");
            foreach (var item in match)
            {
                int i = 0;
            }
            var match1 = Regex.Matches(reg, "(?<=).*?(?=;)");
            foreach (var item in match1)
            {
                int z = 0;

            }

            string reg2 = "He is. The dog ran.The sun is out.";
            var match3 = Regex.Matches(reg2, @"\w+(?=\.)");

            Console.ReadKey();
            int aaaa = 0;
        }
    }
}
