using JiangXinService.Model;
using JiangXinService.Utils;
using MesInvoke.JiangXin.Approval.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC.DAL.BLL;
using ZC.DBUtils;

namespace JiangXinService.Module
{
    public class ApprovalModule : CommonBase, IBaseModule<ProcessInfo>
    {
        public LoginUserInfo userInfo;
        public string sqlConn;
        public string Creator = "Test";
        public ApprovalModule()
        {
            sqlConn = _SQLCONN2;
        }
        // API Path
        string hostPath = string.Empty;
        // Token令牌
        string token = string.Empty;
        // 创建人
        string creator = string.Empty;
        // 标题
        string approvaltitle = string.Empty;
        // 这个审批单的创建人ID
        string createLoginID = string.Empty;

        public ReturnModel MainTest(string sqlConn, object Parameter)
        {
            var arrpoval = InitSettingData(sqlConn, Parameter, null);
            return default(ReturnModel);
        }
        public ReturnModel Main(InvokeEntity Parameter)
        {
            var arrpoval = InitSettingData(null, null, null, Parameter);
            return default(ReturnModel);
        }
        /// <summary>
        ///初始化配置数据
        /// </summary>
        public ProcessInfo InitSettingData(string sqlConn, object Parameter, string token, InvokeEntity InvokeParameter = null)
        {
            this.hostPath = GetHostPath();
            this.sqlConn = sqlConn;
            this.token = token;
            var result = JsonConvert.DeserializeObject<ProcessInfo>(Parameter.ToString());
            if (InvokeParameter != null)
            {
                this.sqlConn = InvokeParameter.SqlConnection;
                this.token = InvokeParameter.token;
                this.userInfo = BasePubCommon.FindLoginUserInfo(InvokeParameter.token);
                result = JsonConvert.DeserializeObject<ProcessInfo>(InvokeParameter.obj.ToString());
            }
            return result;
        }
        /// <summary>
        /// 发起
        /// </summary>
        public void InitiateProcess(int settingId, string loginId)
        {
            ProcessInfo process = new ProcessInfo();
            //process
            var json = JsonConvert.SerializeObject(process);
            //将配置中的配置模型将模型的发起步骤提炼出来放入当前发起的记录中

            ProcessDetail detail = new ProcessDetail()
            {
                SettingId = settingId,
                LoginId = loginId,
                ProcessStatus = Convert.ToInt32(ProcessStatus.未处理),
                CurrentNodePositionId = 0,
                NextNodePostionId = 0,
                Content = json
            };
            SaveProcessDetail(detail);
        }

