using JiangXinModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinModel
{
    /// <summary>
    /// 设备台账
    /// </summary>
    public class EquipmentParameterModule
    {
        /// <summary>
        /// 设备信息
        /// </summary>
        public EquipmentInfo EquipmentInfo { get; set; }
        /// <summary>
        /// 部门信息
        /// </summary>
        public Sysdatdepartment Sysdatdepartment { get; set; }
        /// <summary>
        /// 供应商信息
        /// </summary>
        public Sysdatsupplier Sysdatsupplier { get; set; }
        /// <summary>
        /// 检修信息
        /// </summary>
        public RepairInfo RepairInfo { get; set; }
    }
}
