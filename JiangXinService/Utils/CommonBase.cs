using JiangXinService.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC.DAL.BLL;
using ZC.DBUtils;

namespace JiangXinService.Utils
{
    public class CommonBase
    {
        #region 执行结果代码
        ///// <summary>
        ///// 执行失败
        ///// </summary>
        //protected static readonly string _FAIL_STATUS = 0.ToString();

        ///// <summary>
        ///// 执行成功
        ///// </summary>
        //protected static readonly string _SUCCESS_STATUS = 1.ToString();

        ///// <summary>
        ///// 成功提示
        ///// </summary>
        //protected static readonly string _SUCCESS_MESSAGE = "执行成功";

        ///// <summary>
        ///// 系统异常
        ///// </summary>
        //protected static readonly string _ERROR_MESSAGE = "系统异常：";

        ///// <summary>
        ///// 执行失败
        ///// </summary>
        //protected static readonly string _FAIL_MESSAGE = "执行失败";

        ///// <summary>
        ///// 参数异常
        ///// </summary>
        //protected static readonly string _PARAMETER_ERROR_MESSAGE = "参数异常";
        #endregion

        /// <summary>
        /// DEBUG模式下的链接字符串
        /// </summary>

        //protected readonly static string _SQLCONN = "Database='a01';Data Source='172.16.100.80';User Id='root';Password='123456';charset='utf8';pooling=true;port=3306;";

        //protected static string _SQLCONN = "Database='a019_sunrise';Data Source='218.93.219.194';User Id='root';Password='123456';charset='utf8';pooling=true;port=9988;";

        public readonly static string _SQLCONN1 = "Database='c01';Data Source='218.93.219.194';User Id='root';Password='123456';charset='utf8';pooling=true;port=9988;";
        public readonly static string _SQLCONN2 = "Database='v06';Data Source='218.93.219.194';User Id='root';Password='123456';charset='utf8';pooling=true;port=9988;";

        public CommonBase()
        {
            Fail();
        }
        /// <summary>
        /// 响应实体对象
        /// </summary>
        protected ReturnMsg resultData = new ReturnMsg();

        protected static TOut TransReflection<TOut>(JObject tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();

            foreach (var itemOut in tOut.GetType().GetProperties().OrderBy(t => t.Name))
            {
                var outName = itemOut.Name;

                if (!outName.StartsWith("_"))
                {
                    if (tIn.GetValue(outName) != null)
                    {
                        itemOut.SetValue(tOut, tIn.GetValue(outName).ToString());
                    }
                    else
                    {
                        itemOut.SetValue(tOut, null);
                    }
                }
            }
            return tOut;
        }

        protected static List<TOut> TransListReflection<TOut>(JArray tInArray)
        {
            List<TOut> tOutList = Activator.CreateInstance<List<TOut>>();

            if (tInArray.Count > 0)
            {
                foreach (var tInItem in tInArray)
                {
                    TOut tOut = Activator.CreateInstance<TOut>();
                    foreach (var itemOut in tOut.GetType().GetProperties().OrderBy(t => t.Name))
                    {
                        var outName = itemOut.Name;

                        if (!outName.StartsWith("_"))
                        {
                            if (((JObject)tInItem).GetValue(outName) != null)
                            {
                                itemOut.SetValue(tOut, ((JObject)tInItem).GetValue(outName).ToString());
                            }
                            else
                            {
                                itemOut.SetValue(tOut, null);
                            }
                        }
                    }
                    tOutList.Add(tOut);
                }
            }
            return tOutList;
        }


        protected static void WriteErrorLog(string sqlConn, object content, string creator)
        {
            try
            {
                var strSql = @"INSERT INTO logdaterror (Content,Creator) VALUES (@Content,@Creator);";
                CmdParameter[] parameters = new CmdParameter[3];
                parameters[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Content", Value = JsonConvert.SerializeObject(content) };
                parameters[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Creator", Value = creator };

                new BaseSQL(sqlConn).ExeSqlTrans(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 记录TXT日志文件
        /// </summary>
        /// <param name="Content">内容</param>
        /// <param name="Type">日志类型</param>
        public static void WriteLogs(string Content, string Type)
        {
            try
            {
                var path = $@"D:\HistoryLogs\{DateTime.Now.ToString("yyyy-MM")}\{DateTime.Now.ToString("yyyy-MM-dd")}\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.AppendAllText(path + Type + ".txt", "\r\n" + Content);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 写入记录到日志表中
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="requestBody"></param>
        /// <param name="response"></param>
        /// <param name="isSuccess"></param>
        /// <param name="sqlConn"></param>
        /// <param name="tableName"></param>
        public static void WriteLogToTable(string requestUrl, string requestBody, string response, string isSuccess, string sqlConn, string tableName)
        {
            try
            {
                CmdParameter[] parameters = new CmdParameter[4];
                parameters[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@requestUrl", Value = requestUrl };
                parameters[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@requestBody", Value = requestBody };
                parameters[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@response", Value = response };
                parameters[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@isSuccess", Value = isSuccess };

                var strSql = $@"INSERT INTO {tableName}(RequestUrl,RequestBody,Response,IsSuccess) VALUES (@requestUrl,@requestBody,@response,@isSuccess);";
                new BaseSQL(sqlConn).ExeSql(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {

            }
        }
        void Fail()
        {
            resultData.Status = ErrorModel._FAIL_STATUS;
            resultData._ReturnMsg = ErrorModel._FAIL_MESSAGE;
            resultData.MsgCode = "-1";
        }
        public void Fail(string message)
        {
            resultData._ReturnMsg = ErrorModel._FAIL_MESSAGE + "-" + message;
        }
        public void Success()
        {
            resultData.Status = ErrorModel._SUCCESS_STATUS;
            resultData._ReturnMsg = ErrorModel._SUCCESS_MESSAGE;
            resultData.MsgCode = "0";
        }
    }
}
