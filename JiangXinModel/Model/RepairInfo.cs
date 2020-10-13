using JiangXinModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinModel.Model
{
    /// <summary>
    /// 报修信息
    /// </summary>
    public class RepairInfo : BaseModel
    {
        /// <summary>
        /// 单据编号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 检修时间
        /// </summary>
        public Nullable<DateTime> repairtime { get; set; }
       
        /// <summary>
        /// 操作人
        /// </summary>
        public string operation { get; set; }
        /// <summary>
        /// 原始状态
        /// </summary>
        public Nullable<EquipmentInfoStatus> originalstatus { get; set; }
        /// <summary>
        /// 检修后状态
        /// </summary>
        public Nullable<EquipmentInfoStatus> afterstatus { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        public Nullable<DateTime> registrationtime { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public Nullable<DateTime> registrationupdatetime { get; set; }
        ///// <summary>
        ///// 登记操作员
        ///// </summary>
        //public string registrationoperation { get; set; }
        ///// <summary>
        ///// 登记修改人
        ///// </summary>
        //public string registrationoupdateperation { get; set; }
        ///// <summary>
        ///// 检修周期
        ///// </summary>
        //public string repaircycle { get; set; }
        ///// <summary>
        ///// 检修单位
        ///// </summary>
        //public string repairunit { get; set; }

    }
}
