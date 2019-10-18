using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Infrastructure.DbConfigurations.ApplicationDbContextConfig;
using Microsoft.EntityFrameworkCore;

namespace Boc.Assets.Infrastructure.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 资产分类表
        /// </summary>
        public DbSet<AssetCategory> AssetCategories { get; set; }
        /// <summary>
        /// 服务商表
        /// </summary>
        public DbSet<Maintainer> Maintainers { get; set; }
        /// <summary>
        /// 资产表
        /// </summary>
        public DbSet<Asset> Assets { get; set; }
        /// <summary>
        /// 资产申请
        /// </summary>
        public DbSet<AssetApply> AssetApplies { get; set; }
        /// <summary>
        /// 资产机构间调配
        /// </summary>
        public DbSet<AssetExchange> AssetExchanges { get; set; }
        /// <summary>
        /// 资产交回
        /// </summary>
        public DbSet<AssetReturn> AssetReturns { get; set; }
        /// <summary>
        /// 资产调配表
        /// </summary>
        public DbSet<AssetDeploy> AssetDeploys { get; set; }
        /// <summary>
        /// 机构表
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }
        /// <summary>
        /// 员工表
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
        /// <summary>
        /// 机构角色表
        /// </summary>
        public DbSet<OrganizationRole> OrganizationRoles { get; set; }
        /// <summary>
        /// 机构空间表
        /// </summary>
        public DbSet<OrganizationSpace> OrganizationSpaces { get; set; }
        /// <summary>
        /// 资产盘点任务表
        /// </summary>
        public DbSet<AssetInventory> AssetInventories { get; set; }
        /// <summary>
        /// 资产盘点明细表
        /// </summary>
        public DbSet<AssetInventoryDetail> AssetInventoryDetails { get; set; }
        /// <summary>
        /// 资产盘点任务机构登记表
        /// </summary>
        public DbSet<AssetInventoryRegister> AssetInventoryRegisters { get; set; }
        /// <summary>
        /// 机构管理资产分类的注册表
        /// </summary>
        public DbSet<CategoryManageRegister> CategoryManageRegisters { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AssetApplyDbConfig());
            builder.ApplyConfiguration(new AssetExchangeDbConfig());
            builder.ApplyConfiguration(new AssetReturnDbConfig());
            builder.ApplyConfiguration(new AssetInventoryDbConfig());
            builder.ApplyConfiguration(new AssetInventoryDetailDbConfig());
            builder.ApplyConfiguration(new AssetInventoryRegisterDbConfig());
            builder.ApplyConfiguration(new AssetDbConfig());
            builder.ApplyConfiguration(new AssetCategoryDbConfig());
            builder.ApplyConfiguration(new AssetDeployDbConfig());
            builder.ApplyConfiguration(new CategoryManageRegisterDbConfig());
            builder.ApplyConfiguration(new MaintainerDbConfig());
            builder.ApplyConfiguration(new EmployeeDbConfig());
            builder.ApplyConfiguration(new OrganizationDbConfig());
            builder.ApplyConfiguration(new OrganizationRoleDbConfig());
            builder.ApplyConfiguration(new OrganizationSpaceDbConfig());

        }
    }
}