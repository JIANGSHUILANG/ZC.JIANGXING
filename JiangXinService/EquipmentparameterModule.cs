using JiangXinCommand;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC.DAL.BLL;
using ZC.DBUtils;
using Newtonsoft.Json;

namespace JiangXinService
{
    public class EquipmentparameterModule : CommonBase
    {
        LoginUserInfo userInfo;
        ParameterModel model;
        string sqlConn;
        public ReturnMsg GetEquipmentWarning(string sqlConn, object Parameter)
        {
            this.sqlConn = sqlConn;
            model = Parameter as ParameterModel;
            return GetWarning();
        }
        /// <summary>
        /// 获取设备预警
        /// </summary>
        /// <param name="Parameter"></param>
        public ReturnMsg GetEquipmentWarning(InvokeEntity Parameter)
        {
            userInfo = BasePubCommon.FindLoginUserInfo(Parameter.token);
            model = JsonConvert.DeserializeObject<ParameterModel>(Parameter.obj.ToString());
            return GetWarning();
        }
        ReturnMsg GetWarning()
        {
            string cmdSql = string.Empty;
            CmdParameter[] paras = new CmdParameter[3];
            paras[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@pageindex", Value = (model.PageIndex - 1) * model.PageSize };
            paras[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@pagesize", Value = model.PageSize };
            paras[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@advancedays", Value = model.AdvanceDays };

            switch (model.FuncName)
            {
                case "GetEquipmentWarrantyWarning":
                    cmdSql = GetEquipmentWarrantyWarningCmdSql(paras);
                    break;
                case "GetEquipmentScrapWarning":
                    cmdSql = GetEquipmentScrapWarningCmdSql(paras);
                    break;
                case "GetEquipmentMaintenanceWarning":
                    cmdSql = GetEquipmentMaintenanceWarningCmdSql(paras);
                    break;
                default:
                    break;
            }
            try
            {
                DataSet ds = new BaseSQL(sqlConn).QuserDs(cmdSql, paras);
                resultData.item = new
                {
                    data = ds.Tables[0],
                    dataCount = ds.Tables[0].Rows.Count
                };
                Success();
            }
            catch (Exception ex)
            {
                Fail(ex.ToString());
            }
            return resultData;
        }
        /// <summary>
        /// 设备保修预警
        /// </summary>
        string GetEquipmentWarrantyWarningCmdSql(CmdParameter[] paras)
        {
            string cmdSql = string.Empty;
            #region cmdSql
            cmdSql = @"select
	datediff(
		emi.repairservicetotime,
		sysdate()
	) days,
	emi.repairservicetotime,
	#保修期至
	emi. code emicode,
	#设备编码
	emi. name emsname,
	#设备名称
	purchasedata,
	#采购日期
	repaircycle,
	#检修周期
	repairunit,
	#检修周期单位
	nextrepairtime,
	#下次检修日期
	emi.price,
	#原值
	sbr.id brandid,
	#品牌id
	sbr. name brandname,
	#品牌名称
	scs.id specificationid,
	#规格型号id
	scs. name specificationname,
	#规格型号名称
	pd.id productionid,
	#生产商id
	pd. name productionname,
	#生产商名称
	spp.suppliername,
	#供应商名称
	spp.supplierid,
	#供应商编号
	spp.id sid,
	#供应商id
	dep.departid,
	#部门编号
	dep.departname,
	#部门名称
	dep.id did #部门id
from
	sfcdatequipmentinfo emi
left join sfcdatbrand sbr on sbr.id = emi.brandid
left join sfcdatspecifications scs on scs.id = emi.specificationid
left join sfcdatproduction pd on pd.id = emi.producerid
left join sysdatsupplier spp on spp.id = emi.sysdatsupplierid
left join sysdatdepartment dep on dep.departid = emi.departid
where
	datediff(
		emi.repairservicetotime,
		sysdate()
	) >= 0
and datediff(
	emi.repairservicetotime,
	sysdate()
) <= @advancedays
or emi.repairservicetotime <= sysdate()
order by
	datediff(
		emi.repairservicetotime,
		sysdate()
	) desc ";
            cmdSql += ((model.IsPage == null || model.IsPage == 0) ? "" : "limit @pageindex,@pagesize");
            #endregion
            return cmdSql;
        }
        /// <summary>
        /// 设备报废预警
        /// </summary>
        /// <param name="Parameter"></param>
        string GetEquipmentScrapWarningCmdSql(CmdParameter[] paras)
        {
            string cmdSql = string.Empty;
            #region cmdSql
            cmdSql = @"select
	datediff(
		emi.scraptime,
		sysdate()
	)  days,
	emi.code emicode,#设备编码
	emi.name emsname,#设备名称
    emi.scraptime,#报废期至
	purchasedata,#采购日期
	repaircycle,#检修周期
	repairunit,#检修周期单位
	nextrepairtime,#下次检修日期
	emi.price,
	#原值
	sbr.id brandid,
	#品牌id
	sbr. name brandname,
	#品牌名称
	scs.id specificationid,
	#规格型号id
	scs. name specificationname,
	#规格型号名称
	pd.id productionid,
	#生产商id
	pd. name productionname,
	#生产商名称
	spp.suppliername,
	#供应商名称
	spp.supplierid,
	#供应商编号
	spp.id sid,
	#供应商id
	dep.departid,
	#部门编号
	dep.departname,
	#部门名称
	dep.id did #部门id
from
	sfcdatequipmentinfo emi
left join sfcdatbrand sbr on sbr.id = emi.brandid
left join sfcdatspecifications scs on scs.id = emi.specificationid
left join sfcdatproduction pd on pd.id = emi.producerid
left join sysdatsupplier spp on spp.id = emi.sysdatsupplierid
left join sysdatdepartment dep on dep.departid = emi.departid
where
	datediff(
		emi.scraptime,
		sysdate()
	) >= 0
AND datediff(
	emi.scraptime,
	sysdate()
) <=@advancedays
OR emi.scraptime <= SYSDATE() order by datediff(
	emi.scraptime,
	sysdate()
)  desc ;";
            cmdSql += ((model.IsPage == null || model.IsPage == 0) ? "" : "limit @pageindex,@pagesize");
            #endregion
            return cmdSql;
        }
        /// <summary>
        /// 设备检修预警
        /// </summary>
        /// <param name="Parameter"></param>
        string GetEquipmentMaintenanceWarningCmdSql(CmdParameter[] paras)
        {
            //根据表： sfcdatrepairinfo   nextrepairtime 下次检修时间来查询数据
            //datediff(c.nextrepairtime, SYSDATE())  两个时间相减后的天数
            string cmdSql = string.Empty;
            #region cmdSql
            cmdSql = @"select
	datediff(
		emi.nextrepairtime,
		sysdate()
	) days,
	emi. code emicode,
	#设备编码
	emi. name emsname,
	#设备名称
	purchasedata,
	#采购日期
	repaircycle,
	#检修周期
	repairunit,
	#检修周期单位
	nextrepairtime,
	#下次检修日期
	emi.price,
	#原值
	sbr.id brandid,
	#品牌id
	sbr. name brandname,
	#品牌名称
	scs.id specificationid,
	#规格型号id
	scs. name specificationname,
	#规格型号名称
	pd.id productionid,
	#生产商id
	pd. name productionname,
	#生产商名称
	spp.suppliername,
	#供应商名称
	spp.supplierid,
	#供应商编号
	spp.id sid,
	#供应商id
	dep.departid,
	#部门编号
	dep.departname,
	#部门名称
	dep.id did #部门id
from
	sfcdatequipmentinfo emi
left join sfcdatbrand sbr on sbr.id = emi.brandid
left join sfcdatspecifications scs on scs.id = emi.specificationid
left join sfcdatproduction pd on pd.id = emi.producerid
left join sysdatsupplier spp on spp.id = emi.sysdatsupplierid
left join sysdatdepartment dep on dep.departid = emi.departid
where
	datediff(
		emi.nextrepairtime,
		sysdate()
	) >= 0
AND datediff(
	emi.nextrepairtime,
	sysdate()
) <=@advancedays
OR emi.nextrepairtime <= SYSDATE()
order by datediff(
	emi.nextrepairtime,
	sysdate()
) desc ;";
            cmdSql += ((model.IsPage == null || model.IsPage == 0) ? "" : "limit @pageindex,@pagesize");
            //string sql = @"select
            //emi.code  emicode,#设备编码
            //emi.name emsname,#设备名称
            //purchasedata,#采购日期
            //repaircycle,#检修周期
            //repairunit,#检修周期单位
            //nextrepairtime,#下次检修日期
            //emi.price,#原值
            //sbr.id brandid,#品牌id
            //sbr.name brandname, #品牌名称
            //scs.id specificationid,#规格型号id
            //scs.name specificationname,#规格型号名称
            //pd.id productionid,#生产商id
            //pd.name productionname,#生产商名称
            //spp.suppliername,#供应商名称
            //spp.supplierid,#供应商编号
            //spp.id sid,#供应商id
            //dep.departid,#部门编号
            //dep.departname,#部门名称
            //dep.id did#部门id
            //from
            //	sfcdatequipmentinfo emi
            //where
            //	exists (
            //		select
            //			c.id
            //		from
            //			sfcdatrepairinfo c
            //		where
            //			datediff(c.nextrepairtime, sysdate()) <= @advancedays
            //		and c.equipmentcode = emi.`code`
            //	)
            //limit @pageindex,@pagesize;";
            #endregion
            return cmdSql;
        }


        //select* from student limit(curPage-1)*pageSize,pageSize;
        object ReadEveryLine(DataRow row)
        {
            return default(object);
        }
    }
}
