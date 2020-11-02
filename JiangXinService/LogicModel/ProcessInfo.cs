using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinService
{
    public class ProcessWorkFlowDef
    {
        /// <summary>
        /// 合同审批
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int publicFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sortNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duplicateRemovelFlag { get; set; }
        /// <summary>
        /// 选择提示
        /// </summary>
        public string optionTip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int optionNotNull { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 条件
    /// </summary>
    public class ProcessConditionList
    {
        /// <summary>
        /// 
        /// </summary>
        public int columnId { get; set; }
        /// <summary>
        /// 1是人，3是部门
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string conditionEn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string conditionCn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string optType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdy1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdy2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string opt1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string opt2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string columnDbname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string columnType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fixedDownBoxValue { get; set; }
    }

    /// <summary>
    /// 发起人下用户的类型和用户信息
    /// </summary>
    public class ProcessNodeUser
    {
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public int NodeConfigId { get; set; }
        /// <summary>
        ///发起人的 loginid 
        /// </summary>
        public int targetId { get; set; }
        /// <summary>
        /// 1.个人 3.部门
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 天旭
        /// </summary>
        public string name { get; set; }
    }

    public class ProcessConditionNodes
    {
        /// <summary>
        /// 条件1
        /// </summary>
        public string nodeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int priorityLevel { get; set; }
        /// <summary>
        /// 审核人设置类型 1.指定成员 2.主管 3.发起人自选 4.发起人自己 5.连续多级主管
        /// </summary>
        public int settype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int selectMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int selectRange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineRoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int directorLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int replaceByUp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int noHanderAction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineEndType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineEndRoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineEndDirectorLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ccSelfSelectFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessConditionList> conditionList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessNodeUser> nodeUserList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ProcessChildNode childNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessConditionNodes> conditionNodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool error { get; set; }
    }

    public class ProcessChildNode
    {
        public int Id { get; set; }

        public ChildNodeStatus Status { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string nodeName { get; set; }

        public int NodeConfigId { get; set; }
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int priorityLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int settype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int selectMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int selectRange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineRoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int directorLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int replaceByUp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int noHanderAction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineEndType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineEndRoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int examineEndDirectorLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ccSelfSelectFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessConditionList> conditionList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessNodeUser> nodeUserList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ProcessChildNode childNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessConditionNodes> conditionNodes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool error { get; set; }
    }
    public class ProcessType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class ProcessNodeConfig
    {
        /// <summary>
        /// 当前创建审核对象人的id
        /// </summary>
        public string pkId { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string nodeName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public string priorityLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string settype { get; set; }
        /// <summary>
        /// 0:
        /// </summary>
        public string selectMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string selectRange { get; set; }
        /// <summary>
        /// 检查角色id
        /// </summary>
        public string examineRoleId { get; set; }
        /// <summary>
        /// 主管等级
        /// </summary>
        public string directorLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string replaceByUp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string examineMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string noHanderAction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string examineEndType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string examineEndRoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string examineEndDirectorLevel { get; set; }
        /// <summary>
        /// 是否抄送自己
        /// </summary>
        public string ccSelfSelectFlag { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public List<ProcessConditionList> conditionList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessNodeUser> nodeUserList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ProcessChildNode childNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProcessConditionNodes> conditionNodes { get; set; }
    }

    public class ProcessInfo
    {

        public int Id { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tableId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string workFlowVersionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ProcessWorkFlowDef workFlowDef { get; set; }
        /// <summary>
        /// 主管最高级别
        /// </summary>
        public int directorMaxLevel { get; set; }
        /// <summary>
        /// 许可
        /// </summary>
        public List<ProcessNodeUser> flowPermission { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ProcessNodeConfig nodeConfig { get; set; }
    }

    /// <summary>
    /// 流程配置
    /// </summary>
    public class ProcessSetting
    {
        /// <summary>
        /// 配置Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 配置Code
        /// </summary>
        public string Code { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 前端发送的配置json信息,供前端渲染页面
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 流程配置状态
        /// </summary>
        public ProcessSettingStatus Status { get; set; }
        /// <summary>
        /// 创建当前配置人的LoginId
        /// </summary>
        public string LoginId { get; set; }
    }

    public enum ChildNodeStatus
    {

    }

    public enum ProcessSettingStatus
    {
        /// <summary>
        /// 在用
        /// </summary>
        Use = 1,
        /// <summary>
        /// 废弃
        /// </summary>
        Abandoned = 2
    }

    public enum ProcessStatus
    {
        驳回 = -1, // 驳回
        未处理 = 0,// 未审批，默认状态
        己处理 = 1,// 审批通过
        审批通过 = 2,
        单据做废 = -2,
        创建单据 = 9
    }
    /// <summary>
    /// 流程详情
    /// </summary>
    public class ProcessDetail
    {
        public int Id { get; set; }
        public int SettingId { get; set; }
        public ProcessSetting ProcessSetting { get; set; }
        /// <summary>
        /// 发起人Id
        /// </summary>
        public string LoginId { get; set; }
        ///// <summary>
        ///// 当前流程
        ///// </summary>
        public int CurrentProcessInfoId { get; set; }
        /// <summary>
        /// 当前审批状态
        /// </summary>
        public int ProcessStatus { get; set; }
        /// <summary>
        /// 当前节点保存的流程信息
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 当前正在节点的位置
        /// </summary>
        public int CurrentNodePositionId { get; set; }

        /// <summary>
        /// 下一个节点位置
        /// </summary>
        public int NextNodePostionId { get; set; }



    }

}
