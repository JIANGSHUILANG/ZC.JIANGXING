using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinService.Model
{
    public class ErrorModel
    {
        private int zzz;
        public static readonly string _FAIL_STATUS = 0.ToString();
        public static readonly string _SUCCESS_STATUS = 1.ToString();
        public static readonly string _SUCCESS_MESSAGE = "执行成功";
        public static readonly string _ERROR_MESSAGE = "系统异常：";
        public static readonly string _FAIL_MESSAGE = "执行失败";
        public static readonly string _PARAMETER_ERROR_MESSAGE = "参数异常";

        public int Zzz
        {
            get
            {
                return zzz;
            }

            set
            {
                zzz = value;
            }
        }
    }
}
