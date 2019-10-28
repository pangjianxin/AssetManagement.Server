using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetInventoryRegisterService : IAssetInventoryRegisterService
    {
        private readonly IMapper _mapper;
        private readonly IAssetInventoryRegisterRepository _registerRepository;

        public AssetInventoryRegisterService(IMapper mapper, IAssetInventoryRegisterRepository registerRepository)
        {
            _mapper = mapper;
            _registerRepository = registerRepository;
        }
        public IQueryable<AssetInventoryRegisterDto> Get(Expression<Func<AssetInventoryRegister, bool>> predicate = null)
        {
            //   _registerRepository.GetAll(predicate).UseAsDataSource
            //var result = _registerRepository.GetAll(predicate)
            //    .UseAsDataSource(_mapper.ConfigurationProvider).For<AssetInventoryRegisterDto>();
            //return result;
            return _mapper.ProjectTo<AssetInventoryRegisterDto>(_registerRepository.GetAll(predicate));
        }
    }
}