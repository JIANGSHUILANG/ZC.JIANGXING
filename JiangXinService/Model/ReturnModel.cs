using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC.DAL.BLL;

namespace JiangXinService.Model
{
    public class ReturnModel
    {
        public object item { get; set; }
        public string MsgCode { get; set; }
        public string ReturnCnt { get; set; }
        public string Status { get; set; }
        public string _ReturnMsg { get; set; }
        public ReturnModel(ReturnMsg retMsg)
        {
            "".ToString();
        }
    }
}
