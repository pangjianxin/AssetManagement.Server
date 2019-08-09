using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetReturnService : IAssetReturnService
    {
        private readonly IAssetReturnRepository _assetReturnRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IUser _user;
        private readonly SieveOptions _sieveOptions;
        public AssetReturnService(
            IAssetReturnRepository assetReturnRepository,
            IMapper mapper,
            IBus bus,
            ISieveProcessor sieveProcessor,
            IUser user,
            IOptions<SieveOptions> options)
        {
            _assetReturnRepository = assetReturnRepository;
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _user = user;
            _sieveOptions = options.Value;
        }

        public async Task<PaginatedList<AssetReturnDto>> PaginationAsync(SieveModel model, Expression<Func<AssetReturn, bool>> predicate)
        {
            var entities = _assetReturnRepository.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, entities)
                .ProjectTo<AssetReturnDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<AssetReturnDto>(
                _sieveOptions, model.Page, model.PageSize, count, pagination);
        }

        public async Task<AssetReturnDto> RemoveAsync(Guid eventId)
        {
            var assetReturn = await _assetReturnRepository.GetByIdAsync(eventId);
            if (assetReturn == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，请联系管理员或重新发起"));
                return null;
            }
            _assetReturnRepository.Remove(assetReturn);
            return _mapper.Map<AssetReturnDto>(assetReturn);
        }

        public async Task RevokeAsync(Revoke model)
        {
            var command = _mapper.Map<RevokeAssetReturnCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task HandleAsync(HandleAssetReturn model)
        {
            var command = _mapper.Map<HandleAssetReturnCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task AssetReturnAsync(ReturnAsset model)
        {
            var command = _mapper.Map<ReturnAssetCommand>(model);
            await _bus.SendCommandAsync(command);
        }
    }
}