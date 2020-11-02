using JiangXinService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC.DAL.BLL;

namespace JiangXinService.Model
{
    public interface IBaseModule<T> 
    {
        ReturnModel MainTest(string sqlConn, object Parameter);
        ReturnModel Main(InvokeEntity Parameter);
    }
}
