
using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Organizations;
using Sieve.Services;
using System;
using System.Linq;

namespace Boc.Assets.Application.Pagination
{
    public class CustomSieveFilterMethods : ISieveCustomFilterMethods
    {
        public IQueryable<Organization> OrgFilter(IQueryable<Organization> source, string op, string[] values)
        {
            return source.Where(it =>
                it.OrgIdentifier.Equals(values[0], StringComparison.OrdinalIgnoreCase) ||
                it.OrgNam.Contains(values[0], StringComparison.OrdinalIgnoreCase));
        }

        public IQueryable<Asset> AssetsFilter(IQueryable<Asset> source, string op, string[] values)
        {
            return source.Where(it =>
                 it.AssetName.Contains(values[0]) || it.Brand.Contains(values[0]));
        }

        public IQueryable<AssetCategory> AssetCategoryFilter(IQueryable<AssetCategory> source, string op,
            string[] values)
        {
            return source.Where(it => it.AssetThirdLevelCategory.Contains(values[0])
                                      || it.AssetFirstLevelCategory.Contains(values[0])
                                      || it.AssetSecondLevelCategory.Contains(values[0]));
        }

        public IQueryable<OrganizationSpace> OrgSpaceFilter(
            IQueryable<OrganizationSpace> source,
            string op,
            string[] values)
        {
            return source.Where(it => it.SpaceName.Contains(values[0])
                                      || it.SpaceDescription.Contains(values[0]));
        }
        public IQueryable<AssetApply> AssetApplyingEventsFilter(IQueryable<AssetApply> source,
            string op, string[] values)
        {
            return source.Where(it => it.RequestOrgIdentifier == values[0] ||
                                      it.TargetOrgIdentifier == values[0] ||
                                      it.TargetOrgNam.Contains(values[0]));
        }

        public IQueryable<AssetReturn> AssetReturningEventsFilter(IQueryable<AssetReturn> source,
            string op, string[] values)
        {
            return source.Where(it => it.AssetName.Contains(values[0]) ||
                                      it.TargetOrgNam.Contains(values[0]) ||
                                      it.TargetOrgIdentifier == values[0]);
        }

        public IQueryable<AssetExchange> AssetExchangingFilter(IQueryable<AssetExchange> source,
            string op, string[] values)
        {
            return source.Where(it => it.AssetName.Contains(values[0]) ||
                                      it.RequestOrgNam.Contains(values[0]) ||
                                      it.RequestOrgIdentifier == values[0]);
        }

        public IQueryable<AssetDeploy> AssetDeployFilter(IQueryable<AssetDeploy> source,
            string op, string[] values)
        {
            return source.Where(it => it.AssetName.Contains(values[0]) ||
                                      it.ExportOrgInfo.OrgNam.Contains(values[0]) ||
                                      it.ImportOrgInfo.OrgNam.Contains(values[0]));
        }

        public IQueryable<Employee> EmployeeFilter(IQueryable<Employee> source, string op, string[] values)
        {
            return source.Where(it => it.Name.Contains(values[0]) || it.Identifier == values[0]);
        }
        public IQueryable<AssetInventoryRegister> InventoryRegistersFilter(
            IQueryable<AssetInventoryRegister> source,
            string op, string[] values)
        {
            return source.Where(it => it.Participation.OrgNam.Contains(values[0]));
        }
    }
}