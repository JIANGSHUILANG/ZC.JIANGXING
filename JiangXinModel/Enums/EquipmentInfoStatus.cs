using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinModel.Enums
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum EquipmentInfoStatus
    {
        /// <summary>
        /// 在用
        /// </summary>
        use = 0,
        /// <summary>
        /// 报废
        /// </summary>
        scrap = 1,
        /// <summary>
        /// 闲置
        /// </summary>
        idle = 2,
        /// <summary>
        /// 借出
        /// </summary>
        lend = 3
    }
}
