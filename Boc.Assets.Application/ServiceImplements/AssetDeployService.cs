using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.AssetDeploy;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetDeployService : IAssetDeployService
    {
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IAssetDeployRepository _assetDeployRepository;
        private readonly IUser _user;
        private readonly SieveOptions _sieveOptions;

        public AssetDeployService(IMapper mapper,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> options,
            IAssetDeployRepository assetDeployRepository,
            IUser user)
        {
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _assetDeployRepository = assetDeployRepository;
            _user = user;
            _sieveOptions = options.Value;
        }

        public async Task<ExcelPackage> DownloadAssetDeploy(DownloadAssetDeploy model)
        {
            var deploys = _assetDeployRepository.GetAll(it => it.AuthorizeOrgInfo.OrgId == _user.OrgId
                                                              && it.CreateDateTime >= model.StartDate
                                                              && it.CreateDateTime <= model.EndDate);
            if (model.ImportOrgId != null)
            {
                deploys = deploys.Where(it => it.ImportOrgInfo.OrgId == model.ImportOrgId.Value);
            }

            if (model.ExportOrgId != null)
            {
                deploys = deploys.Where(it => it.ExportOrgInfo.OrgId == model.ExportOrgId.Value);
            }

            var dtos = await _mapper.ProjectTo<AssetDeployDto>(deploys).ToListAsync();
            return CreateAssetDeployExcelPackage(dtos);

        }
        public async Task<PaginatedList<AssetDeployDto>> PaginationAsync(SieveModel model, Expression<Func<AssetDeploy, bool>> predicate)
        {
            var deploys = _assetDeployRepository.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, deploys, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, deploys).ProjectTo<AssetDeployDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<AssetDeployDto>(_sieveOptions, model.Page, model.PageSize, count, pagination);
        }

        private ExcelPackage CreateAssetDeployExcelPackage(IEnumerable<AssetDeployDto> assetDeploys)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "资产流转记录表";
            package.Workbook.Properties.Author = "wall-ee";
            package.Workbook.Properties.Subject = "包头分行信息科技中心";
            package.Workbook.Properties.Keywords = "资产流转";


            var worksheet = package.Workbook.Worksheets.Add("资产流转记录");

            //First add the headers
            var headerRow = 1;
            worksheet.Cells[headerRow, 1].Value = "资产名称";
            worksheet.Cells[headerRow, 2].Value = "资产标签号";
            worksheet.Cells[headerRow, 3].Value = "资产编号";
            worksheet.Cells[headerRow, 4].Value = "资产索引";
            worksheet.Cells[headerRow, 5].Value = "调配类型";
            worksheet.Cells[headerRow, 6].Value = "二级行";
            worksheet.Cells[headerRow, 7].Value = "日期";
            worksheet.Cells[headerRow, 8].Value = "转出机构号";
            worksheet.Cells[headerRow, 9].Value = "转出机构名称";
            worksheet.Cells[headerRow, 10].Value = "转入机构号";
            worksheet.Cells[headerRow, 11].Value = "转入机构名称";
            worksheet.Cells[headerRow, 12].Value = "审批机构号";
            worksheet.Cells[headerRow, 13].Value = "审批机构名称";
            var rowCount = 2;
            foreach (var item in assetDeploys)
            {
                worksheet.Cells[rowCount, 1].Value = item.AssetName;
                worksheet.Cells[rowCount, 2].Value = item.AssetTagNumber;
                worksheet.Cells[rowCount, 3].Value = item.AssetNo;
                worksheet.Cells[rowCount, 4].Value = item.AssetId;
                worksheet.Cells[rowCount, 5].Value = item.AssetDeployCategory;
                worksheet.Cells[rowCount, 6].Value = item.Org2;
                worksheet.Cells[rowCount, 7].Value = item.CreateDateTime.ToString("yyyy-MM-DD");
                worksheet.Cells[rowCount, 8].Value = item.ExportOrgIdentifier;
                worksheet.Cells[rowCount, 9].Value = item.ExportOrgNam;
                worksheet.Cells[rowCount, 10].Value = item.ImportOrgIdentifier;
                worksheet.Cells[rowCount, 11].Value = item.ImportOrgNam;
                worksheet.Cells[rowCount, 12].Value = item.AuthorizeOrgIdentifier;
                worksheet.Cells[rowCount, 13].Value = item.AuthorizeOrgNam;
                rowCount++;
            }
            // AutoFitColumns
            worksheet.Cells[1, 1, 4, 4].AutoFitColumns();
            return package;
        }
    }
}