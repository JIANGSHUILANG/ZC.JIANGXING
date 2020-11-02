
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZC.DAL.BLL;
using ZC.DBUtils;
using Newtonsoft.Json;
using JiangXinService.Utils;

namespace JiangXinService
{
    public class EquipmentparameterModule : CommonBase
    {
        LoginUserInfo userInfo;
        public EquipmentModel model;
        Sfcdatequipmentspecies equipmentspecieModel;
        string sqlConn;
        public ReturnMsg GetEquipmentWarning(string sqlConn, object Parameter)
        {
            this.sqlConn = sqlConn;
            model = Parameter as EquipmentModel;
            return GetWarning();
        }
        /// <summary>
        /// 获取设备预警
        /// </summary>
        /// <param name="Parameter"></param>
        public ReturnMsg GetEquipmentWarning(InvokeEntity Parameter)
        {
            userInfo = BasePubCommon.FindLoginUserInfo(Parameter.token);
            model = JsonConvert.DeserializeObject<EquipmentModel>(Parameter.obj.ToString());
            this.sqlConn = Parameter.SqlConnection;
            return GetWarning();
        }
        public ReturnMsg Test(InvokeEntity Parameter)
        {
            //Success();
            return new ReturnMsg();
            //return resultData;
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
            var msg = JsonConvert.SerializeObject(resultData);
            //return msg;
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
	emi.repairservicetotime,#保修期至
	emi. code emicode, #设备编码
	emi. name emsname, #设备名称
	purchasedata, #采购日期
	nextrepairtime, #下次检修日期
	emi.price, #原值
	spp.suppliername, #供应商名称
	spp.supplierid, #供应商编号
	spp.id sid, #供应商id
	dep.departid, #部门编号
	dep.departname, #部门名称
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

        public ReturnMsg AutoSetEquipmentSpecieCodeTest(string sqlConn, Sfcdatequipmentspecies equipmentspecieModel)
        {
            this.sqlConn = sqlConn;
            //equipmentspecieModel = JsonConvert.DeserializeObject<Sfcdatequipmentspecies>(Parameter.ToString());
            return SetEquipmentSpecieCode(equipmentspecieModel);
        }
        /// <summary>
        /// 动态创建设备种类子节点的Code值
        /// </summary>
        /// <returns></returns>
        public ReturnMsg AutoSetEquipmentSpecieCode(InvokeEntity Parameter)
        {
            userInfo = BasePubCommon.FindLoginUserInfo(Parameter.token);
            equipmentspecieModel = JsonConvert.DeserializeObject<Sfcdatequipmentspecies>(Parameter.obj.ToString());
            equipmentspecieModel.Creator = userInfo.UserName;
            this.sqlConn = userInfo.SQLCONN;
            return SetEquipmentSpecieCode(equipmentspecieModel);
        }

        ReturnMsg SetEquipmentSpecieCode(Sfcdatequipmentspecies model)
        {
            if (string.IsNullOrWhiteSpace(model.parentcode))
            {
                model.parentcode = "0";
            }
            if (model != null)
            {
                string maxCode = string.Empty;
                string maxCodeSql = "select CONCAT('00',max(c.code)+1) maxcode from Sfcdatequipmentspecies c where c.parentcode=@parentcode;";
                CmdParameter[] maxCodeparas = new CmdParameter[1];
                maxCodeparas[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@parentcode", Value = model.parentcode };
                DataSet ds = new BaseSQL(sqlConn).QuserDs(maxCodeSql, maxCodeparas);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    maxCode = row["maxcode"] != DBNull.Value ? row["maxcode"].ToString() : (model.parentcode + "001");
                }
                else
                {
                    Fail("未查询到数据parentCode对应的数据");
                }

                #region Insert Sql

                string cmdSql = @"insert into Sfcdatequipmentspecies(
code,
name,
parentcode,
parentname,
internalcode,
warrantymonth,
scrapmonth,
depreciationwayname,
depreciationwayid,
salvage,
depreciationmonth,
Creator,
Attributionname,
Attributionid
)values(
@code,
@name,
@parentcode,
@parentname,
@internalcode,
@warrantymonth,
@scrapmonth,
@depreciationwayname,
@depreciationwayid,
@salvage,
@depreciationmonth,
@Creator,
@Attributionname,
@Attributionid
);
";
                #endregion
                CmdParameter[] paras = new CmdParameter[14];
                paras[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@code", Value = maxCode };
                paras[1] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@name", Value = model.name };
                paras[2] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@parentcode", Value = model.parentcode };
                paras[3] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@parentname", Value = model.parentname };
                paras[4] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@internalcode", Value = model.internalcode };
                paras[5] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@warrantymonth", Value = model.warrantymonth };
                paras[6] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@scrapmonth", Value = model.scrapmonth };
                paras[7] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@depreciationwayname", Value = model.depreciationwayname };
                paras[8] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@depreciationwayid", Value = model.depreciationwayid };
                paras[9] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@salvage", Value = model.salvage };
                paras[10] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@depreciationmonth", Value = model.depreciationmonth };
                paras[11] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Creator", Value = model.Creator };
                paras[12] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Attributionname", Value = model.Attributionname };
                paras[13] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@Attributionid", Value = model.Attributionid };
                bool flag = new BaseSQL(sqlConn).ExeSql(cmdSql, paras);
                if (!flag)
                    Fail("插入数据库Sfcdatequipmentspecies失败！");
            }
            Success();
            return resultData;
        }

        public ReturnMsg Equiparameter(InvokeEntity Parameter)
        {
            model = JsonConvert.DeserializeObject<EquipmentModel>(Parameter.obj.ToString());
            this.sqlConn = Parameter.SqlConnection;
            return GetEquiparameter();
        }

        ReturnMsg GetEquiparameter()
        {
            CmdParameter[] paras = new CmdParameter[1];
            paras[0] = new CmdParameter() { DBtype = DBType.String, ParameterName = "@usetime", Value = model.Usetime };

            StringBuilder cmdSql = new StringBuilder();
            string where = (string.IsNullOrWhiteSpace(model.Usetime) ? null : $" where v.`启用日期` is not null and v.`启用日期`= @usetime ");
            switch (model.FuncName)
            {
                case "SummaryByDepartment":
                    cmdSql.AppendFormat(@"SELECT

    v.`部门编码`,
    v.`部门名称`,
    COUNT(1) 设备数量,
    SUM(v.`原值`) 设备原值,
    SUM(v.`累计折旧`) 累计折旧,
    SUM(v.`原值`) - SUM(v.`累计折旧`) 设备净值
FROM
    v_sfcdatcurrentequiparameter v {0}
GROUP BY

    v.`部门名称`,
    v.`部门编码`", where);

                    var ds = new BaseSQL(sqlConn).QuserDs(cmdSql.ToString(), paras);
                    var json = JsonConvert.SerializeObject(ds.Tables[0]);
                    resultData.item = ds.Tables[0];
                    break;
                case "SummaryBySpecies":
                    cmdSql.AppendFormat(@"SELECT

    v.`设备种类`,
    v.`设备种类编码`,
    COUNT(1) 设备数量,
    SUM(v.`原值`) 设备原值,
    SUM(v.`累计折旧`) 累计折旧,
    SUM(v.`原值`) - SUM(v.`累计折旧`) 设备净值
FROM
    v_sfcdatcurrentequiparameter v {0}
GROUP BY

    v.`设备种类`,
    v.`设备种类编码`", where);
                    var ds1 = new BaseSQL(sqlConn).QuserDs(cmdSql.ToString(), paras);
                    var json1 = JsonConvert.SerializeObject(ds1.Tables[0]);
                    resultData.item = ds1.Tables[0];
                    break;
                default:
                    break;
            }
            Success();
            return resultData;
        }

        /// <summary>
        /// 获取合同结算
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        //ReturnMsg GetContractSettlement(InvokeEntity Parameter) { }
        //select* from student limit(curPage-1)*pageSize,pageSize;
        object ReadEveryLine(DataRow row)
        {
            return default(object);
        }
    }

    public class Sfcdatequipmentspecies
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string parentcode { get; set; }
        public string parentname { get; set; }
        public string internalcode { get; set; }
        /// <summary>
        /// 保修年限
        /// </summary>
        public int warrantymonth { get; set; }
        /// <summary>
        /// 报废年限
        /// </summary>
        public string scrapmonth { get; set; }
        /// <summary>
        /// 折旧方式名称
        /// </summary>
        public string depreciationwayname { get; set; }
        /// <summary>
        /// 折旧方式
        /// </summary>
        public string depreciationwayid { get; set; }
        /// <summary>
        /// 净残值率
        /// </summary>
        public string salvage { get; set; }
        /// <summary>
        /// 预计使用
        /// </summary>
        public string depreciationmonth { get; set; }
        public string Creator { get; set; }
        public string Attributionname { get; set; }
        public string Attributionid { get; set; }
        public string Createtime { get; set; }
        public string Updatetime { get; set; }
    }
}
