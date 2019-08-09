namespace Boc.Assets.Web.Auth.Authorization
{
    public class Permissions
    {
        public static class Controllers
        {
            public const string Asset = "资产";
            public const string AssetApply = "资产申请";
            public const string AssetCategory = "资产类别";
            public const string AssetExchange = "资产机构间调配申请";
            public const string AssetReturn = "资产交回";
            public const string AssetStockTaking = "资产盘点";
            public const string Dashboard = "仪表板";
            public const string Employe = "员工";
            public const string Maintainer = "资产维修供应商";
            public const string ManagementLine = "机构条线";
            public const string NonAuditEvent = "用户事件";
            public const string OrganizationSpace = "机构空间";
            public const string Permission = "权限";
        }

        public static class Actions
        {
            /*资产的操作权限*/
            public const string Asset_Read_Secondary = "二级行读取";
            public const string Asset_Read_Current = "本机构读取";
            public const string Asset_Create_Current = "创建";
            public const string Asset_Modify_Current = "本机构更新";
            public const string Asset_Update_Secondary = "二级行更新";
            public const string Asset_Delete = "删除";


            /*资产分类的操作权限*/
            public const string AssetCategory_Read_Current = "本机构读取";
            public const string AssetCategory_Read_Secondary = "二级行读取";
            public const string AssetCategory_Modify_Secondary = "二级行更新";

            /*资产申请的操作权限*/
            public const string AssetApply_Read_Current = "本机构读取";
            public const string AssetApply_Create_Current = "发起申请";
            public const string AssetApply_Delete_Current = "本机构删除";
            public const string AssetApply_Modify_Secondary = "二级行更新";
            public const string AssetApply_Read_Secondary = "二级行读取";

            /*资产的机构间调配申请的操作权限*/
            public const string AssetExchange_Read_Current = "本机构读取";
            public const string AssetExchange_Create_Current = "发起申请";
            public const string AssetExchange_Delete_Current = "本机构删除";
            public const string AssetExchange_Read_Secondary = "二级行读取";
            public const string AssetExchange_Modify_Secondary = "二级行更新";

            /*资产交回申请的操作权限*/
            public const string AssetReturn_Read_Current = "本机构读取";
            public const string AssetReturn_Create_Current = "发起申请";
            public const string AssetReturn_Delete_Current = "本机构删除";
            public const string AssetReturn_Read_Secondary = "二级行读取";
            public const string AssetReturn_Modify_Secondary = "二级行更新";

            /*资产盘点的操作权限*/
            public const string AssetStockTaking_Read_Current = "本机构读取";
            public const string AssetStockTaking_Create_Currrent = "本机构盘点资产";
            public const string AssetStockTaking_Create_Secondary = "二级行创建盘点任务";
            public const string AssetStockTaking_Delete_Current = "本机构删除";
            public const string AssetStockTaking_Read_Secondary = "二级行读取";
            public const string AssetStockTaking_Modify_Secondary = "二级行更新";

            /*仪表板的操作权限*/
            public const string Dashboard_Read_Current = "本机构读取";
            public const string Dashboard_Read_Secondary = "二级行读取";
            public const string Dashboard_Modify_Secondary = "二级行更新";
            public const string Dashboard_Download_Secondary = "二级行报表下载";

            /*员工实体的操作权限*/
            public const string Employe_Read = "读取";
            public const string Employe_Create = "创建";
            public const string Employe_Delete = "删除";
            public const string Employe_Modify = "更新";

            /*资产维修供应商操作权限*/
            public const string Maintainer_Read_Current = "本机构读取";
            public const string Maintainer_Delete_Secondary = "二级行删除";
            public const string Maintainer_Create_Secondary = "二级行创建";
            public const string Maintainer_Read_Secondary = "二级行读取";
            public const string Maintainer_Modify_Secondary = "二级行更新";

            /*机构条线的操作权限*/
            public const string ManagementLine_Read = "读取";

            /*用户事件的操作权限*/
            public const string NonAuditEvent_Read_Current = "本机构读取";
            public const string NonAuditEvent_Delete_Current = "本机构删除";
            public const string NonAuditEvent_Delete_Secondary = "二级行删除";
            public const string NonAuditEvent_Read_Secondary = "二级行读取";

            /*机构空间的操作权限*/
            public const string OrgSpace_Read_Current = "本机构读取";
            public const string OrgSpace_Create_Current = "本机构创建";
            public const string OrgSpace_Delete_Current = "本机构删除";
            public const string OrgSpace_Modify_Current = "本机构更新";
            public const string OrgSpace_Read_Secondary = "二级行读取";

            /*权限实体的操作权限*/
            public const string Permission_Read_Secondary = "二级行读取";
            public const string Permission_Create_Secondary = "二级行创建";
            public const string Permission_Read_Current = "本机构读取";
            public const string Permission_Modify_Secondary = "二级行更新";
        }

    }
}