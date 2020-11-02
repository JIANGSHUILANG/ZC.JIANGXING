using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinService
{
    public class EquipmentModel
    {
        /// <summary>
        /// 调用方法名称
        /// </summary>
        public string FuncName { get; set; }
        /// <summary>
        /// 启用时间
        /// </summary>
       public string Usetime { get; set; }
        /// <summary>
        /// 提前预警天数
        /// </summary>
        public int AdvanceDays { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页尺寸
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 是否分页 0：不分页 1：分页
        /// </summary>
        public int? IsPage { get; set; }
    }
}