        /// <summary>
        /// 保存流程配置
        /// </summary>
        public void SaveProcessSetting(ProcessSetting model)
        {
            string cmdSql = "insert into sfcdatProcessSetting(Code,Content,Status,LoginId,Creator) values(@Code,@Content,@Status,@LoginId,@Creator)";
            CmdParameter[] paras = new CmdParameter[5];
            paras[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Code", Value = model.Code };
            paras[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Content", Value = model.Content };
            paras[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Status", Value = model.Status };
            paras[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@LoginId", Value = model.LoginId };
            paras[4] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Creator", Value = Creator };

            bool flag = new BaseSQL(sqlConn).ExeSql(cmdSql, paras);

            if (!flag)
                Fail("插入数据库ApprovalProcessSetting失败！");
            else
                Success();
        }
        public void SaveProcessDetail(ProcessDetail model)
        {
            string cmdSql = @"insert into sfcdatProcessDetail(CurrentProcessInfoId,SettingId,LoginId,ProcessStatus,Content,CurrentNodePositionId,NextNodePostionId
, Creator) values(@CurrentProcessInfoId,@SettingId,@LoginId,@ProcessStatus,@Content,@CurrentNodePositionId,@NextNodePostionId
, @Creator)";
            CmdParameter[] paras = new CmdParameter[8];
            paras[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@SettingId", Value = model.SettingId };
            paras[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@LoginId", Value = model.LoginId };
            paras[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@ProcessStatus", Value = model.ProcessStatus };
            paras[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Content", Value = model.Content };
            paras[4] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@CurrentNodePositionId", Value = model.CurrentNodePositionId };
            paras[5] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@NextNodePostionId", Value = model.NextNodePostionId };
            paras[6] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Creator", Value = Creator };
            paras[7] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@CurrentProcessInfoId", Value = model.CurrentProcessInfoId };

            bool flag = new BaseSQL(sqlConn).ExeSql(cmdSql, paras);

            if (!flag)
                Fail("插入数据库sfcdatProcessDetail失败！");
            else
                Success();
        }

        public void SaveProcessInfo(ProcessInfo model)
        {
            SaveProcessNodeUsers(model.flowPermission);
            string cmdSql = "insert into sfcdatProcessInfo(Code,tableId,workFlowVersionId,workFlowDef,directorMaxLevel,nodeConfig)values(@Code,@tableId,@workFlowVersionId,@workFlowDef,@directorMaxLevel,@nodeConfig)";
            CmdParameter[] paras = new CmdParameter[5];
            paras[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Code", Value = model.Code };
            paras[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@tableId", Value = model.tableId };
            paras[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@workFlowVersionId", Value = model.workFlowVersionId };
            paras[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@workFlowDef", Value = model.workFlowDef };
            paras[4] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@directorMaxLevel", Value = model.directorMaxLevel };
            paras[5] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@nodeConfig", Value = model.nodeConfig };
            bool flag = new BaseSQL(sqlConn).ExeSql(cmdSql, paras);
            if (!flag)
                Fail("插入数据库ProcessInfo失败！");
        }

        void SaveProcessNodeUsers(List<ProcessNodeUser> flowPermission)
        {
            foreach (ProcessNodeUser nodeUser in flowPermission)
            {
                string cmdSql = "insert into sfcdatProcessNodeUser(ProcessId,NodeConfigId,targetId,type,name)values(@ProcessId,@NodeConfigId,@targetId,@type,@name)";
                CmdParameter[] paras = new CmdParameter[5];
                paras[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@ProcessId", Value = nodeUser.ProcessId };
                paras[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@NodeConfigId", Value = nodeUser.NodeConfigId };
                paras[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@targetId", Value = nodeUser.targetId };
                paras[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@type", Value = nodeUser.type };
                paras[4] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@name", Value = nodeUser.name };
                bool flag = new BaseSQL(sqlConn).ExeSql(cmdSql, paras);
            }
        }



        /// <summary>
        /// 获取申请人到了当前哪个流程
        /// </summary>
        public void GetCurrentApprovalPhase(string loginId)
        {

        }



        // 发送通知
        void SendNotif(string sendLoginID, string addTitle, string endTitle)
        {
            if ("".Equals(sendLoginID))
            {
                return;
            }
            string addGuid = "-" + Guid.NewGuid().ToString();
            // 获取邮件群组
            string mailGroupGuid = GetMailGroupGuid(sendLoginID);
            // 创建消息
            CreateNotifMsg(approvaltitle, mailGroupGuid, sendLoginID, addTitle, endTitle, addGuid);
            // 发送消息
            string url = $"http://{hostPath}/notify/createNotifyToUser/?token={token}&title={approvaltitle + addGuid}";
            string result = Utils.HttpHelper.HttpPost(url, "");
            resultData = JsonConvert.DeserializeObject<ReturnMsg>(result);
        }

        void CreateNotifMsg(string title, string mailGroupGuid, string sendLoginID, string addTitle, string endTitle, string addGuid)
        {
            CmdParameter[] parameters = new CmdParameter[0];
            string cmd = "insert into sysdatnotify(title,eventid,content,type,level,isenable,isgroup,groupid,groupname,starttime,endtime,creator)values";
            cmd += "(@title,1,@content,1,1,1,1,@groupid,@groupname,(select now() as 'starttime'),(select date_add(NOW(), interval 1 MONTH) as 'endtime'),@creator);";

            //cmd += "insert into sysdatusernotify(loginid,msgid,isread,isenable,creator)values";
            //cmd += "(@loginid,(select id from sysdatnotify where title=@title limit 1),0,1,@creator);";

            parameters = new CmdParameter[10];
            parameters[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@groupid", Value = mailGroupGuid };
            parameters[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@groupname", Value = mailGroupGuid };
            parameters[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@title", Value = title + addGuid };
            parameters[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@content", Value = addTitle + title + "    " + endTitle };
            parameters[4] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@loginid", Value = sendLoginID };
            parameters[5] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@creator", Value = creator };
            bool flag = new BaseSQL(sqlConn).ExeSql(cmd, parameters);
            if (!flag)
            {
                throw new Exception("创建通知消息失败");
            }
        }

        string GetMailGroupGuid(string sendLoginID)
        {
            string cmd = $"SELECT a.*,b.UserID FROM `sysdatmailgroup` a LEFT JOIN sysdatmailgroupdetail b on a.Guid=b.MailGroupID where MailGroupID='{sendLoginID}';";
            CmdParameter[] parameters = new CmdParameter[0];
            DataTable dt = new BaseSQL(sqlConn).QueryDt(cmd, parameters);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["guid"].ToString();
            }
            else
            {
                // 新增
                string guid = Guid.NewGuid().ToString();
                cmd = "insert into sysdatmailgroup(guid,mailgroupname,isenable,creator) values ";
                cmd += "(@guid,@mailgroupname,1,@creator);";

                cmd += "insert into sysdatmailgroupdetail(mailgroupid,userid,username,creator) values ";
                cmd += "(@guid,@userid,(select username from sysdatuser where userid=@userid),@creator);";

                parameters = new CmdParameter[4];
                parameters[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@guid", Value = guid };
                parameters[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@mailgroupname", Value = "system(" + sendLoginID + ")：" + guid };
                parameters[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@creator", Value = creator };
                parameters[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@userid", Value = sendLoginID.Remove(0, 3) };
                bool flag = new BaseSQL(sqlConn).ExeSql(cmd, parameters);
                if (flag)
                {
                    return guid;
                }
                else
                {
                    throw new Exception("自动设置群组失败");
                }
            }
        }
        /// <summary>
        /// 并获取hostPath，获取审批节点ID
        /// </summary>
        /// <returns></returns>
        string GetHostPath()
        {
            CmdParameter[] parameters = new CmdParameter[] { };
            string cmd = "SELECT hosturl from sysdatcompany limit 1;";
            DataSet dataSet = new BaseSQL(sqlConn).QuserDs(cmd, parameters);
            return dataSet.Tables[0].Rows[0]["hosturl"].ToString();
        }
    }

    public class Model
    {
        // 审批ID
        public string approvalid { get; set; }
        // 审批节点ID
        public string nodeid { get; set; }
        // 审批结果
        public string result { get; set; }
        // 表单内容
        public string content { get; set; }
        // 指定审批人
        public string selectpeople { get; set; }
    }
}
