using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Domain.Events;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Models.Organizations;
using Microsoft.Extensions.Options;

namespace Boc.Assets.Application.Pagination
{

    public class PangSieveProcessor : SieveProcessor

    {
        private readonly IOptions<SieveOptions> _options;
        private readonly ISieveCustomSortMethods _customSortMethods;
        private readonly ISieveCustomFilterMethods _customFilterMethods;
        private readonly SievePropertyMapper mapper = new SievePropertyMapper();

        public PangSieveProcessor(IOptions<SieveOptions> options,
            ISieveCustomSortMethods customSortMethods,
            ISieveCustomFilterMethods customFilterMethods) : base(options, customSortMethods, customFilterMethods)
        {
            mapper = MapProperties(mapper);
            _options = options;
            _customSortMethods = customSortMethods;
            _customFilterMethods = customFilterMethods;
        }
        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            //机构
            mapper.Property<Organization>(it => it.OrgIdentifier).CanSort();
            mapper.Property<Organization>(it => it.OrgLvl).CanSort();
            mapper.Property<Organization>(it => it.OrgShortNam).CanSort();
            mapper.Property<Organization>(it => it.OrgNam).CanFilter();
            //资产
            mapper.Property<Asset>(it => it.AssetName).CanSort();
            mapper.Property<Asset>(it => it.AssetTagNumber).CanSort();
            //资产分类
            mapper.Property<AssetCategory>(it => it.AssetThirdLevelCategory).CanSort();
            //机构空间
            mapper.Property<OrganizationSpace>(it => it.Id).HasName("spaceId").CanSort();
            mapper.Property<OrganizationSpace>(it => it.SpaceName).CanSort();
            //非审计类事件
            mapper.Property<NonAuditEvent>(it => it.OrgIdentifier).CanSort();
            mapper.Property<NonAuditEvent>(it => it.OrgNam).CanSort();
            mapper.Property<NonAuditEvent>(it => it.Type).CanSort();
            //资产申请事件
            mapper.Property<AssetApply>(it => it.RequestOrgIdentifier).CanSort();
            mapper.Property<AssetApply>(it => it.TimeStamp).HasName("dateTimeFromNow").CanSort();
            mapper.Property<AssetApply>(it => it.RequestOrgNam).CanSort();
            mapper.Property<AssetApply>(it => it.Status).CanSort();
            //资产交回事件
            mapper.Property<AssetReturn>(it => it.AssetName).CanSort();
            mapper.Property<AssetReturn>(it => it.RequestOrgIdentifier).CanSort();
            mapper.Property<AssetReturn>(it => it.TimeStamp).HasName("dateTimeFromNow").CanSort();
            mapper.Property<AssetReturn>(it => it.Status).CanSort();
            //资产调配事件
            mapper.Property<AssetExchange>(it => it.AssetName).CanSort();
            mapper.Property<AssetExchange>(it => it.TimeStamp).HasName("dateTimeFromNow").CanSort();
            mapper.Property<AssetExchange>(it => it.Status).CanSort();
            mapper.Property<AssetExchange>(it => it.RequestOrgIdentifier).CanSort();
            //资产调转记录
            mapper.Property<AssetDeploy>(it => it.CreateDateTime).HasName("dateTimeFromNow").CanSort();
            mapper.Property<AssetDeploy>(it => it.AssetDeployCategory).CanSort();
            mapper.Property<AssetDeploy>(it => it.AssetName).CanSort();
            //资产盘点参与机构
            mapper.Property<AssetStockTakingOrganization>(it => it.Organization.OrgNam).HasName("orgNam").CanSort();
            //员工
            mapper.Property<Employee>(it => it.Identifier).CanSort();
            mapper.Property<Employee>(it => it.Name).CanSort();
            return mapper;
        }
    }
}