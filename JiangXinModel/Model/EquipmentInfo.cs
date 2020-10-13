using JiangXinModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangXinModel.Model
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class EquipmentInfo : BaseModel
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 设备种类
        /// </summary>
        public EquipmentSpecies equipmentspeciesid { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public EquipmentInfoStatus sfcequipmentinfostatusid { get; set; }
        /// <summary>
        /// 设备品牌
        /// </summary>
        public string brand { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string specifications { get; set; }
        /// <summary>
        /// 生产商
        /// </summary>
        public string producers { get; set; }
        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime purchaseData { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string use { get; set; }
        /// <summary>
        /// 出场编号
        /// </summary>
        public string factorynmber { get; set; }
        /// <summary>
        /// 责任人
        /// </summary>
        public string responsible { get; set; }
        /// <summary>
        /// 使用人
        /// </summary>
        public string useoper { get; set; }
        /// <summary>
        /// 启用日期
        /// </summary>
        public Nullable<DateTime> usetime { get; set; }
        /// <summary>
        /// 建档日期
        /// </summary>
        public Nullable<DateTime> inputtime { get; set; }
        /// <summary>
        /// 财务编号
        /// </summary>
        public string financialcode { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime productiontime { get; set; }
        /// <summary>
        /// 保修截至期
        /// </summary>
        public DateTime repairservicetotime { get; set; }
        /// <summary>
        /// 报废截至日期
        /// </summary>
        public DateTime scraptime { get; set; }
        /// <summary>
        /// 购买合同
        /// </summary>
        public string purchasecontract { get; set; }
        /// <summary>
        /// 购买人
        /// </summary>
        public string buyer { get; set; }
        /// <summary>
        /// 设备备注
        /// </summary>
        public string equipmentdescription { get; set; }
        /// <summary>
        /// 残值率
        /// </summary>
        public double salvage { get; set; }
        /// <summary>
        /// 折旧方式
        /// </summary>
        public string depreciationway { get; set; }
        /// <summary>
        /// 预计折旧月
        /// </summary>
        public int depreciationmonth { get; set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string assetscode { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        public Nullable<DateTime> registrationtime { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string registrationoperation { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string updateoperation { get; set; }

        /// <summary>
        /// 检修周期
        /// </summary>
        public string repaircycle { get; set; }
        /// <summary>
        /// 检修单位
        /// </summary>
        public string repairunit { get; set; }
        /// <summary>
        /// 下次检修时间
        /// </summary>
        public Nullable<DateTime> nextrepairtime { get; set; }
    }
}
