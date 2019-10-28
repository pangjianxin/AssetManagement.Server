using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Models.Assets.TableViews;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetSumarryService : IAssetSumarryService
    {
        private readonly DbQuery<AssetSumarryByCategory> _assetSumarrybycategoryView;
        private readonly DbQuery<AssetSumarryByCount> _assetSumarrybyCountView;
        private readonly IMapper _mapper;

        public AssetSumarryService(ApplicationDbContext context, IMapper mapper)
        {
            _assetSumarrybycategoryView = context.AssetSumarryByCategorys;
            _assetSumarrybyCountView = context.AssetSumarryByCounts;
            this._mapper = mapper;
        }

        public IQueryable<ChartData> GetSumarryByCategory(Expression<Func<AssetSumarryByCategory, bool>> predicate = null)
        {

            return _mapper.ProjectTo<ChartData>(predicate == null ? _assetSumarrybycategoryView : _assetSumarrybycategoryView.Where(predicate));
        }

        public IQueryable<ChartData> GetSumarryByCount(Expression<Func<AssetSumarryByCount, bool>> predicate = null)
        {
            return _mapper.ProjectTo<ChartData>(predicate == null ? _assetSumarrybyCountView : _assetSumarrybyCountView.Where(predicate));
        }
    }
}